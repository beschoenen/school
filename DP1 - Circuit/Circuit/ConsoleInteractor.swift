//
//  ConsoleInteractor.swift
//  Circuit
//
//  Created by Kevin Richter on 11/05/2017.
//  Copyright © 2017 Job & Kevin. All rights reserved.
//

import Foundation

class ConsoleInteractor: InteractionProvider {

    static func chooseFile(inPath path: String = ".") -> String? {
        print("Choose a file to start the simulation")

        let files = DiskReader.listFiles(inPath: path)

        if files.count < 1 {
            print("There are no files in the directory")
            return nil
        }

        let filenames = files.map { file in
            return NSURL(fileURLWithPath: file).lastPathComponent!
        }

        let choosenFile = pickFrom(list: filenames, withPrompt: "Please choose a file from the list")

        if choosenFile == nil {
            return nil
        }

        return files[choosenFile!]
    }

    static func toggleInput(nodes: [String]) -> Int? {
        return pickFrom(list: nodes)
    }

    static func askFor(input: String) -> String? {
        print("\(input): ")

        return readLine()
    }

    static func pickFrom(list: [String], withPrompt prompt: String = "Please choose an item from the list") -> Int? {
        for (index, item) in list.enumerated() {
            print("\(index + 1). \(item)")
        }

        var choosenFile: Int? = nil

        while choosenFile == nil {
            print("\(prompt), press \"c\" to cancel")

            print("-> ", terminator: "")

            let input = readLine()

            if String(describing: input!).lowercased() == "c" {
                return nil
            }

            if let int = Int(input!) {
                if (1...list.count).contains(int) {
                    choosenFile = int - 1
                } else {
                    show(warning: "This number is invalid")
                }
            } else {
                show(warning: "Please try again")
            }
        }

        return choosenFile
    }

    static func show(warning message: String) {
        print("\u{001B}[0;33mWarning: \(message)!")
    }

    static func show(error message: String) {
        print("\u{001B}[0;31mError: \(message)!")
    }

}
