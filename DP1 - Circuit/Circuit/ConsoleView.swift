//
//  ConsoleView.swift
//  Circuit
//
//  Created by Kevin Richter on 11/05/2017.
//  Copyright © 2017 Job & Kevin. All rights reserved.
//

import Foundation

class ConsoleView: ViewProvider {

    static func printView(inputNodes: [InputNode], outputNodes: [OutputNode]) {
        print("\u{001B}[2J")

        var table = TextTable(columns: [
            TextTableColumn(header: "inputs"),
            TextTableColumn(header: ""),
            TextTableColumn(header: "outputs"),
            TextTableColumn(header: ""),
        ])

        let max = [inputNodes.count, outputNodes.count].max()!
        (0..<max).forEach { index in
            var row: [String] = []

            if inputNodes.indices.contains(index) {
                row.append(inputNodes[index].name!)
                row.append(inputNodes[index].getValue() ? "1" : "0")
            }

            if outputNodes.indices.contains(index) {
                row.append(outputNodes[index].name!)
                row.append(outputNodes[index].getValue() ? "1" : "0")
            }

            table.addRow(values: row)
        }

        print(table.render())
    }

}
