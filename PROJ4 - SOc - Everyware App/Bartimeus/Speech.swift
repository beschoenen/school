//
//  Speech.swift
//  Bartimeus
//
//  Created by Kevin Richter on 07/05/2017.
//  Copyright © 2017 Kevin Richter. All rights reserved.
//

import AVFoundation

class Speech {

    private let synthesizer: AVSpeechSynthesizer

    private var voice: AVSpeechSynthesisVoice?

    private var speed: Float

    init() {
        self.synthesizer = AVSpeechSynthesizer()
        self.voice = AVSpeechSynthesisVoice(language: "nl-NL")
        self.speed = 0.45
    }

    /**
     Set the language

     - parameter language: The language code, default `nl-NL`

     - returns: The instance of the Speech class
     */
    public func set(language: String) -> Self {
        self.voice = AVSpeechSynthesisVoice(language: language)

        return self
    }

    /**
     Set the speed of the pronunciation

     - parameter speed: The speed of the pronunciation, default `0.5`

     - returns: The instance of the Speech class
     */
    public func set(speed: Float) -> Self {
        self.speed = speed

        return self
    }

    /**
     Speak a sentence

     - parameter sentence: The sentence to be spoken
     */
    public func say(sentence phrase: String) {
        return say(sentence: phrase, withDelay: .now())
    }

    /**
     Speak a sentence with a delay

     - parameter sentence: The sentence to be spoken
     - parameter withDelay: The amount of default before the sentence will be spoken
     */
    public func say(sentence phrase: String, withDelay delay: DispatchTime) {
        DispatchQueue.main.asyncAfter(deadline: delay) {
            let utterance = AVSpeechUtterance(string: phrase)

            utterance.rate = self.speed
            utterance.voice = self.voice

            self.synthesizer.speak(utterance)
        }
    }
}
