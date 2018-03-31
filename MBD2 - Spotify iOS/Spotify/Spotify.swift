//
//  Spotify.swift
//  Spotify
//
//  Created by Kevin Richter on 14/03/2017.
//  Copyright © 2017 Kevin Richter. All rights reserved.
//

import Foundation

class Spotify {

    static func search(artist: String) -> URL {
        return URL(string: "https://api.spotify.com/v1/search?query=\(artist.replacingOccurrences(of: " ", with: "+"))&type=artist&offset=0&limit=20")!
    }

    static func artist(artistId: String) -> URL {
        return URL(string: "https://api.spotify.com/v1/artists/\(artistId)")!
    }

    static func albums(artistId: String) -> URL {
        return URL(string: "https://api.spotify.com/v1/artists/\(artistId)/albums")!
    }

    static func album(albumId: String) -> URL {
        return URL(string: "https://api.spotify.com/v1/albums/\(albumId)")!
    }

    static func topSongs(artistId: String) -> URL {
        return URL(string: "https://api.spotify.com/v1/artists/\(artistId)/top-tracks?country=NL")!
    }

    static func relatedArtists(artistId: String) -> URL {
        return URL(string: "https://api.spotify.com/v1/artists/\(artistId)/related-artists")!
    }

}
