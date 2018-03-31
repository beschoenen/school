//
//  Navigator.swift
//  Bartimeus
//
//  Created by Kevin Richter on 16/06/2017.
//  Copyright © 2017 Kevin Richter. All rights reserved.
//
import Foundation

protocol NavigationListener {
    func nodeChanged()
    func routeEnded()
}

protocol Navigator: NearestBeaconNotifier {

    var navigationListeners: [NavigationListener] { get set }
    var canNavigate: Bool { get }
    // Destination / Tour selection
    var routeSelectionTitle: String { get }
    func routeList(callback: @escaping ([String : String]) -> Void)
    func select(route: String)

    // Current navigation
    func startNavigation()
    func currentInstruction() -> String?
    func currentDirection() -> Int?
}

extension Navigator {

    mutating func listen(delegate: NavigationListener) -> Int {
        self.navigationListeners.append(delegate)
        return self.navigationListeners.count - 1
    }

    mutating func unListen(listenerId index: Int) {
        self.navigationListeners.remove(at: index)
    }

    func notifyListeners() {
        self.navigationListeners.forEach { $0.nodeChanged() }
    }

    func routeEnded() {
        self.navigationListeners.forEach { $0.routeEnded() }
    }

}
