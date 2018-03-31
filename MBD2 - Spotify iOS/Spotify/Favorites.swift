//
//  Favorites.swift
//  Spotify
//
//  Created by Kevin Richter on 14/03/2017.
//  Copyright © 2017 Kevin Richter. All rights reserved.
//

import Foundation

class Favorites {

    static let sharedInstance = Favorites()

    public var Artists: [Artist] = []

    private init() {
        if let data = UserDefaults.standard.object(forKey: "nl.kevin.spotify.artists") as? [Data] {
            data.forEach { data in
                if let t = NSKeyedUnarchiver.unarchiveObject(with: data) {
                    Artists.append(t as! Artist)
                }
            }
        }
    }

    public func toggle(artist: Artist) {
        let index = Artists.index(where: { $0.Id == artist.Id })

        if index == nil {
            Artists.append(artist)
        } else {
            Artists.remove(at: index!)
        }

        self.save()
    }

    public func remove(at index: Int) {
        Artists.remove(at: index)
    }

    public func contains(artist: Artist) -> Bool {
        return Artists.index(where: { $0.Id == artist.Id }) != nil
    }

    public func save() {
        let artistsData = self.Artists.map { artist in
            return NSKeyedArchiver.archivedData(withRootObject: artist)
        }

        UserDefaults.standard.set(artistsData, forKey: "nl.kevin.spotify.artists")
        UserDefaults.standard.synchronize()
    }

}
