//
//  TestInteractor.swift
//  Circuit
//
//  Created by Kevin Richter on 02/06/2017.
//  Copyright © 2017 Job & Kevin. All rights reserved.
//

import Foundation

class TestInteractor: InteractionProvider {

    static var calledError: Int = 0

    static var calledWarning: Int = 0

    static func chooseFile(inPath path: String) -> String? {
        return ""
    }

    static func toggleInput(nodes: [String]) -> Int? {
        return nil
    }

    static func show(warning message: String) {
        calledWarning += 1
        return
    }

    static func show(error message: String) {
        calledError += 1
        return
    }

}
