//
//  UIImageView.swift
//  Spotify
//
//  Created by Kevin Richter on 14/03/2017.
//  Copyright © 2017 Kevin Richter. All rights reserved.
//

import Swift
import Foundation
import UIKit

extension UIImageView {

    func downloadFrom(url: URL?, contentMode mode: UIViewContentMode = .scaleAspectFit) {
        contentMode = mode

        self.image = UIImage(named: "artist")

        if url == nil {
            return
        }

        URLSession.shared.dataTask(with: url!) { data, response, error in
            guard
                let httpURLResponse = response as? HTTPURLResponse, httpURLResponse.statusCode == 200,
                let mimeType = response?.mimeType, mimeType.hasPrefix("image"),
                let data = data, error == nil,
                let image = UIImage(data: data)
                else {
                    return
            }

            DispatchQueue.main.async() { () -> Void in
                self.image = image
            }

        }.resume()
    }

    func downloadFrom(link: String, contentMode mode: UIViewContentMode = .scaleAspectFit) {
        guard let url = URL(string: link) else { return }
        downloadFrom(url: url, contentMode: mode)
    }
}
