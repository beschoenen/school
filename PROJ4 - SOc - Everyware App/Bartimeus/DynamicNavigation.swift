//
//  DynamicNavigation.swift
//  Bartimeus
//
//  Created by Kevin Richter on 17/06/2017.
//  Copyright © 2017 Kevin Richter. All rights reserved.
//

import Foundation

class DynamicNavigation: Navigator, UpdateRouteDelegate {

    var navigationListeners: [NavigationListener] = []

    var beacons: [MovinBeacon] = []
    var routeBeacons: [String : RouteBeacon?] = [:]

    var destination: MovinBeacon?
    var currentBeacon: RouteBeacon?

    var canNavigate: Bool = false
    var routeSelectionTitle: String = "Selecteer bestemming"

    func routeList(callback: @escaping ([String : String]) -> Void) {
        Movin.instance.getAllBeacons { beacons, _ in
            if beacons != nil {
                self.beacons = beacons!

                var beaconList: [String : String] = [:]

                self.beacons.forEach { beacon in
                    beaconList[beacon.id] = beacon.beaconCode
                }

                callback(beaconList)
            }
        }
    }

    func select(route: String) {
        self.destination = self.beacons.first { beacon in
            return beacon.id == route
        }

        self.canNavigate = self.destination != nil
    }

    func startNavigation() {
        _ = NavigationManager.add(routeDelegate: self)
        _ = BeaconListener.add(nearest: self)

        self.routeBeacons = [:]

        let start = BeaconListener.nearestBeacon?.movinRangedBeacon?.beacon

        NavigationManager.instance.getRoute(from: start, to: destination)
    }

    func currentInstruction() -> String? {
        return self.currentBeacon?.instruction
    }

    func currentDirection() -> Int? {
        return self.currentBeacon?.direction
    }

    func updateNearest(beacon: BartimeusRangedBeacon) {
        if self.routeBeacons.count > 0 {
            if let b = self.routeBeacons[(beacon.movinRangedBeacon?.beacon?.beaconCode)!] {
                self.currentBeacon = b
            }
            self.notifyListeners()
        }
        if self.currentBeacon?.beacon.movinBeacon == destination {
            self.routeEnded()
            self.routeBeacons = [:]
        }
    }

    func updateRoute(route: [String : RouteBeacon]) {
        self.routeBeacons = route
        self.updateNearest(beacon: BeaconListener.nearestBeacon!)
    }

}
