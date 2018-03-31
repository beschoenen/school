//
//  TopTracksViewController.swift
//  Spotify
//
//  Created by Rogier Bazelmans on 12/04/2017.
//  Copyright © 2017 Kevin Richter. All rights reserved.
//

import UIKit

class TopTracksViewController: UITableViewController {

    var Artist: Artist?

    @IBOutlet weak var SongsTableView: UITableView!
    
    override func viewDidLoad() {
        super.viewDidLoad()

        self.SongsTableView.delegate = self
        self.SongsTableView.dataSource = self

        self.Artist?.downloadTopSongs() {
            self.SongsTableView.reloadData()
        }
    }

    override func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        return self.Artist?.TopSongs.count ?? 0
    }

    override func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        let cell = tableView.dequeueReusableCell(withIdentifier: "TrackCell", for: indexPath)

        let song = Artist?.TopSongs[indexPath.row]

        if song != nil {
            cell.textLabel?.text = "\(song!.Number). \(song!.Name)"
            cell.detailTextLabel?.text = song!.Runtime
        }

        return cell
    }

}
