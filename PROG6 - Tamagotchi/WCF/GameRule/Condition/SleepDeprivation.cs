using WCF.Enum;
using WCF.Service;

namespace WCF.GameRule.Condition
{
    public class SleepDeprivation : IGameRule
    {
        public Tamagotchi Execute(Tamagotchi tamagotchi)
        {
            if (tamagotchi.Sleep > 80)
            {
                tamagotchi.Health -= 20;
                if (!tamagotchi.Statuses.Contains(Status.SleepDeprevated))
                {
                    tamagotchi.Statuses.Add(Status.SleepDeprevated);
                }
            }
            else
            {
                tamagotchi.Statuses.Remove(Status.SleepDeprevated);
            }

            return tamagotchi;
        }
    }
}