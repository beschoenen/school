//
//  ArtistMoreInfoViewController.swift
//  Spotify
//
//  Created by Rogier Bazelmans on 12/04/2017.
//  Copyright © 2017 Kevin Richter. All rights reserved.
//

import UIKit

class ArtistMoreInfoViewController: UIViewController {

    var Artist: Artist?

    @IBOutlet weak var lbFollowers: UILabel!
    @IBOutlet weak var lbPopularity: UILabel!
    @IBOutlet weak var lbGenres: UILabel!

    override func viewDidLoad() {
        super.viewDidLoad()

        let followers = Artist?.Followers
        let popularity = Artist?.Popularity
        let genres = Artist?.Genres
        let totalFollowers = String(format:"%.1f", followers!)
        let totalPopularity = String(format:"%.1f", popularity!)
        var totalGenres = ""

        var first = true
        for genre in genres!{
            if first{
                totalGenres = totalGenres + genre
                first = false
            }
            else{
                totalGenres = totalGenres + ", \(genre)"
            }
        }

        lbFollowers.text = "Followers: \(totalFollowers)"
        lbPopularity.text = "Popularity: \(totalPopularity)"
        lbGenres.text = "Genres: \(totalGenres)"
    }

    override func prepare(for segue: UIStoryboardSegue, sender: Any?) {
        if let topTracksViewController = segue.destination as? TopTracksViewController {
            topTracksViewController.Artist = Artist
        }

        if let relArtist = segue.destination as? RelatedArtistsViewController{
            relArtist.Artist = Artist
        }
    }
}
