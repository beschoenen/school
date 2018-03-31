//
//  MainViewController.swift
//  Bartimeus
//
//  Created by Job Cuppen on 01/06/2017.
//  Copyright © 2017 Kevin Richter. All rights reserved.
//

import Foundation

class MainViewController: UIViewController {

    @IBOutlet weak var navigatieLabel: UILabel!
    @IBOutlet weak var navigatieButton: UIButton!
    @IBOutlet weak var tourButton: UIButton!

    override func prepare(for segue: UIStoryboardSegue, sender: Any?) {
        if let destinationController = segue.destination as? DestinationSelectionViewController {
            let buttonText = (sender as? UIButton)!.titleLabel?.text
            guard buttonText != nil else { return }

            destinationController.navigator = self.getNavigator(fromText: buttonText)
        }
    }

    override func viewDidLoad() {
        super.viewDidLoad()

        tourButton.layer.cornerRadius = 10
        tourButton.titleLabel?.minimumScaleFactor = 0.2
        tourButton.titleLabel?.adjustsFontSizeToFitWidth = true

        navigatieButton.layer.cornerRadius = 10
        navigatieButton.titleLabel?.minimumScaleFactor = 0.2
        navigatieButton.titleLabel?.adjustsFontSizeToFitWidth = true
    }

    private func getNavigator(fromText text: String?) -> Navigator? {
        switch text {
        case "Navigatie"?:
            return DynamicNavigation()
        case "Tour"?:
            return TourNavigation()
        default:
            return nil
        }
    }

}
