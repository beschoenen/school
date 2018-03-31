using WCF.Service;

namespace WCF.GameRule
{
    public class Age : IGameRule
    {
        public Tamagotchi Execute(Tamagotchi tamagotchi)
        {
            tamagotchi.Age += 10;

            return tamagotchi;
        }
    }
}