//
//  SearchViewController.swift
//  Spotify
//
//  Created by Kevin Richter on 14/03/2017.
//  Copyright © 2017 Kevin Richter. All rights reserved.
//

import UIKit

class SearchViewController: UITableViewController, UISearchBarDelegate {

    @IBOutlet weak var SearchBar: UISearchBar!

    var SearchResults: [Artist] = []

    override func viewDidLoad() {
        super.viewDidLoad()

        self.SearchBar.delegate = self
    }

    func searchBarSearchButtonClicked(_ searchBar: UISearchBar) {
        SearchResults.removeAll()
        self.tableView.reloadData()

        searchBar.endEditing(true)

        let task = URLSession.shared.dataTask(with: Spotify.search(artist: searchBar.text ?? "")) { data, response, error in
            do {
                let json = try JSONSerialization.jsonObject(with: data!) as! [String: Any]
                let info = json["artists"] as! [String: Any]

                let artists = info["items"] as! [[String: Any]]

                artists.forEach { value in
                    self.SearchResults.append(Artist(fromJSON: value))
                }
            } catch {
                // Error
            }

            DispatchQueue.main.async {
                self.tableView.reloadData()
            }
        }

        task.resume()
    }

    override func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        return SearchResults.count
    }

    override func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        let cell = tableView.dequeueReusableCell(withIdentifier: "ArtistCell", for: indexPath)

        cell.textLabel?.text = SearchResults[indexPath.row].Name

        if SearchResults[indexPath.row].Image != nil {
            cell.imageView?.downloadFrom(url: SearchResults[indexPath.row].Image)
        }

        return cell
    }

    override func prepare(for segue: UIStoryboardSegue, sender: Any?) {
        if let artistViewController = segue.destination as? ArtistViewController {
            artistViewController.Artist = SearchResults[self.tableView.indexPathForSelectedRow!.row]
        }
    }
    
}

