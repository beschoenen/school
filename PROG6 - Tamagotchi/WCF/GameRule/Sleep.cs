using System;
using WCF.Service;

namespace WCF.GameRule
{
    public class Sleep : IGameRule
    {
        public Tamagotchi Execute(Tamagotchi tamagotchi)
        {
            tamagotchi.Sleep += new Random(Guid.NewGuid().GetHashCode()).Next(15, 35);

            return tamagotchi;
        }
    }
}