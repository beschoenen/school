//
//  ViewController.swift
//  Spotify
//
//  Created by Kevin Richter on 14/03/2017.
//  Copyright © 2017 Kevin Richter. All rights reserved.
//

import UIKit

class ArtistViewController: UIViewController, UITableViewDelegate, UITableViewDataSource {

    @IBOutlet weak var ArtistImage: UIImageView!
    @IBOutlet weak var AlbumTableView: UITableView!
    @IBOutlet weak var FavoriteButton: UIButton!

    var Artist: Artist?

    override func viewDidLoad() {
        super.viewDidLoad()

        self.AlbumTableView.delegate = self
        self.AlbumTableView.dataSource = self

        if Artist == nil {
            return
        }

        self.title = Artist?.Name

        self.ArtistImage.downloadFrom(url: self.Artist?.ImageBig ?? self.Artist?.Image)

        self.Artist?.downloadAlbums() {
            self.AlbumTableView.reloadData()
        }

        self.setFavoriteButtonText()
    }

    @IBAction func FavoriteButtonClicked(_ sender: Any) {
        Favorites.sharedInstance.toggle(artist: self.Artist!)
        setFavoriteButtonText()
    }

    func setFavoriteButtonText() {
        if Favorites.sharedInstance.contains(artist: self.Artist!) {
            FavoriteButton.setTitle("Unfavorite", for: .normal)
        } else {
            FavoriteButton.setTitle("Favorite", for: .normal)
        }
    }

    func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        return self.Artist?.Albums.count ?? 0
    }

    func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        let cell = tableView.dequeueReusableCell(withIdentifier: "AlbumCell", for: indexPath)

        cell.textLabel?.text = Artist?.Albums[indexPath.row].Name

        if Artist?.Albums[indexPath.row].Image != nil {
            cell.imageView?.downloadFrom(url: Artist?.Albums[indexPath.row].Image)
        }

        return cell
    }

    override func prepare(for segue: UIStoryboardSegue, sender: Any?) {
        if let albumViewController = segue.destination as? AlbumViewController {
            albumViewController.Album = self.Artist?.Albums[self.AlbumTableView.indexPathForSelectedRow!.row]
        }

        if let moreinfo = segue.destination as? ArtistMoreInfoViewController{
            moreinfo.Artist = Artist
        }
    }

}

