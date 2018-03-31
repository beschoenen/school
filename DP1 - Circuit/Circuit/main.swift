//
//  main.swift
//  Circuit
//
//  Created by Kevin Richter on 11/05/2017.
//  Copyright © 2017 Job & Kevin. All rights reserved.
//

import Foundation

// Default implementation for strategy pattern
let interactor: InteractionProvider.Type = ConsoleInteractor.self
let view: ViewProvider.Type = ConsoleView.self
let reader: FileProvider.Type = DiskReader.self

// Fill Element factory
ElementFactory.register(element: NandGate(), as: "NAND")
ElementFactory.register(element: OrGate(), as: "OR")
ElementFactory.register(element: XorGate(), as: "XOR")
ElementFactory.register(element: AndGate(), as: "AND")

ElementFactory.register(element: Inverter(), as: "NOT")

ElementFactory.register(element: InputNode(value: true), as: "INPUT_HIGH")
ElementFactory.register(element: InputNode(value: false), as: "INPUT_LOW")

ElementFactory.register(element: OutputNode(), as: "PROBE")

// Application start
let controller = CircuitController(withInteractor: interactor, fileReader: reader, view: view)

if controller.loadFile() {
    controller.runSimulation()
}
