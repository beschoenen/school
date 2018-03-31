using System;

namespace Gold_Rush.Model
{
    public class Dock
    {
        public Ship Ship { get; set; }

        public void TransferLoad(Cart cart)
        {
            if (!Ship.AtDock) return;

            Game.Score++;
            Ship.Load++;

            cart.Load = 0;
        }
    }
}