//
//  ElementFactory.swift
//  Circuit
//
//  Created by Kevin Richter on 11/05/2017.
//  Copyright © 2017 Job & Kevin. All rights reserved.
//

import Foundation

class ElementFactory {

    private static var elements: [String: Element] = [:]

    class func register(element: Element, as key: String) {
        elements[key] = element
    }

    class func getCopyOf(type key: String) -> Element {
        return (elements[key]?.clone())!
    }
}
