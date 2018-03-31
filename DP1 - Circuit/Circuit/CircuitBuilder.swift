//
//  CircuitBuilder.swift
//  Circuit
//
//  Created by Kevin Richter on 11/05/2017.
//  Copyright © 2017 Job & Kevin. All rights reserved.
//

import Foundation

class CircuitBuilder {

    private let interactor: InteractionProvider.Type

    private let circuit: Circuit

    private let blueprints: [ElementBlueprint]

    init(blueprints: [ElementBlueprint], interactingVia interactor: InteractionProvider.Type) {
        self.blueprints = blueprints
        self.interactor = interactor
        self.circuit = Circuit()
    }

    func parse() -> (Circuit, Bool) {
        self.blueprints.forEach { blueprint in
            // Get class instance from the factory
            var element = ElementFactory.getCopyOf(type: blueprint.type)
            // Set its name
            element.name = blueprint.name

            self.circuit.elements.append(element)
        }

        if !linkElements() {
            return (circuit, true)
        }

        return (circuit, checkCircuit())
    }

    func linkElements() -> Bool {
        for (index, blueprint) in self.blueprints.enumerated() {
            if !resolve(next: blueprint.next, forElement: self.circuit.elements[index]) {
                return false
            }
        }

        return true
    }

    func checkCircuit() -> Bool {
        for element in self.circuit.elements {
            // Check if all elements are valid
            let valid = element.isValid()

            if valid.0 == .Error {
                self.interactor.show(error: valid.1!)
                return true
            } else if valid.0 == .Warning {
                self.interactor.show(warning: valid.1!)
            }
        }

        return hasLoop()
    }

    // Find an element from the circuit by its name
    func findElementBy(name: String) -> Element? {
        return self.circuit.elements.first(where: { $0.name! == name })
    }

    func resolve(next: [String], forElement element: Element) -> Bool {
        var result = true

        next.forEach { name in
            // If the element could be found
            if let nextElement = self.findElementBy(name: name) {
                // Add it to the next of the element
                element.set(next: nextElement)
                // And set the current element to the previous of the found one
                nextElement.set(previous: element)
            } else {
                self.interactor.show(error: "This circuit is invalid")
                return result = false
            }
        }

        return result
    }

    func hasLoop() -> Bool {
        // Do a check on every input node
        for element in self.circuit.inputNodes {
            if check(next: element) {
                self.interactor.show(error: "Circuit has a loop")
                return true
            }
        }

        return false
    }

    func check(next: Element, previous: [Element] = []) -> Bool {
        var prev = previous
        var result = false

        for element in next.next {
            if prev.contains(where: { $0.name! == element.name! }) {
                return true
            }

            prev.append(element)
            result = result || check(next: element, previous: prev)
        }

        return result
    }

}
