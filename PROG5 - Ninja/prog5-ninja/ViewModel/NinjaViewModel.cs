using System.Linq;
using database;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using prog5_ninja.Repositories;

namespace prog5_ninja.ViewModel
{
    public class NinjaViewModel : ViewModelBase
    {
        public const int TotalGold = 5000;

        public string Gold => $"{NinjaRepository.Instance.CurrentNinja?.equipment.Sum(e => e.value) ?? 0}/{TotalGold}";
        public int TotalStrength => NinjaRepository.Instance.CurrentNinja?.equipment.Sum(e => e.strength) ?? 0;
        public int TotalIntelligence => NinjaRepository.Instance.CurrentNinja?.equipment.Sum(e => e.intelligence) ?? 0;
        public int TotalAgility => NinjaRepository.Instance.CurrentNinja?.equipment.Sum(e => e.agility) ?? 0;

        public RelayCommand<equipment> RemoveItemCommand { get; set; }
        public RelayCommand ClearNinjaCommand { get; set; }

        public NinjaViewModel()
        {
            RemoveItemCommand = new RelayCommand<equipment>(RemoveItem);
            ClearNinjaCommand = new RelayCommand(ClearNinja);

            EquipmentRepository.Instance.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == "All") NinjaUpdated();
            };

            NinjaRepository.Instance.PropertyChanged += (sender, e) =>
            {
                if (new[] {"CurrentNinja", "All"}.Contains(e.PropertyName)) NinjaUpdated();
            };
        }

        private void RemoveItem(equipment equipment)
        {
            NinjaRepository.Instance.UnequipItem(equipment);
            NinjaUpdated();
        }

        private void ClearNinja()
        {
            NinjaRepository.Instance.ClearItemsFromNinja();
            NinjaUpdated();
        }

        private void NinjaUpdated()
        {
            RaisePropertyChanged(nameof(TotalStrength));
            RaisePropertyChanged(nameof(TotalIntelligence));
            RaisePropertyChanged(nameof(TotalAgility));
            RaisePropertyChanged(nameof(Gold));
        }
    }
}
