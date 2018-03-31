//
//  FavoritesViewController.swift
//  Spotify
//
//  Created by Kevin Richter on 14/03/2017.
//  Copyright © 2017 Kevin Richter. All rights reserved.
//

import UIKit

class FavoritesViewController: UITableViewController {

    let Favorite = Favorites.sharedInstance

    override func viewDidAppear(_ animated: Bool) {
        super.viewDidAppear(animated)

        self.tableView.reloadData()
    }

    @IBAction func EditButtonClicked(_ sender: Any) {
        self.tableView.setEditing(!self.tableView.isEditing, animated: true)
    }

    override func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        return Favorite.Artists.count
    }

    override func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        let cell = tableView.dequeueReusableCell(withIdentifier: "ArtistCell", for: indexPath)

        cell.textLabel?.text = Favorite.Artists[indexPath.row].Name

        if Favorite.Artists[indexPath.row].Image != nil {
            cell.imageView?.downloadFrom(url: Favorite.Artists[indexPath.row].Image)
        }

        return cell
    }

    override func tableView(_ tableView: UITableView, commit editingStyle: UITableViewCellEditingStyle, forRowAt indexPath: IndexPath) {
        tableView.beginUpdates()

        tableView.deleteRows(at: [indexPath], with: .automatic)
        Favorite.remove(at: indexPath.row)

        tableView.endUpdates()

        Favorite.save()
    }

    override func prepare(for segue: UIStoryboardSegue, sender: Any?) {
        if let artistViewController = segue.destination as? ArtistViewController {
            artistViewController.Artist = Favorite.Artists[self.tableView.indexPathForSelectedRow!.row]
        }
    }

}
