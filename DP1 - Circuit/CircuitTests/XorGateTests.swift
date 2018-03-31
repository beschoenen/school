//
//  CircuitTests.swift
//  CircuitTests
//
//  Created by Job Cuppen on 01/06/2017.
//  Copyright © 2017 Job & Kevin. All rights reserved.
//

import XCTest

class XorGateTests: BaseTest {

    var xorGate: XorGate!
    var input1: InputNode!
    var input2: InputNode!
    var output: OutputNode!

    override func setUp() {
        super.setUp()

        xorGate = ElementFactory.getCopyOf(type: "XOR") as! XorGate
        input1 = ElementFactory.getCopyOf(type: "INPUT_HIGH") as! InputNode
        input2 = ElementFactory.getCopyOf(type: "INPUT_LOW") as! InputNode
        output = ElementFactory.getCopyOf(type: "PROBE") as! OutputNode
    }

    func testAddPrevious() {
        xorGate.set(previous: input1)
        xorGate.set(previous: input2)

        let containsInput1 = xorGate.previous[0].name == input1.name
        let containsInput2 = xorGate.previous[1].name == input2.name

        assert(containsInput2 && containsInput1, "previous does not contain input1 and input2")
    }

    func testAddNext() {
        xorGate.set(next: output)

        let containsOutput = xorGate.next[0].name == output.name

        assert(containsOutput, "previous does not contain input1 and input2")
    }

}
