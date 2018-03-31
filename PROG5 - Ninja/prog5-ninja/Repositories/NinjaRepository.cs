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
    public class NinjaRepository : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private static Entities Context => Database.Instance.Context;

        private static NinjaRepository _instance;
        public static NinjaRepository Instance => _instance ?? (_instance = new NinjaRepository());

        public ObservableCollection<ninja> All => new ObservableCollection<ninja>(Context.ninjas.Local.ToList());

        private NinjaRepository()
        {
            Context.ninjas.Load();
        }

        private ninja _currentNinja;
        public ninja CurrentNinja {
            get { return _currentNinja; }
            set
            {
                if (_currentNinja == value) return;
                _currentNinja = value;

                CurrentNinjaChanged();
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void SaveNinja(ninja ninja)
        {
            Context.ninjas.AddOrUpdate(ninja);

            NinjasChanged();
            if (ninja == CurrentNinja) CurrentNinjaChanged();
        }

        public void DeleteNinja(ninja ninja)
        {
            ninja.equipment.Clear();
            Context.ninjas.Remove(ninja);

            NinjasChanged();
            if (ninja == CurrentNinja) CurrentNinjaChanged();
        }

        public void ClearItemsFromNinja()
        {
            CurrentNinja.equipment.Clear();

            NinjasChanged();
        }

        public void EquipItem(equipment equipment)
        {
            UnequipItem(equipment.category);

            CurrentNinja.equipment.Add(equipment);

            CurrentNinjaChanged();
        }

        private void UnequipItem(category category)
        {
            var item = CurrentNinja.equipment.FirstOrDefault(e => e.category == category);
            if (item != null)
            {
                CurrentNinja.equipment.Remove(item);
            }
        }

        public void UnequipItem(equipment equipment)
        {
            CurrentNinja.equipment.Remove(equipment);

            NinjasChanged();
        }

        private void NinjasChanged()
        {
            OnPropertyChanged(nameof(All));
        }

        private void CurrentNinjaChanged()
        {
            OnPropertyChanged(nameof(CurrentNinja));
        }
    }
}
