using System;
using WCF.Service;

namespace WCF.GameRule
{
    public class Boredom : IGameRule
    {
        public Tamagotchi Execute(Tamagotchi tamagotchi)
        {
            tamagotchi.Boredom += new Random(Guid.NewGuid().GetHashCode()).Next(15, 35);

            return tamagotchi;
        }
    }
}