//
//  Album.swift
//  Spotify
//
//  Created by Kevin Richter on 14/03/2017.
//  Copyright © 2017 Kevin Richter. All rights reserved.
//

import Foundation

class Album {

    public let Id: String
    public let ArtistId: String

    public let Name: String
    public var ReleaseDate: Date? = nil

    public var Image: URL? = nil
    public var ImageBig: URL? = nil

    public let Link: URL

    public var Songs: [Song] = []

    init(fromJSON data: [String: Any], artistId: String) {
        self.Id = data["id"] as! String
        self.ArtistId = artistId

        self.Name = data["name"] as! String

        let images = data["images"] as! [[String: Any]]

        if images.count > 0 {
            self.Image = URL(string: images[images.count - 1]["url"] as! String)
        }

        if images.count > 1 {
            self.ImageBig = URL(string: images[images.count - 2]["url"] as! String)
        }

        let external = data["external_urls"] as! [String: Any]

        self.Link = URL(string: external["spotify"] as! String)!
    }

    public func downloadTracks(callback: @escaping () -> Void) {
        if self.Songs.count > 0 {
            callback()
            return
        }

        let task = URLSession.shared.dataTask(with: Spotify.album(albumId: self.Id)) { data, response, error in
            do {
                let json = try JSONSerialization.jsonObject(with: data!) as! [String: Any]

                let track_object = json["tracks"] as! [String: Any]

                let songs = track_object["items"] as! [[String: Any]]

                songs.forEach { value in
                    self.Songs.append(Song(fromJson: value, artistId: self.ArtistId, albumId: self.Id))
                }

                DispatchQueue.main.async {
                    callback()
                }
            } catch {
                // Error
            }
        }
        
        task.resume()

    }

}
