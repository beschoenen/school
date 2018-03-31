//
//  Direction.swift
//  Bartimeus
//
//  Created by Kevin Richter on 17/06/2017.
//  Copyright © 2017 Kevin Richter. All rights reserved.
//

import Foundation

class Direction {

    enum Direction: Int {
        case back = -1
        case north = 0
        case northEast = 1
        case east = 2
        case southEast = 3
        case south = 4
        case southWest = 5
        case west = 6
        case northWest = 7
    }

    class func from(enum direction: Direction) -> String {
        return self.from(number: direction.rawValue)
    }

    class func from(number: Int) -> String {
        switch number {
        case -1:
            return "↷︎"
        case 0:
            return "↑︎"
        case 1:
            return "↗︎"
        case 2:
            return "↱︎"
        case 3:
            return "↘︎"
        case 4:
            return "↓︎"
        case 5:
            return "↙︎"
        case 6:
            return "↰︎"
        case 7:
            return "↖︎"
        default:
            return "○︎"
        }
    }

}
