//
//  NavigationManager.swift
//  Bartimeus
//
//  Created by Job Cuppen on 07/06/2017.
//  Copyright © 2017 Kevin Richter. All rights reserved.
//

import Foundation

protocol UpdateRouteDelegate {
    func updateRoute(route: [String : RouteBeacon])
}

class NavigationManager: InRangeBeaconsNotifier {

    static let instance = NavigationManager()

    static private var routeListeners: [Int : UpdateRouteDelegate] = [:]

    let floor = 1.0
    let beaconSearchSize = 0.00002

    var startB: MovinBeacon?
    var endB: MovinBeacon?

    var startNode: MovinNavigationNode?
    var endNode: MovinNavigationNode?

    var inRangeBeacons: [BartimeusRangedBeacon?] = []

    var beaconsRoute: [String : RouteBeacon] = [:]

    private init() {
        _ = BeaconListener.add(inRange: self)
    }

    func getRoute(from startBeacon: MovinBeacon?, to destinationBeacon: MovinBeacon?) {
        self.beaconsRoute = [:]

        setStartNode(start: startBeacon)
        setDestinationNode(destination: destinationBeacon)
        setRoute { route in
            self.setRouteBeacons(movinRoute: route)
        }
    }

    static func add(routeDelegate delegate: UpdateRouteDelegate) -> Int {
        let index = self.routeListeners.count + 1

        self.routeListeners[index] = delegate

        return index
    }

    func updateInRange(beacons: [BartimeusRangedBeacon]) {
        self.inRangeBeacons = beacons
    }

    private func setStartNode(start: MovinBeacon?) {

        let map = Movin.instance.map!

        let startFloorPosition: FloorPosition = start!.position!

        let startPoint: GeoLatLng = startFloorPosition.position
        let startFloor: Double = startFloorPosition.floor

        map.getNavigationNodeNearest(toPoint: startPoint, atFloor: startFloor) { (navigationNode, error) in
            guard error == nil else { return }

            self.startNode = navigationNode!
            self.startB = start

            print("Start was set")
        }
    }

    private func setDestinationNode(destination: MovinBeacon?) {
        let map = Movin.instance.map!

        let endFloorPosition: FloorPosition = destination!.position!

        let endPoint: GeoLatLng = endFloorPosition.position
        let endfloor: Double = endFloorPosition.floor

        map.getNavigationNodeNearest(toPoint: endPoint, atFloor: endfloor) { (navigationNode, error) in
            guard error == nil else { return }

            self.endNode = navigationNode!
            self.endB = destination

            print("End was set")
        }
    }

    private func setRoute(callback: @escaping (_ movinRoute: MovinRoute?) -> Void) {
        guard let routingManager = try? Movin.instance.map.getRouter() else {
            print("Unknown error while getting route")
            return callback(nil)
        }

        routingManager.getRouteFrom((self.startNode?.position)!, to: (self.endNode?.position)!,
                                    withInstructionFeatures: [""]) { (route, error) in
            guard error == nil else { return callback(nil) }

            print("Route was fetched")
            callback(route)
        }
    }

    func setRouteBeacons(movinRoute: MovinRoute?) {
        let floorPositions = movinRoute!.path

        var prevKey = ""

        for floorPosition in floorPositions {
            let aabb = GeoAABB(origin: floorPosition.position, andWidth: self.beaconSearchSize,
                               andHeight: self.beaconSearchSize)
            let beacons = Movin.instance.map.getBeaconsIn(aabb, andFloor: self.floor)

            guard (beacons?.indices.contains(0))! else {
                continue
            }

            let navBeacon = (beacons?[0])! //dichtstbijzijnde

            guard let data = navBeacon.properties["description"] as? String else { return }
            let json = JsonParser.parseToDictionary(text: data)

            guard let bartimeusBeacon = BartimeusBeacon(withMovinBeacon: navBeacon, andJson: json) else {
                return
            }

            var prevId = ""
            if prevKey == "" {
                prevId = (bartimeusBeacon.movinBeacon?.id)!
            } else {
                prevId = (self.beaconsRoute[prevKey]?.beacon.movinBeacon?.id)!
            }

            self.beaconsRoute[navBeacon.beaconCode] = RouteBeacon(
                withBeacon: bartimeusBeacon,
                andInstruction: "Bestemming bereikt \(navBeacon.beaconCode)",
                andDirection: 8,
                andPrevBeaconId: prevId
            )

            if self.beaconsRoute.count > 1 {
                let oldRouteBeacon = self.beaconsRoute[prevKey] // start
                let currentId = navBeacon.id // 3yeb id

                let oldBeaconLinkedBeacon = oldRouteBeacon?.beacon.contextObject?.linkedBeacons.first { linkedBeacon in
                    linkedBeacon.beaconId == currentId
                } // linkedBeacon van prev naar current

                let instructionToCurrent = oldBeaconLinkedBeacon?.instrutions.first { instruction in
                    instruction.fromBeaconId == oldRouteBeacon?.prevBeaconId
                }

                let newNavBeacon = RouteBeacon(
                    withBeacon: (oldRouteBeacon?.beacon)!,
                    andInstruction: (instructionToCurrent?.instruction)!,
                    andDirection: (instructionToCurrent?.direction)!,
                    andPrevBeaconId: (oldRouteBeacon?.prevBeaconId)!
                )

                self.beaconsRoute[prevKey] = newNavBeacon
            }

            prevKey = navBeacon.beaconCode
        }

        print("Route calculated.")

        NavigationManager.routeListeners.forEach { _, listener in
            listener.updateRoute(route: self.beaconsRoute)
        }
    }
}
