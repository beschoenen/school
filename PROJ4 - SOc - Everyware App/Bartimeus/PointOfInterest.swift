//
//  PointOfIntrest.swift
//  Bartimeus
//
//  Created by mlcroivsvsa on 24/05/2017.
//  Copyright © 2017 Kevin Richter. All rights reserved.
//

import Foundation

class PointOfIntrest {
    let name: String?
    let direction: String?
    let description: String?
    let spokenDescription: String?

    init?(with input: [String : Any]?) {
        guard let json = input else { return nil }

        name = json["name"] as? String
        direction = json["direction"] as? String
        description = json["description"] as? String
        spokenDescription = json["spokenDescription"] as? String
    }
}
