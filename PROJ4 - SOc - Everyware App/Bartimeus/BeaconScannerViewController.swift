//
//  BeaconScannerViewController.swift
//  Bartimeus
//
//  Created by Kevin Richter on 20/03/2017.
//  Copyright © 2017 Kevin Richter. All rights reserved.
//

import UIKit

class BeaconScannerViewController: UIViewController, UITableViewDelegate,
                                   UITableViewDataSource, NearestBeaconNotifier, InRangeBeaconsNotifier {

    @IBOutlet weak var beaconName: UILabel!
    @IBOutlet weak var beaconProximity: UILabel!
    @IBOutlet weak var beaconDescription: UILabel!
    @IBOutlet weak var inRangeTable: UITableView!

    var nearestDelegateId: Int?
    var inRangeDelegateId: Int?

    var beacons: [BartimeusRangedBeacon] = []

    private var scanner: MovinBeaconScanner!

    override func viewDidLoad() {
        super.viewDidLoad()

        self.nearestDelegateId = BeaconListener.add(nearest: self)
        self.inRangeDelegateId = BeaconListener.add(inRange: self)

        Movin.instance.scanner.add(Movin.instance.map)
        Movin.instance.scanner.start(with: BeaconListener.instance)

        self.inRangeTable.delegate = self
        self.inRangeTable.dataSource = self
//        self.inRangeTable.register(UITableViewCell.self, forCellReuseIdentifier: "beaconcell")
    }

    func updateNearest(beacon rangedBeacon: BartimeusRangedBeacon) {
        guard let beacon = rangedBeacon.movinRangedBeacon?.beacon else { return }

        DispatchQueue.main.async {
            self.beaconName.text = beacon.beaconCode
            self.beaconProximity.text = Movin.nameOf(proximity: (rangedBeacon.movinRangedBeacon?.proximity)!)
            self.beaconDescription.text = beacon.description
        }
    }

    func updateInRange(beacons rangedBeacons: [BartimeusRangedBeacon]) {
        print("update beacons inrange")
        DispatchQueue.main.async {
            self.beacons = rangedBeacons
            self.inRangeTable.reloadData()
        }
    }

    func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        return self.beacons.count
    }

    func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        let cell = inRangeTable.dequeueReusableCell(withIdentifier: "beaconcell", for: indexPath)

        let beacon = self.beacons[indexPath.item]

        cell.textLabel?.text = beacon.movinRangedBeacon?.beacon?.beaconCode
        cell.detailTextLabel?.text = "proximity: \(Movin.nameOf(proximity: (beacon.movinRangedBeacon?.proximity)!))"

        return cell
    }
}
