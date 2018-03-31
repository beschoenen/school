//
//  CircuitTests.swift
//  CircuitTests
//
//  Created by Job Cuppen on 01/06/2017.
//  Copyright © 2017 Job & Kevin. All rights reserved.
//

import XCTest

class NandGateTests: BaseTest {

    var nandGate: NandGate!
    var input1: InputNode!
    var input2: InputNode!
    var output: OutputNode!
    
    override func setUp() {
        super.setUp()

        self.nandGate = ElementFactory.getCopyOf(type: "NAND") as! NandGate
        self.input1 = ElementFactory.getCopyOf(type: "INPUT_HIGH") as! InputNode
        self.input2 = ElementFactory.getCopyOf(type: "INPUT_LOW") as! InputNode
        self.output = ElementFactory.getCopyOf(type: "PROBE") as! OutputNode
    }
    
    func testAddPrevious() {
        nandGate.set(previous: input1)
        nandGate.set(previous: input2)

        let containsInput1 = nandGate.previous[0].name == input1.name
        let containsInput2 = nandGate.previous[1].name == input2.name

        assert(containsInput2 && containsInput1, "previous does not contain input1 and input2")
    }

    func testAddNext() {
        nandGate.set(next: output)

        let containsOutput = nandGate.next[0].name == output.name

        assert(containsOutput, "previous does not contain input1 and input2")
    }

    func testGateReturnsCorrectValue() {
        nandGate.previous.append(ElementFactory.getCopyOf(type: "NAND"))
    }
    
}
