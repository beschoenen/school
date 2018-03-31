//
//  Int.swift
//  Spotify
//
//  Created by Kevin Richter on 15/03/2017.
//  Copyright © 2017 Kevin Richter. All rights reserved.
//

import Foundation

extension Int {

    var TimeString: String {
        return String(format: "%d:%02d", minute, second)
    }

    var minute: Int {
        return Int(self / 60000)
    }

    var second: Int {
        return Int(self % 60000 / 1000)
    }
}
