//
//  JsonParser.swift
//  Bartimeus
//
//  Created by mlcroivsvsa on 24/05/2017.
//  Copyright © 2017 Kevin Richter. All rights reserved.
//

import Foundation

class JsonParser {

    class func parseToDictionary(text: String) -> [String: Any]? {
        var returnValue: [String : Any]? = nil

        guard let data = text.data(using: .utf8) else { return returnValue }
        do {
            returnValue = try JSONSerialization.jsonObject(with: data, options: []) as? [String : Any]
        } catch {
            print(error.localizedDescription)
            return nil
        }
        return returnValue
    }
}
