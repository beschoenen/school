//
//  Song.swift
//  Spotify
//
//  Created by Kevin Richter on 14/03/2017.
//  Copyright © 2017 Kevin Richter. All rights reserved.
//

import Foundation
import AVFoundation

class Song {

    public let Id: String
    public let ArtistId: String
    public let AlbumId: String

    public let Number: Int

    public let Name: String

    public let Runtime: String

    public let Link: URL

    init(fromJson data: [String: Any], artistId: String, albumId: String) {
        self.Id = data["id"] as! String
        self.ArtistId = artistId
        self.AlbumId = albumId

        self.Number = data["track_number"] as! Int
        self.Name = data["name"] as! String

        self.Runtime = (data["duration_ms"] as! Int).TimeString

        let external = data["external_urls"] as! [String: Any]

        self.Link = URL(string: external["spotify"] as! String)!
    }

}
