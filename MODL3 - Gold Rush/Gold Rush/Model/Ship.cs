using System;

namespace Gold_Rush.Model
{
    public class Ship : Vehicle
    {
        public const int MaxLoad = 8;

        public bool OnWayBack { get; private set; }

        public bool AtDock
        {
            get { return Water.Dock != null; }
        }

        public override int Load { get; set; }

        public Water Water { get; set; }

        public Ship()
        {
            OnWayBack = false;
        }

        public override void Move()
        {
            // If we're moving back towards the Dock.
            if (OnWayBack)
            {
                // Move the Ship.
                Water = Water.Previous as Water;

                // If we've arrived at the Dock.
                if (AtDock) OnWayBack = false;
            }

            // If we're not fully loaded.
            if (Load != MaxLoad) return;

            // If there's no next Water Field.
            if (Water.Next == null)
            {
                Unload();
                OnWayBack = true;
                return;
            }

            // Move the Ship.
            Water = Water.Next as Water;
        }

        public void Unload()
        {
            Load = 0;
            Game.Score += 10;
        }
    }
}