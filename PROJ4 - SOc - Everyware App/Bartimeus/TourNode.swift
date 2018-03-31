//
//  TourNode.swift
//  Bartimeus
//
//  Created by Kevin Richter on 11/06/2017.
//  Copyright © 2017 Kevin Richter. All rights reserved.
//

import Foundation

class TourNode {

    let node: String
    let nextNode: String?
    let spokenDescription: String
    let writtenDescription: String?
    let direction: Int
    let isStart: Bool

    let beaconCode: String

    init(fromJson json: [String: Any]) {
        self.node = json["node"] as! String
        self.nextNode = json["next_node"] as? String
        self.spokenDescription = json["spoken_description"] as! String
        self.writtenDescription = json["written_description"] as? String
        self.direction = json["direction"] as! Int
        self.isStart = json["is_start"] as! Bool

        let beacon = json["beacon"] as! [String : Any]

        self.beaconCode = (beacon["beacon"] as! [String : Any])["beaconCode"] as! String
    }

}
