//
//  AlbumViewController.swift
//  Spotify
//
//  Created by Kevin Richter on 14/03/2017.
//  Copyright © 2017 Kevin Richter. All rights reserved.
//

import UIKit

class AlbumViewController: UIViewController, UITableViewDelegate, UITableViewDataSource {

    @IBOutlet weak var AlbumImageView: UIImageView!
    @IBOutlet weak var TrackTableView: UITableView!

    var Album: Album?

    override func viewDidLoad() {
        super.viewDidLoad()

        self.TrackTableView.delegate = self
        self.TrackTableView.dataSource = self

        if Album == nil {
            return
        }

        self.title = Album?.Name

        self.AlbumImageView.downloadFrom(url: self.Album?.ImageBig ?? self.Album?.Image)

        self.Album?.downloadTracks() {
            self.TrackTableView.reloadData()
        }
    }

    func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        return self.Album?.Songs.count ?? 0
    }

    func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        let cell = tableView.dequeueReusableCell(withIdentifier: "TrackCell", for: indexPath)

        let song = Album?.Songs[indexPath.row]

        if song != nil {
            cell.textLabel?.text = "\(song!.Number). \(song!.Name)"
            cell.detailTextLabel?.text = song!.Runtime
        }

        return cell
    }

}
