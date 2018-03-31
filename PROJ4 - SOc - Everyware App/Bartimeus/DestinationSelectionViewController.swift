//
//  DestinationSelectionViewController.swift
//  Bartimeus
//
//  Created by Uitleen on 17/05/2017.
//  Copyright © 2017 Kevin Richter. All rights reserved.
//

import Foundation

class DestinationSelectionViewController: UITableViewController, UISearchResultsUpdating {

    var navigator: Navigator!

    private var filteredBeacons: [String : String] = [:]
    private var allBeacons: [String : String] = [:]

    let searchController = UISearchController(searchResultsController: nil)

    override func viewDidLoad() {
        super.viewDidLoad()

        searchController.searchResultsUpdater = self
        searchController.hidesNavigationBarDuringPresentation = false
        searchController.dimsBackgroundDuringPresentation = false
        searchController.searchBar.sizeToFit()

        self.tableView.tableHeaderView = searchController.searchBar
        self.title = self.navigator.routeSelectionTitle

        self.navigator.routeList { beacons in
            self.allBeacons = beacons
            self.filteredBeacons = self.allBeacons

            DispatchQueue.main.async {
                self.tableView.reloadData()
            }
        }
    }

    override func tableView(_ beaconList: UITableView, numberOfRowsInSection section: Int) -> Int {
        return self.filteredBeacons.count
    }

    override func tableView(_ beaconList: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        let cell = beaconList.dequeueReusableCell(withIdentifier: "BeaconCell", for: indexPath)
        let beacon = Array(self.filteredBeacons.values)[indexPath.row]

        cell.textLabel?.text = beacon

        return cell
    }

    override func shouldPerformSegue(withIdentifier identifier: String, sender: Any?) -> Bool {
        let beacon = self.allBeacons.first { beacon in
            return beacon.value == (sender as? UITableViewCell)?.textLabel?.text
        }

        self.navigator.select(route: beacon!.key)

        return self.navigator.canNavigate
    }

    override func prepare(for segue: UIStoryboardSegue, sender: Any?) {
        if let navigationController = segue.destination as? NavigationViewController {
            self.searchController.isActive = false
            navigationController.navigator = self.navigator
        }
    }

    func updateSearchResults(for searchController: UISearchController) {
        if (searchController.searchBar.text?.isEmpty)! {
            self.filteredBeacons = allBeacons
            return self.tableView.reloadData()
        }

        self.filteredBeacons = [:]
        self.allBeacons.forEach { beacon in
            if beacon.value.localizedCaseInsensitiveContains(searchController.searchBar.text!) {
                self.filteredBeacons[beacon.key] = beacon.value
            }
        }

        self.tableView.reloadData()
    }
}
