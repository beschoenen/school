//
//  ViewController.swift
//  beacons-poc
//
//  Created by Kevin Richter on 26/02/2017.
//  Copyright © 2017 Kevin Richter. All rights reserved.
//

import UIKit
import CoreLocation

class ViewController: UITableViewController, CLLocationManagerDelegate {

    var beaconUUID: UUID = UUID(uuidString: "f7826da6-4fa2-4e98-8024-bc5b71e0893e")!
    
    var locationManager: CLLocationManager?
    var beacons: [CLBeacon] = []
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        locationManager = CLLocationManager()
        locationManager!.delegate = self
        locationManager!.requestWhenInUseAuthorization()
    }

    /////
    // Location Manager
    ////

    func locationManager(_ manager: CLLocationManager, didChangeAuthorization status: CLAuthorizationStatus) {
        if status != .authorizedWhenInUse {
            return
        }

        if CLLocationManager.isMonitoringAvailable(for: CLBeaconRegion.self) {
            if CLLocationManager.isRangingAvailable() {
                startScanning()
            }
        }
    }

    func locationManager(_ manager: CLLocationManager, didRangeBeacons beacons: [CLBeacon], in region: CLBeaconRegion) {
        self.beacons = beacons
        self.tableView.reloadData()
    }


    /////
    // Table View
    ////
    
    override func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        return beacons.count
    }
    
    override func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        let cell = tableView.dequeueReusableCell(withIdentifier: "BeaconCell", for: indexPath)
        
        cell.textLabel?.text = "Beacon at \(String(beacons[indexPath.row].rssi)) RSSI"
        cell.detailTextLabel?.text = "Major: \(String(describing: beacons[indexPath.row].major)), Minor: \(String(describing: beacons[indexPath.row].minor))"
        
        updateDistance(beacons[indexPath.row].proximity, forCell: cell)
        
        return cell
    }
    
    /////
    // Scanning methods
    /////
    
    func startScanning() {
        let beaconRegion = CLBeaconRegion(proximityUUID: self.beaconUUID, identifier: "MyBeacon")
        
        locationManager?.startMonitoring(for: beaconRegion)
        locationManager?.startRangingBeacons(in: beaconRegion)
    }
    
    func updateDistance(_ distance: CLProximity, forCell cell: UITableViewCell) {
        UIView.animate(withDuration: 0.8) {
            switch distance {
            case .unknown:
                cell.backgroundColor = UIColor.gray
            case .far:
                cell.backgroundColor = UIColor.blue
            case .near:
                cell.backgroundColor = UIColor.orange
            case .immediate:
                cell.backgroundColor = UIColor.red
            }
        }
    }
    
}

