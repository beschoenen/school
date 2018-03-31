//
//  InputParserTests.swift
//  Circuit
//
//  Created by Job Cuppen on 02/06/2017.
//  Copyright © 2017 Job & Kevin. All rights reserved.
//

import XCTest

class InputParserTests: BaseTest {

    var input = "" +
    "A: INPUT_HIGH;\n" +
    "B: INPUT_HIGH;\n" +
    "Cin: INPUT_LOW;\n" +
    "Cout: PROBE;\n" +
    "S: PROBE;\n" +
    "NODE1: OR;\n" +
    "NODE2: AND;\n" +
    "NODE3: AND;\n" +
    "NODE4: NOT;\n" +
    "NODE5: AND;\n" +
    "NODE6: OR;\n" +
    "NODE7: NOT;\n" +
    "NODE8: NOT;\n" +
    "NODE9: AND;\n" +
    "NODE10: AND;\n" +
    "NODE11: OR;\n" +

    "Cin: NODE3,NODE7,NODE10;\n" +
    "A: NODE1,NODE2;\n" +
    "B: NODE1,NODE2;\n" +
    "NODE1: NODE3,NODE5;\n" +
    "NODE2: NODE4,NODE6;\n" +
    "NODE3: NODE6;\n" +
    "NODE4: NODE5;\n" +
    "NODE5: NODE8,NODE9;\n" +
    "NODE6: Cout;\n" +
    "NODE7: NODE9;\n" +
    "NODE8: NODE10;\n" +
    "NODE9: NODE11;\n" +
    "NODE10: NODE11;\n" +
    "NODE11: S;\n"

    func testExample() {
        let test: [ElementBlueprint] = InputParser.parse(file: input)

        assert(test[0].name == "A" && test[0].type == "INPUT_HIGH")
        assert(test[1].name == "B" && test[1].type == "INPUT_HIGH")
        assert(test[2].name == "Cin" && test[2].type == "INPUT_LOW")
        assert(test[3].name == "Cout" && test[3].type == "PROBE")
        assert(test[4].name == "S" && test[4].type == "PROBE")
        assert(test[5].name == "NODE1" && test[5].type == "OR")
        assert(test[6].name == "NODE2" && test[6].type == "AND")
        assert(test[7].name == "NODE3" && test[7].type == "AND")
        assert(test[8].name == "NODE4" && test[8].type == "NOT")
        assert(test[9].name == "NODE5" && test[9].type == "AND")
        assert(test[10].name == "NODE6" && test[10].type == "OR")
        assert(test[11].name == "NODE7" && test[11].type == "NOT")
        assert(test[12].name == "NODE8" && test[12].type == "NOT")
        assert(test[13].name == "NODE9" && test[13].type == "AND")
        assert(test[14].name == "NODE10" && test[14].type == "AND")
        assert(test[15].name == "NODE11" && test[15].type == "OR")
    }

}
