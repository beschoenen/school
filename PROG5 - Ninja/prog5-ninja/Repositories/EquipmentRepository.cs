using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Runtime.CompilerServices;
using database;
using prog5_ninja.Annotations;

namespace prog5_ninja.Repositories
{
    public class EquipmentRepository : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private static Entities Context => Database.Instance.Context;

        private static EquipmentRepository _instance;
        public static EquipmentRepository Instance => _instance ?? (_instance = new EquipmentRepository());
        
        public ObservableCollection<equipment> All => new ObservableCollection<equipment>(Context.equipments.Local.ToList());

        private EquipmentRepository()
        {
            Context.equipments.Load();
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public List<equipment> ItemsInCategory(category category)
        {
            return All.Where(e => e.category == category).ToList();
        }

        public void SaveModel(equipment equipment)
        {
            Context.equipments.AddOrUpdate(equipment);

            EquipmentChanged();
        }

        public void RemoveItemFromNinjas(equipment equipment)
        {
            equipment.ninjas.Clear();

            EquipmentChanged();
        }

        public void DeleteEquipment(equipment equipment)
        {
            equipment.ninjas.Clear();
            Context.equipments.Remove(equipment);

            All.Remove(equipment);

            EquipmentChanged();
        }

        private void EquipmentChanged()
        {
            OnPropertyChanged(nameof(All));
        }
    }
}
