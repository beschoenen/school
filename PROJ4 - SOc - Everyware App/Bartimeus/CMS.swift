//
//  CMS.swift
//  Bartimeus
//
//  Created by Kevin Richter on 16/06/2017.
//  Copyright © 2017 Kevin Richter. All rights reserved.
//

import Foundation

class CMS {

    static let host: String = "https://cms.bartimeus.kevinrichter.nl"

    class func tours(callback: @escaping ([Tour]) -> Void) {
        URLSession.shared.dataTask(with: URL(string: "\(self.host)/api/tours")!) { data, _, _ in
            var tours: [Tour] = []

            do {
                let json = try JSONSerialization.jsonObject(with: data!) as! [[String: Any]]

                json.forEach { value in
                    tours.append(Tour(fromJson: value))
                }

                callback(tours)
            } catch {
                // Error
            }
        }.resume()
    }

    class func nodes(forTour tour: Int, callback: @escaping ([TourNode]) -> Void) {
        URLSession.shared.dataTask(with: URL(string: "\(self.host)/api/tours/\(tour)")!) { data, _, _ in
            var tourNodes: [TourNode] = []

            do {
                let json = try JSONSerialization.jsonObject(with: data!) as! [String: Any]

                let nodes = json["nodes"] as! [[String: Any]]

                nodes.forEach { value in
                    tourNodes.append(TourNode(fromJson: value))
                }

                callback(tourNodes)
            } catch {
                // Error
            }
        }.resume()
    }

}
