using WCF.Service;

namespace WCF.GameRule
{
    public interface IGameRule
    {
        Tamagotchi Execute(Tamagotchi tamagotchi);
    }
}
