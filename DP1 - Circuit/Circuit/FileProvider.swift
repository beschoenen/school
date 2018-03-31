//
//  FileProvider.swift
//  Circuit
//
//  Created by Kevin Richter on 11/05/2017.
//  Copyright © 2017 Job & Kevin. All rights reserved.
//

import Foundation

protocol FileProvider {

    static func listFiles(inPath path: String) -> [String]

    static func readFile(path: String) -> String?

}

extension FileProvider {

    //

}
