//
//  ViewController.swift
//  text-to-speech-demo
//
//  Created by Kevin Richter on 09/03/2017.
//  Copyright © 2017 Kevin Richter. All rights reserved.
//

import UIKit
import AVFoundation

class ViewController: UIViewController {

    @IBOutlet weak var TextField: UITextField!

    let synthesizer = AVSpeechSynthesizer()

    @IBAction func SpeakButton(_ sender: Any) {
        DispatchQueue.main.asyncAfter(deadline: .now() + 1, execute: {
            let utterance = AVSpeechUtterance(string: self.TextField.text!)
            utterance.rate = 0.5

            self.synthesizer.speak(utterance)
        })
    }
}
