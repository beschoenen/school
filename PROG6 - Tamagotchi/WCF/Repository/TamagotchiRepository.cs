using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using Database;

namespace WCF.Repository
{
    public class TamagotchiRepository : ITamagotchiRepository
    {
        private readonly Entities _db;

        public TamagotchiRepository()
        {
            _db = new Entities();
            _db.Database.Connection.Open();
        }

        public Tamagotchi FindById(int id)
        {
            return _db.Tamagotchis.FirstOrDefault(t => t.Id == id);
        }

        public List<Tamagotchi> GetAll()
        {
            return _db.Tamagotchis.ToList();
        }

        public void Remove(Tamagotchi tamagotchi)
        {
            _db.Tamagotchis.Remove(tamagotchi);
        }

        public void AddOrUpdate(Tamagotchi tamagotchi)
        {
            _db.Tamagotchis.AddOrUpdate(tamagotchi);
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}
