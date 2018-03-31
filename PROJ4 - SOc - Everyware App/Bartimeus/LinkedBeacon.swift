//
//  LinkedBeacon.swift
//  Bartimeus
//
//  Created by Job Cuppen on 13/06/2017.
//  Copyright © 2017 Kevin Richter. All rights reserved.

import Foundation

class LinkedBeacon {
    let beaconId: String?
    var instrutions: [Instruction] = []

    init?(with input: [String : Any]?) {
        self.beaconId = input?["beacon"] as? String
        guard let arr = input?["instructions"] as? [[String : Any]] else { return nil }
        for item in arr {
            instrutions.append(Instruction(with: item)!)
        }
    }
}
