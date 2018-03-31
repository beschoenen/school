//
//  InteractionProvider.swift
//  Circuit
//
//  Created by Kevin Richter on 11/05/2017.
//  Copyright © 2017 Job & Kevin. All rights reserved.
//

import Foundation

protocol InteractionProvider {

    static func chooseFile(inPath path: String) -> String?

    static func toggleInput(nodes: [String]) -> Int?

    static func show(warning message: String)

    static func show(error message: String)

}

extension InteractionProvider {

    //

}
