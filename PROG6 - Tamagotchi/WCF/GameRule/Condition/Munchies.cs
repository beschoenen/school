using System;
using WCF.Enum;
using WCF.Service;

namespace WCF.GameRule.Condition
{
    public class Munchies : IGameRule
    {
        public Tamagotchi Execute(Tamagotchi tamagotchi)
        {
            if (tamagotchi.Boredom > 80)
            {
                tamagotchi.Hunger += new Random(Guid.NewGuid().GetHashCode()).Next(15, 35);
                if (!tamagotchi.Statuses.Contains(Status.Munchies))
                {
                    tamagotchi.Statuses.Add(Status.Munchies);
                }
            }
            else
            {
                tamagotchi.Statuses.Remove(Status.Munchies);
            }

            return tamagotchi;
        }
    }
}