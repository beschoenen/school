using System;
using WCF.Service;

namespace WCF.GameRule
{
    public class Hunger : IGameRule
    {
        public Tamagotchi Execute(Tamagotchi tamagotchi)
        {
            tamagotchi.Hunger += new Random(Guid.NewGuid().GetHashCode()).Next(15, 35);

            return tamagotchi;
        }
    }
}