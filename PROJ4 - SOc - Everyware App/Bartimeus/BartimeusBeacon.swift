//
//  BartimeusBeacon.swift
//  Bartimeus
//
//  Created by Job Cuppen on 13/06/2017.
//  Copyright © 2017 Kevin Richter. All rights reserved.
//

import Foundation

class BartimeusBeacon {
    let movinBeacon: MovinBeacon?
    let contextObject: ContextObject?

    init?(withMovinBeacon beacon: MovinBeacon?, andJson json: [String : Any]?) {
        self.movinBeacon = beacon
        self.contextObject = ContextObject(with: json)
    }
}
