using System.Linq;
using WCF.Enum;
using WCF.Service;

namespace WCF.GameRule.Condition
{
    public class Crazy : IGameRule
    {
        public Tamagotchi Execute(Tamagotchi tamagotchi)
        {
            if (tamagotchi.Sleep > 70 && tamagotchi.Hunger > 70)
            {
                if (!tamagotchi.Statuses.Contains(Status.Crazy))
                {
                    tamagotchi.Statuses.Add(Status.Crazy);
                }
            }
            else
            {
                tamagotchi.Statuses.Remove(Status.Crazy);
            }

            return tamagotchi;
        }
    }
}