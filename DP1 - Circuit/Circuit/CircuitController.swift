//
//  CircuitController.swift
//  Circuit
//
//  Created by Kevin Richter on 11/05/2017.
//  Copyright © 2017 Job & Kevin. All rights reserved.
//

import Foundation

class CircuitController {

    private let interactor: InteractionProvider.Type
    private let file: FileProvider.Type
    private let view: ViewProvider.Type

    private var circuit: Circuit?

    init(withInteractor interactor: InteractionProvider.Type, fileReader file: FileProvider.Type, view: ViewProvider.Type) {
        self.interactor = interactor
        self.file = file
        self.view = view
    }

    func loadFile() -> Bool {
        guard let path = self.interactor.chooseFile(inPath: CommandLine.arguments.indices.contains(1) ? CommandLine.arguments[1] : ".") else {
            self.interactor.show(error: "No file was choosen or non were available")
            return false
        }

        guard let file = file.readFile(path: path) else { return false }

        let elements: [ElementBlueprint] = InputParser.parse(file: file)

        let (circuit, hasError) = CircuitBuilder(blueprints: elements, interactingVia: interactor).parse()

        if hasError {
            return false
        }

        self.circuit = circuit

        return true
    }

    func runSimulation() {
        guard let circuit = self.circuit else {
            return self.interactor.show(error: "Circuit was not set properly")
        }

        while true {
            view.printView(inputNodes: circuit.inputNodes, outputNodes: circuit.outputNodes)

            let nodes = circuit.inputNodes.map({ $0.name! })

            guard let index = self.interactor.toggleInput(nodes: nodes) else { return }

            circuit.inputNodes[index].value = !circuit.inputNodes[index].value
        }
    }

}
