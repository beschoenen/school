//
//  CircuitTests.swift
//  CircuitTests
//
//  Created by Job Cuppen on 01/06/2017.
//  Copyright © 2017 Job & Kevin. All rights reserved.
//

import XCTest

class OrGateTests: BaseTest {

    var orGate: OrGate!
    var input1: InputNode!
    var input2: InputNode!
    var output: OutputNode!
    
    override func setUp() {
        super.setUp()

        orGate = ElementFactory.getCopyOf(type: "OR") as! OrGate
        input1 = ElementFactory.getCopyOf(type: "INPUT_HIGH") as! InputNode
        input2 = ElementFactory.getCopyOf(type: "INPUT_LOW") as! InputNode
        output = ElementFactory.getCopyOf(type: "PROBE") as! OutputNode
    }
    
    func testAddPrevious() {
        orGate.set(previous: input1)
        orGate.set(previous: input2)

        let containsInput1 = orGate.previous[0].name == input1.name
        let containsInput2 = orGate.previous[1].name == input2.name

        assert(containsInput2 && containsInput1, "previous does not contain input1 and input2")
    }

    func testAddNext() {
        orGate.set(next: output)

        let containsOutput = orGate.next[0].name == output.name

        assert(containsOutput, "previous does not contain input1 and input2")
    }

}
