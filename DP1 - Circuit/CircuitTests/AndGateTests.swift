//
//  CircuitTests.swift
//  CircuitTests
//
//  Created by Job Cuppen on 01/06/2017.
//  Copyright © 2017 Job & Kevin. All rights reserved.
//

import XCTest

class AndGateTests: BaseTest {

    var andGate: AndGate!
    var input1: InputNode!
    var input2: InputNode!
    var output: OutputNode!
    
    override func setUp() {
        super.setUp()

        andGate = ElementFactory.getCopyOf(type: "AND") as! AndGate
        input1 = ElementFactory.getCopyOf(type: "INPUT_HIGH") as! InputNode
        input2 = ElementFactory.getCopyOf(type: "INPUT_LOW") as! InputNode
        output = ElementFactory.getCopyOf(type: "PROBE") as! OutputNode
    }
    
    func testAddPrevious() {
        andGate.set(previous: input1)
        andGate.set(previous: input2)

        let containsInput1 = andGate.previous[0].name == input1.name
        let containsInput2 = andGate.previous[1].name == input2.name

        assert(containsInput2 && containsInput1, "previous does not contain input1 and input2")
    }

    func testAddNext() {
        andGate.set(next: output)

        let containsOutput = andGate.next[0].name == output.name

        assert(containsOutput, "previous does not contain input1 and input2")
    }

}
