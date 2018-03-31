//
//  InputParser.swift
//  Circuit
//
//  Created by Kevin Richter on 11/05/2017.
//  Copyright © 2017 Job & Kevin. All rights reserved.
//

import Foundation

class InputParser {

    static let pattern: String = "^([0-9A-Za-z]+):\\s+([0-9A-Za-z,_]+);$"

    static func parse(file: String) -> [ElementBlueprint] {
        var elements: [ElementBlueprint] = []

        // Loop through lines in file
        for line in file.components(separatedBy: "\n") {
            let groups = line.capturedGroups(withRegex: self.pattern)

            // Check whether all required group were matched
            if groups.count < 2 { continue }

            // Check if a model with a certain name already exists
            if let model = elements.first(where: { $0.name == groups[0] }) {
                // Add next nodes
                model.add(next: groups[1])
            } else {
                // Create new model
                elements.append(ElementBlueprint(name: groups[0], type: groups[1]))
            }
        }

        return elements
    }
}
