//
//  Element.swift
//  Circuit
//
//  Created by Kevin Richter on 11/05/2017.
//  Copyright © 2017 Job & Kevin. All rights reserved.
//

import Foundation

protocol Element {

    var name: String? { get set }

    var next: [Element] { get set }

    var previous: [Element] { get set }

    func clone() -> Element

    func isValid() -> (Enums.validation, String?)

    func getValue() -> Bool

    func set(next: Element)

    func set(previous: Element)

}

extension Element {

    func isValid() -> (Enums.validation, String?) {
        if self.name == nil {
            return (.Error, "Parse error, element has no name")
        }

        return (.Valid, nil)
    }

}
