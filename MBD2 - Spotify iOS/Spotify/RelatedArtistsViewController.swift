//
//  RelatedArtistsViewController.swift
//  Spotify
//
//  Created by Rogier Bazelmans on 12/04/2017.
//  Copyright © 2017 Kevin Richter. All rights reserved.
//

import UIKit

class RelatedArtistsViewController: UITableViewController {

    var Artist: Artist?

    @IBOutlet weak var ArtistsTableView: UITableView!

    override func viewDidLoad() {
        super.viewDidLoad()

        self.Artist?.downloadRelatedArtists() {
            self.ArtistsTableView.reloadData()
        }
    }

    override func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        return self.Artist?.RelatedArtists.count ?? 0
    }

    override func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        let cell = tableView.dequeueReusableCell(withIdentifier: "ArtistCell", for: indexPath)

        cell.textLabel?.text = Artist?.RelatedArtists[indexPath.row].Name

        if Artist?.RelatedArtists[indexPath.row].Image != nil {
            cell.imageView?.downloadFrom(url: Artist?.RelatedArtists[indexPath.row].Image)
        }

        return cell
    }

    override func prepare(for segue: UIStoryboardSegue, sender: Any?) {
        if let artistViewController = segue.destination as? ArtistViewController {
            artistViewController.Artist = Artist?.RelatedArtists[self.tableView.indexPathForSelectedRow!.row]
        }
    }
}
