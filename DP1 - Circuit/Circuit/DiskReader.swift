//
//  DiskReader.swift
//  Circuit
//
//  Created by Kevin Richter on 11/05/2017.
//  Copyright © 2017 Job & Kevin. All rights reserved.
//

import Foundation

class DiskReader: FileProvider {

    static func listFiles(inPath path: String = ".") -> [String] {
        let pathURL = URL(fileURLWithPath: path, isDirectory: true)
        let fileManager = FileManager.default

        var allFiles: [String] = []

        do {
            let enumerator = try fileManager.contentsOfDirectory(atPath: path)

            for file in enumerator {
                if let path = NSURL(fileURLWithPath: file, relativeTo: pathURL as URL).path, path.hasSuffix(".txt") {
                    allFiles.append(path)
                }
            }
        } catch {
            //
        }

        return allFiles
    }

    static func readFile(path: String) -> String? {
        return try? String(contentsOfFile: path, encoding: .utf8)
    }

}
