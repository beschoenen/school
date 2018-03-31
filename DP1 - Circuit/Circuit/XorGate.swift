//
//  XorGate.swift
//  Circuit
//
//  Created by Kevin Richter on 11/05/2017.
//  Copyright © 2017 Job & Kevin. All rights reserved.
//

import Foundation

class XorGate: Gate {

    var name: String?

    var next: [Element] = []

    var previous: [Element] = []

    func clone() -> Element {
        return XorGate()
    }

    func getValue() -> Bool {
        return self.previous[0].getValue() != self.previous[1].getValue()
    }

    func set(previous: Element) {
        self.previous.append(previous)
    }

    func set(next: Element) {
        self.next = [next]
    }

}
