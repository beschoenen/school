//
//  Instruction.swift
//  Bartimeus
//
//  Created by Job Cuppen on 20/06/2017.
//  Copyright © 2017 Kevin Richter. All rights reserved.
//

import Foundation

class Instruction {
    let fromBeaconId: String?
    let instruction: String?
    let direction: Int?

    init?(with input: [String : Any]?) {
        self.fromBeaconId = input?["from"] as? String
        self.instruction = input?["instruction"] as? String
        self.direction = Int((input?["direction"] as? String)!)
    }
}
