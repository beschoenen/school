//
//  TourNavigation.swift
//  Bartimeus
//
//  Created by Kevin Richter on 16/06/2017.
//  Copyright © 2017 Kevin Richter. All rights reserved.
//

import Foundation

class TourNavigation: Navigator, NearestBeaconNotifier {

    var navigationListeners: [NavigationListener] = []

    var canNavigate: Bool = false
    var routeSelectionTitle: String = "Selecteer een tour"

    private var tours: [Tour] = []
    private var selectedTour: Tour?
    private var currentNode: TourNode?

    private func turnBackNode() -> TourNode {
        return TourNode(fromJson: [
            "node": "1",
            "spoken_description": "Ga terug",
            "direction": -1,
            "is_start": false,
            "beacon": [
                "beacon": [
                    "beaconCode": "fake"
                ]
            ]
        ])
    }

    func routeList(callback: @escaping ([String : String]) -> Void) {
        CMS.tours { tours in
            self.tours = tours

            var selectableTours: [String : String] = [:]

            self.tours.forEach { tour in
                selectableTours[String(tour.id)] = tour.name
            }

            DispatchQueue.main.async {
                callback(selectableTours)
            }
        }
    }

    func select(route: String) {
        self.selectedTour = self.tours.first { tour in
            return String(tour.id) == route
        }

        self.canNavigate = self.selectedTour != nil
    }

    func startNavigation() {
        _ = BeaconListener.add(nearest: self)

        CMS.nodes(forTour: (self.selectedTour?.id)!) { nodes in
            self.selectedTour?.nodes = nodes

            DispatchQueue.main.async {
                self.notifyListeners()
            }
        }
    }

    func currentInstruction() -> String? {
        return self.currentNode?.spokenDescription
    }

    func currentDirection() -> Int? {
        return self.currentNode?.direction
    }

    func updateNearest(beacon: BartimeusRangedBeacon) {
        if let node = self.isInTour(beacon: beacon) {
            self.currentNode = node
        } else {
            self.currentNode = self.turnBackNode()
        }

        self.notifyListeners()

        if self.currentNode?.nextNode == nil &&
           self.selectedTour?.nodes.last?.beaconCode == self.currentNode?.beaconCode {
            self.routeEnded()
        }
    }

    func isInTour(beacon: BartimeusRangedBeacon) -> TourNode? {
        return self.selectedTour?.nodes.first { node in
            return node.beaconCode == beacon.movinRangedBeacon?.beacon?.beaconCode
        }
    }

}
