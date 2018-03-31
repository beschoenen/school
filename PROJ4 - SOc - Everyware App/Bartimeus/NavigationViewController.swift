//
//  NavigationViewController.swift
//  Bartimeus
//
//  Created by Coen on 15/05/2017.
//  Copyright © 2017 Kevin Richter. All rights reserved.
//

import UIKit

class NavigationViewController: UIViewController, NavigationListener {

    var listenerId: Int?
    var navigator: Navigator!
    var tts = Speech()

    @IBOutlet weak var iconLabel: UILabel!
    @IBOutlet weak var instructionLabel: UILabel!
    @IBOutlet weak var repeatButton: UIButton!

    override func viewDidLoad() {
        super.viewDidLoad()

        self.listenerId = self.navigator.listen(delegate: self)
        self.navigator.startNavigation()

        if UIAccessibilityIsVoiceOverRunning() {
            repeatButton.isHidden = true
        } else {
            print("voice over is not running")
        }

        repeatButton.layer.cornerRadius = 8
        repeatButton.titleLabel?.minimumScaleFactor = 0.33
        repeatButton.titleLabel?.adjustsFontSizeToFitWidth = true
    }

    @IBAction func repeatInstruction(_ sender: Any) {
        tts.say(sentence: self.instructionLabel.text!)
    }

    func nodeChanged() {
        DispatchQueue.main.async {
            self.instructionLabel.text = self.navigator.currentInstruction() ?? "Geen instructie beschikbaar"
            self.iconLabel.text = Direction.from(number: self.navigator.currentDirection() ?? 8)
            self.tts.say(sentence: self.instructionLabel.text!)
        }
    }

    func routeEnded() {
        DispatchQueue.main.async {
            let alert = UIAlertController(
                title: "Einde navigatie",
                message: "U heeft het einde van de navigatie bereikt",
                preferredStyle: UIAlertControllerStyle.alert
            )

            alert.addAction(UIAlertAction(title: "Ok", style: UIAlertActionStyle.default, handler: nil))

            self.present(alert, animated: true, completion: nil)
        }

        self.navigator.unListen(listenerId: self.listenerId!)
    }
}
