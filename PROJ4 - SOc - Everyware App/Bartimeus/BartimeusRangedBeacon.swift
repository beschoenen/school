//
//  BartimeusRangedBeacon.swift
//  Bartimeus
//
//  Created by mlcroivsvsa on 24/05/2017.
//  Copyright © 2017 Kevin Richter. All rights reserved.
//

import Foundation

class BartimeusRangedBeacon {
    let movinRangedBeacon: MovinRangedBeacon?
    let contextObject: ContextObject?

    init?(withMovinRangedBeacon beacon: MovinRangedBeacon?, andJson json: [String : Any]?) {
        self.movinRangedBeacon = beacon
        self.contextObject = ContextObject(with: json)
    }
}
