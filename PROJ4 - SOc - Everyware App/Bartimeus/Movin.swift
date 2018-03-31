//
//  Movin.swift
//  Bartimeus
//
//  Created by Kevin Richter on 24/05/2017.
//  Copyright © 2017 Kevin Richter. All rights reserved.
//

import Foundation

class Movin {

    /////////////
    // Properties

    static let instance = Movin()

    let clientName: String = "bartimeus-c"

    var apiKey: String
    var scanner: MovinBeaconScanner!
    var map: MovinMap!

    //////////////
    // Constructor

    private init() {
        let path: String = Bundle.main.path(forResource: "config", ofType: "plist")!
        let config: NSDictionary = NSDictionary(contentsOfFile: path)!

        self.apiKey = (config.object(forKey: "api-key") as? String)!
    }

    //////////
    // Methods

    func initialize(callback: @escaping (_ success: Bool) -> Void) {
        MovinLocationManager.instance().requestWhenInUseAuthorization()

        MovinSDK.setCacheProtocolForTypes(MovinCacheableData.all, to: MovinCacheProtocol.doNotCache())
        MovinSDK.initWithCustomer(self.clientName, andApikey: self.apiKey) { _, err in
            guard err == nil else { return callback(false) }

            do {
                self.map = try MovinSDK.getMaps()[1]
            } catch {
                print("Unable to get maps")
                return callback(false)
            }

            self.map.downloadNavigationNodes { succes, _ in
                guard succes else { return callback(false) }

                print("All navigation nodes on current map have been downloaded.")

                self.map.downloadBeaconData { success, _ in
                    guard success else { return callback(false) }

                    print("All beacons on current map have been downloaded.")
                }
            }

            do {
                self.scanner = try MovinSDK.getBeaconScanner()
            } catch {
                print("Unable to initialize beacon scanner")
                return callback(false)
            }

            self.scanner.add(self.map)
            self.scanner.start(with: BeaconListener.instance)

            callback(true)
        }
    }

    func getAllBeacons(withCallback callback: @escaping ([MovinBeacon]?, Error?) -> Void) {
        self.map.getBeaconsWithCallback { beacons, errors in
            callback(beacons, errors)
        }
    }

    /////////////////
    // Helper methods

    static func nameOf(proximity: CLProximity) -> String {
        switch proximity {
            case CLProximity.immediate:
                return "immediate"
            case CLProximity.far:
                return "far"
            case CLProximity.near:
                return "near"
            default:
                return "unknown"
        }
    }
}
