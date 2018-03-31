//
//  ElementBlueprint.swift
//  Circuit
//
//  Created by Kevin Richter on 23/05/2017.
//  Copyright © 2017 Job & Kevin. All rights reserved.
//

import Foundation

class ElementBlueprint {

    let name: String

    let type: String

    var next: [String] = []

    init(name: String, type: String) {
        self.name = name
        self.type = type
    }

    func add(next: String) {
        self.next.append(contentsOf: next.components(separatedBy: ","))
    }

}
