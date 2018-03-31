//
//  OutputNode.swift
//  Circuit
//
//  Created by Kevin Richter on 11/05/2017.
//  Copyright © 2017 Job & Kevin. All rights reserved.
//

import Foundation

class OutputNode: Node {

    var name: String?

    var next: [Element] = []

    var previous: [Element] = []

    func clone() -> Element {
        return OutputNode()
    }

    func isValid() -> (Enums.validation, String?) {
        if self.name == nil {
            return (.Error, "Output node has no name")
        }

        if self.next.count > 0 {
            return (.Warning, "Output node \"\(self.name!)\" has succeding elements")
        }

        if self.previous.count < 1 {
            return (.Warning, "Output node \"\(self.name!)\" is not connected to anything")
        }

        return (.Valid, nil)
    }

    func getValue() -> Bool {
        return previous[0].getValue()
    }

    func set(next: Element) {
        // Don't need this
    }

    func set(previous: Element) {
        self.previous = [previous]
    }

}
