using System;
using System.Security.Cryptography;

namespace Gold_Rush.Model
{
    public class Warehouse
    {
        public Track FirstTrack { get; set; }

        public Cart NewCart()
        {
            var random = new Random(DateTime.Now.ToLongTimeString().GetHashCode() + GetHashCode()).Next(1, 100);
            var score = 10 + (Game.Score / 9) + ((Game.Score / 18) * 5) / 100;

            return random < score ? new Cart(FirstTrack) : null;
        }
    }
}