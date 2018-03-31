using System.Collections.Generic;
using System.Linq;
using Database;

namespace WCF.Repository
{
    public class DummyTamagotchiRepository : ITamagotchiRepository
    {
        private readonly List<Tamagotchi> _tamagotchis;

        public DummyTamagotchiRepository()
        {
            _tamagotchis = new List<Tamagotchi>();
        }

        public Tamagotchi FindById(int id)
        {
            return _tamagotchis.FirstOrDefault(t => t.Id == id);
        }

        public List<Tamagotchi> GetAll()
        {
            return _tamagotchis;
        }

        public void Remove(Tamagotchi tamagotchi)
        {
            _tamagotchis.Remove(tamagotchi);
        }

        public void AddOrUpdate(Tamagotchi tamagotchi)
        {
            if (FindById(tamagotchi.Id) != null)
            {
                _tamagotchis.Remove(_tamagotchis.First(t => t.Id == tamagotchi.Id));
            }

            _tamagotchis.Add(tamagotchi);
        }

        public void SaveChanges()
        {
            // ¯\_(ツ)_/¯
        }
    }
}
