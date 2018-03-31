using WCF.Enum;
using WCF.Service;

namespace WCF.GameRule.Condition
{
    public class Starvation : IGameRule
    {
        public Tamagotchi Execute(Tamagotchi tamagotchi)
        {
            if (tamagotchi.Hunger > 80)
            {
                tamagotchi.Health -= 20;
                if (!tamagotchi.Statuses.Contains(Status.Starved))
                {
                    tamagotchi.Statuses.Add(Status.Starved);
                }
            }
            else
            {
                tamagotchi.Statuses.Remove(Status.Starved);
            }

            return tamagotchi;
        }
    }
}