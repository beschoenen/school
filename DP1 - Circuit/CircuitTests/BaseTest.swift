//
//  BaseTest.swift
//  Circuit
//
//  Created by Kevin Richter on 02/06/2017.
//  Copyright © 2017 Job & Kevin. All rights reserved.
//

import XCTest

class BaseTest: XCTestCase {

    override func setUp() {
        super.setUp()

        ElementFactory.register(element: NandGate(), as: "NAND")
        ElementFactory.register(element: OrGate(), as: "OR")
        ElementFactory.register(element: XorGate(), as: "XOR")
        ElementFactory.register(element: AndGate(), as: "AND")
        ElementFactory.register(element: Inverter(), as: "NOT")
        ElementFactory.register(element: InputNode(value: true), as: "INPUT_HIGH")
        ElementFactory.register(element: InputNode(value: false), as: "INPUT_LOW")
        ElementFactory.register(element: OutputNode(), as: "PROBE")
    }

    override func tearDown() {
        TestInteractor.calledError = 0
        TestInteractor.calledWarning = 0
    }

}
