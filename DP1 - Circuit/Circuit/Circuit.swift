//
//  Circuit.swift
//  Circuit
//
//  Created by Kevin Richter on 11/05/2017.
//  Copyright © 2017 Job & Kevin. All rights reserved.
//

import Foundation

class Circuit {

    var elements: [Element] = []

    var inputNodes: [InputNode] {
        get {
            return elements.flatMap { $0 as? InputNode }
        }
    }

    var outputNodes: [OutputNode] {
        get {
            return elements.flatMap { $0 as? OutputNode }
        }
    }

    var gates: [Gate] {
        get {
            return elements.flatMap { $0 as? Gate }
        }
    }

}
