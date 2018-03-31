//
//  Artist.swift
//  Spotify
//
//  Created by Kevin Richter on 14/03/2017.
//  Copyright © 2017 Kevin Richter. All rights reserved.
//

import Foundation

class Artist: NSObject, NSCoding {

    public let Id: String

    public let Name: String

    public var Image: URL? = nil

    public var ImageBig: URL? = nil

    public let Link: URL

    public var Albums: [Album] = []

    public var Followers: Double

    public let Popularity: Double

    public let Genres: [String]

    public var TopSongs: [Song] = []

    public var RelatedArtists: [Artist] = []

    init(fromJSON data: [String: Any]) {
        self.Id = data["id"] as! String
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

        self.Popularity = data["popularity"] as! Double

        self.Followers = 0

        if let FollowersObject = data["followers"] as? [String: Any]{
            self.Followers = (FollowersObject["total"] as? Double)!
        }

        self.Genres = data["genres"] as! [String]
    }

    init(Id: String, Name: String, Image: URL?, Link: URL, Followers: Double, Popularity: Double, Genres: [String]) {
        self.Id = Id
        self.Name = Name
        self.Image = Image
        self.Link = Link
        self.Followers = Followers
        self.Popularity = Popularity
        self.Genres = Genres
    }

    required convenience init?(coder aDecoder: NSCoder) {
        guard

        let id = aDecoder.decodeObject(forKey: "id") as? String,
        let name = aDecoder.decodeObject(forKey: "name") as? String,
        let image = aDecoder.decodeObject(forKey: "image") as? URL,
        let link = aDecoder.decodeObject(forKey: "link") as? URL,
        let followers = aDecoder.decodeObject(forKey: "total") as? Double,
        let popularity = aDecoder.decodeObject(forKey: "popularity") as? Double,
        let genres = aDecoder.decodeObject(forKey: "genres") as? [String]

        else { return nil }

        self.init(
            Id: id,
            Name: name,
            Image: image,
            Link: link,
            Followers: followers,
            Popularity: popularity,
            Genres: genres
        )
    }

    func encode(with aCoder: NSCoder) {
        aCoder.encode(self.Id, forKey: "id")
        aCoder.encode(self.Name, forKey: "name")
        aCoder.encode(self.Image, forKey: "image")
        aCoder.encode(self.Link, forKey: "link")
    }

    public func downloadAlbums(callback: @escaping () -> Void) {
        if self.Albums.count > 0 {
            callback()
            return
        }

        let task = URLSession.shared.dataTask(with: Spotify.albums(artistId: self.Id)) { data, response, error in
            do {
                let json = try JSONSerialization.jsonObject(with: data!) as! [String: Any]
                let albums = json["items"] as! [[String: Any]]

                albums.forEach { value in
                    self.Albums.append(Album(fromJSON: value, artistId: self.Id))
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

    public func downloadTopSongs(callback: @escaping () -> Void) {
        if self.TopSongs.count > 0 {
            callback()
            return
        }

        let task = URLSession.shared.dataTask(with: Spotify.topSongs(artistId: self.Id)) { data, response, error in
            do {
                let json = try JSONSerialization.jsonObject(with: data!) as! [String: Any]
                let track_object = json["tracks"] as! [[String: Any]]

                track_object.forEach { value in
                    self.TopSongs.append(Song(fromJson: value, artistId: self.Id, albumId: ""))
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

    public func downloadRelatedArtists(callback: @escaping () -> Void) {
        if self.RelatedArtists.count > 0 {
            callback()
            return
        }

        let task = URLSession.shared.dataTask(with: Spotify.relatedArtists(artistId: self.Id)) { data, response, error in
            do {
                let json = try JSONSerialization.jsonObject(with: data!) as! [String: Any]
                let info = json["artists"] as! [[String: Any]]

                info.forEach { value in
                    self.RelatedArtists.append(Artist(fromJSON: value))
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
