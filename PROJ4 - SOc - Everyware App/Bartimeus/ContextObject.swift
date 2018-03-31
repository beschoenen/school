//
//  ContextObject.swift
//  Bartimeus
//
//  Created by mlcroivsvsa on 24/05/2017.
//  Copyright © 2017 Kevin Richter. All rights reserved.
//

import Foundation

class ContextObject {
    let name: String?
    var linkedBeacons: [LinkedBeacon] = []

    init?(with input: [String : Any]?) {
        guard let json = input else { return nil }

        name = json["name"] as? String

        if let linkedBeaconJSONArray = (json["linkedBeacon"] as? [[String : Any]]) {
            for linkedBeaconJSON in linkedBeaconJSONArray {
                if let linkedBeacon = LinkedBeacon(with: linkedBeaconJSON) {
                    self.linkedBeacons.append(linkedBeacon)
                }
            }
        }
    }
}
