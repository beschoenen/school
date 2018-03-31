using System.Collections.Generic;
using Database;

namespace WCF.Repository
{
    public interface ITamagotchiRepository
    {
        Tamagotchi FindById(int id);

        List<Tamagotchi> GetAll();

        void Remove(Tamagotchi tamagotchi);

        void AddOrUpdate(Tamagotchi tamagotchi);

        void SaveChanges();
    }
}
