using System;
using Gold_Rush.Enum;

namespace Gold_Rush.Model
{
    public class Cart : Vehicle
    {
        public override int Load { get; set; }

        public Track Track { get; set; }

        public Cart(Track firstTrack)
        {
            Load = 1;
            Track = firstTrack;
        }

        public override void Move()
        {
            // If the next track is null, then we're at the end of the route.
            if (Track.Next == null)
            {
                // If we're on a Yard; don't move.
                if (Track is Yard) return;

                // Remove ourselves from the Track.
                Track.Cart = null;

                // Clear the Track; So we can be removed from the PlayingGround.
                Track = null;

                return;
            }

            if (Track is Yard)
            {
                if (Track.Next.Cart != null) return;
            }

            if (Track.Next is Switch)
            {
                var gameSwitch = (Switch) Track.Next;
                if (gameSwitch.SwitchType == SwitchType.Merge && !(
                        (gameSwitch.SwitchState == SwitchState.Top && gameSwitch.Top == Track) ||
                        (gameSwitch.SwitchState == SwitchState.Bottom && gameSwitch.Bottom == Track)
                    )) return;
            }

            // If the Next Track has a Cart; But we can't collide on a Yard.
            if (Track.Next.Cart != null && !(Track is Yard))
            {
                Game.IsPlaying = false; // Collided
                return;
            }

            // Remove ourselves from the Track.
            Track.Cart = null;

            // Move the Cart.
            Track = Track.Next;
            Track.Cart = this;

            // Check if we can offload our Load.
            if (Track.Dock != null)
            {
                Track.Dock.TransferLoad(this);
            }
        }
    }
}