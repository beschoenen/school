//
//  BaseGateTests.swift
//  Circuit
//
//  Created by Kevin Richter on 02/06/2017.
//  Copyright © 2017 Job & Kevin. All rights reserved.
//

import XCTest

class BaseGateTests: BaseTest {

    var nandGate: NandGate! = nil

    override func setUp() {
        super.setUp()

        self.nandGate = ElementFactory.getCopyOf(type: "NAND") as! NandGate
    }

    func testGateIsValidGivesErrorWhenNoName() {
        assert(nandGate.isValid().0 == Enums.validation.Error)
    }

    func testGateIsValidGivesErrorWhenNotConnectedToAnything() {
        nandGate.name = "Test"
        assert(nandGate.isValid().0 == Enums.validation.Error)
    }

    func testGateIsValidGiveWarning() {
        nandGate.name = "Test"
        nandGate.set(previous: ElementFactory.getCopyOf(type: "AND") as! AndGate)
        nandGate.set(previous: ElementFactory.getCopyOf(type: "AND") as! AndGate)

        assert(nandGate.isValid().0 == Enums.validation.Warning)
    }

}
