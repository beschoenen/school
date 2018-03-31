//
//  RouteBeacon.swift
//  Bartimeus
//
//  Created by Thomas van Leeuwen on 01/06/2017.
//  Copyright © 2017 Kevin Richter. All rights reserved.

import Foundation

class RouteBeacon {
    let beacon: BartimeusBeacon
    let instruction: String
    let direction: Int
    let prevBeaconId: String

    init(withBeacon beacon: BartimeusBeacon, andInstruction instruction: String, andDirection direction: Int,
         andPrevBeaconId beaconId: String) {
        self.beacon = beacon
        self.instruction = instruction
        self.direction = direction
        self.prevBeaconId = beaconId
    }
}
