//
//  BeaconListener.swift
//  Bartimeus
//
//  Created by Kevin Richter on 24/05/2017.
//  Copyright © 2017 Kevin Richter. All rights reserved.
//

import Foundation

protocol NearestBeaconNotifier {
    func updateNearest(beacon: BartimeusRangedBeacon)
}

protocol InRangeBeaconsNotifier {
    func updateInRange(beacons: [BartimeusRangedBeacon])
}

/**
 This class is responsilbe for implementing all functions of the MovinBeaconScannerListener
 [see movinSDK documentation](https://movinio.github.io/MovinSDK-iOS/Protocols/MovinBeaconScannerListener.html)
 */
class BeaconListener: NSObject, MovinBeaconScannerListener {

    /////////////
    // Properties

    static let instance = BeaconListener()

    static private var nearestListeners: [Int : NearestBeaconNotifier] = [:]
    static private var inRangeListeners: [Int : InRangeBeaconsNotifier] = [:]

    static var nearestBeacon: BartimeusRangedBeacon?

    //////////////////////
    // Listener management

    static func add(nearest delegate: NearestBeaconNotifier) -> Int {
        let index = self.nearestListeners.count + 1

        self.nearestListeners[index] = delegate

        return index
    }

    static func add(inRange delegate: InRangeBeaconsNotifier) -> Int {
        let index = self.inRangeListeners.count + 1

        self.inRangeListeners[index] = delegate

        return index
    }

    static func removeNearest(listener index: Int) {
        self.nearestListeners.removeValue(forKey: index)
    }

    static func removeInRange(listener index: Int) {
        self.nearestListeners.removeValue(forKey: index)
    }

    ////////////////////
    // Events from Movin

    func beaconScanner(_ scanner: MovinBeaconScanner, didChangeNearestBeacon beacon: MovinRangedBeacon?) {
        guard let rangedBeacon = beacon else { return }
        guard let movinBeacon = rangedBeacon.beacon else { return }
        guard let data = movinBeacon.properties["description"] as? String else { return }
        let json = JsonParser.parseToDictionary(text: data)

        guard let bartimeusBeacon = BartimeusRangedBeacon(withMovinRangedBeacon: rangedBeacon, andJson: json) else {
            return
        }

        BeaconListener.nearestBeacon = bartimeusBeacon

        // BeaconListener.nearestBeacon = rangedBeacon;

        BeaconListener.nearestListeners.forEach { _, listener in
            listener.updateNearest(beacon: BeaconListener.nearestBeacon!)
        }
    }

    func beaconScanner(_ scanner: MovinBeaconScanner, didRangeBeacons beacons: [MovinRangedBeacon]) {
        var validBeacons: [BartimeusRangedBeacon] = []

        for rangedBeacon in beacons {
            guard let beacon = rangedBeacon.beacon else { return }
            guard let data = beacon.properties["description"] as? String else { return }
            let json = JsonParser.parseToDictionary(text: data)

            guard let bartimeusBeacon = BartimeusRangedBeacon(withMovinRangedBeacon: rangedBeacon, andJson: json) else {
                return
            }
            validBeacons.append(bartimeusBeacon)

            let nearestMovinRangedBeacon = BeaconListener.nearestBeacon?.movinRangedBeacon
            let nearestBeaconCode = nearestMovinRangedBeacon?.beacon?.beaconCode
            let nearestBeaconProximity = nearestMovinRangedBeacon?.proximity

            if beacon.beaconCode == nearestBeaconCode && rangedBeacon.proximity != nearestBeaconProximity {
                beaconScanner(scanner, didChangeNearestBeacon: rangedBeacon)
            }
        }

        BeaconListener.inRangeListeners.forEach { _, listener in
            listener.updateInRange(beacons: validBeacons)
        }
    }

    func beaconScanner(_ scanner: MovinBeaconScanner, didEnterAreaWith uuid: UUID) {
        //
    }

    func beaconScanner(_ scanner: MovinBeaconScanner, didExitAreaWith uuid: UUID) {
        //
    }

    func beaconScanner(_ scanner: MovinBeaconScanner, isValidNearestBeacon beacon: MovinRangedBeacon) -> Bool {
        return true
    }
}
