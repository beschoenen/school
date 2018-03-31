//
//  NandGate.swift
//  Circuit
//
//  Created by Kevin Richter on 11/05/2017.
//  Copyright © 2017 Job & Kevin. All rights reserved.
//

import Foundation

class NandGate: Gate {

    var name: String?

    var next: [Element] = []

    var previous: [Element] = []

    func clone() -> Element {
        return NandGate()
    }

    func getValue() -> Bool {
        return !previous.map({ $0.getValue() }).reduce(true, { $0 && $1 })
    }

    func set(previous: Element) {
        self.previous.append(previous)
    }

    func set(next: Element) {
        self.next = [next]
    }

}
