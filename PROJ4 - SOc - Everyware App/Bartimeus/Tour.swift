//
//  Tour.swift
//  Bartimeus
//
//  Created by Kevin Richter on 11/06/2017.
//  Copyright © 2017 Kevin Richter. All rights reserved.
//

import Foundation

class Tour {

    let id: Int
    let name: String
    let spokenDescription: String
    let writtenDescription: String?

    var nodes: [TourNode] = []

    init(fromJson json: [String: Any]) {
        self.id = json["id"] as! Int
        self.name = json["name"] as! String
        self.spokenDescription = json["spoken_description"] as! String
        self.writtenDescription = json["written_description"] as? String
    }

    func loadNodes(callback: @escaping (Bool) -> Void) {
        CMS.nodes(forTour: self.id) { nodes in
            self.nodes = nodes
            callback(self.nodes.count > 0)
        }
    }

}
