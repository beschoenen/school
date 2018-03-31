//
//  Inverter.swift
//  Circuit
//
//  Created by Kevin Richter on 11/05/2017.
//  Copyright © 2017 Job & Kevin. All rights reserved.
//

import Foundation

class Inverter: Gate {

    var name: String?

    var next: [Element] = []

    var previous: [Element] = []

    func clone() -> Element {
        return Inverter()
    }

    func isValid() -> (Enums.validation, String?) {
        if self.name == nil {
            return (.Error, "Inverter has no name")
        }

        if self.previous.count > 1 {
            return (.Error, "Invalid circuit, inverter \(self.name!) has multiple inputs")
        }

        if self.previous.count < 1 {
            return (.Warning, "Inverter \"\(self.name!)\" has no inputs")
        }

        if self.next.count < 1 {
            return (.Warning, "Inverter \"\(self.name!)\" is not connected to anything")
        }

        return (.Valid, nil)
    }

    func getValue() -> Bool {
        return !self.previous[0].getValue()
    }

    func set(next: Element) {
        self.next.append(next)
    }

    func set(previous: Element) {
        self.previous = [previous]
    }

}
