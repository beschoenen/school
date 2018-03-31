//
//  InputNode.swift
//  Circuit
//
//  Created by Kevin Richter on 11/05/2017.
//  Copyright © 2017 Job & Kevin. All rights reserved.
//

import Foundation

class InputNode: Node {

    var value: Bool

    var name: String?

    var next: [Element] = []

    var previous: [Element] = []

    init(value: Bool = false) {
        self.value = value
    }

    func clone() -> Element {
        return InputNode(value: self.value)
    }

    func isValid() -> (Enums.validation, String?) {
        if self.name == nil {
            return (.Error, "Input node has no name")
        }

        if self.previous.count > 0 {
            return (.Warning, "Input node \"\(self.name!)\" has preceding elements")
        }

        if self.next.count < 1 {
            return (.Warning, "Input node \"\(self.name!)\" is not connected to anything")
        }

        return (.Valid, nil)
    }

    func getValue() -> Bool {
        return value
    }

    func set(next: Element) {
        self.next.append(next)
    }

    func set(previous: Element) {
        // Don't need this
    }
}
