//
//  CircuitBuilderTests.swift
//  Circuit
//
//  Created by Kevin Richter on 02/06/2017.
//  Copyright © 2017 Job & Kevin. All rights reserved.
//

import XCTest

class CircuitBuilderTests: BaseTest {

    let smallAdder = "" +
    "A: INPUT_HIGH;\n" +
    "B: INPUT_HIGH;\n" +
    "Cin:   INPUT_LOW;\n" +
    "Cout:  PROBE;\n" +
    "S: PROBE;\n" +
    "NODE1: OR;\n" +
    "NODE2: AND;\n" +
    "NODE3: AND;\n" +
    "NODE4: NOT;\n" +
    "NODE5: AND;\n" +
    "NODE6: OR;\n" +
    "Cin:   NODE3;\n" +
    "A: NODE1,NODE2;\n" +
    "B: NODE1,NODE2;\n" +
    "NODE1: NODE3,NODE5;\n" +
    "NODE2: NODE4,NODE6;\n" +
    "NODE3: NODE6;\n" +
    "NODE4: NODE5;\n" +
    "NODE5: S;\n" +
    "NODE6: Cout;\n"

    var loop = "" +
    "1: INPUT_HIGH;\n" +
    "2: OR;\n" +
    "3: OR;\n" +
    "4: NOT;\n" +
    "5: NOT;\n" +
    "6: AND;\n" +
    "1: 2;\n" +
    "2: 3;\n" +
    "3: 4,5;\n" +
    "5: 6;\n" +
    "6: 2;\n"

    var notConnected = "" +
    "A: INPUT_HIGH;\n" +
    "B: INPUT_LOW;\n" +
    "F: PROBE;\n" +
    "NODE1: AND;\n" +
    "NODE2: AND;\n" +
    "NODE3: AND;\n" +
    "NODE4: AND;\n" +
    "A: NODE1;\n" +
    "B: NODE1;\n" +
    "NODE1: NODE2,NODE3;\n" +
    "NODE3: NODE4;\n" +
    "NODE4: F;\n"

    var nonExisting = "" +
    "1: INPUT_HIGH;\n" +
    "2: OR;\n" +
    "3: OR;\n" +
    "4: NOT;\n" +
    "5: NOT;\n" +
    "1: 2;\n" +
    "2: 3;\n" +
    "3: 4;\n" +
    "5: 6;\n"

    func testBuilderReturnsCircuitWithCorrectAmountOfElements() {
        let test: [ElementBlueprint] = InputParser.parse(file: smallAdder)

        let (circuit, error) = CircuitBuilder(blueprints: test, interactingVia: TestInteractor.self).parse()

        assert(error == false)
        assert(circuit.elements.count == 11)
    }

    func testBuilderDetectsLoop() {
        let test: [ElementBlueprint] = InputParser.parse(file: loop)

        let (_, error) = CircuitBuilder(blueprints: test, interactingVia: TestInteractor.self).parse()

        assert(error == true)
        assert(TestInteractor.calledError == 1)
    }

    func testBuilderDetectsNotConnectedNodes() {
        let test: [ElementBlueprint] = InputParser.parse(file: notConnected)

        let (_, error) = CircuitBuilder(blueprints: test, interactingVia: TestInteractor.self).parse()

        assert(error == true)
        assert(TestInteractor.calledError == 1)
    }

    func testBuilderDetectsNotExistingNodes() {
        let test: [ElementBlueprint] = InputParser.parse(file: nonExisting)

        let (_, error) = CircuitBuilder(blueprints: test, interactingVia: TestInteractor.self).parse()

        assert(error == true)
        assert(TestInteractor.calledError == 1)
    }

}
