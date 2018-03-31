using System;
using System.Collections.Generic;
using System.Linq;

namespace Gold_Rush.Model
{
    public class PlayingGround
    {
        public Dictionary<char, Warehouse> Warehouses { get; set; }

        public Dock Dock { get; set; }

        public List<Cart> Carts { get; set; }

        public Dictionary<int, Switch> Switches { get; set; }

        public Ship Ship { get; set; }

        public void MoveSwitch(int id)
        {
            if (Switches.ContainsKey(id))
            {
                Switches[id].ChangeState();
            }
        }

        public void MoveVehicles()
        {
            Ship.Move();

            Carts.ForEach(e => e.Move());
            Carts.RemoveAll(e => e.Track == null);
        }

        public void SpawnNewCarts()
        {
            Warehouses.ToList().ForEach(e =>
            {
                var cart = e.Value.NewCart();
                if (cart != null)
                {
                    cart.Track = e.Value.FirstTrack;
                    e.Value.FirstTrack.Cart = cart;
                    Carts.Add(cart);
                }
            });
        }
    }
}