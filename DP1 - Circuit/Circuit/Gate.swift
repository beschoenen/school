//
//  Gate.swift
//  Circuit
//
//  Created by Kevin Richter on 11/05/2017.
//  Copyright © 2017 Job & Kevin. All rights reserved.
//

import Foundation

protocol Gate: Element {

    //

}

extension Gate {

    func isValid() -> (Enums.validation, String?) {
        if self.name == nil {
            return (.Error, "Gate has no name")
        }

        if self.previous.count < 2 {
            return (.Error, "Gate \"\(self.name!)\" has too few inputs")
        }

        if self.next.count == 0 {
            return (.Warning, "Gate \"\(self.name!)\" is not connected to anything")
        }

        return (.Valid, nil)
    }

}
