using System.Collections.Generic;
using System.Windows;
using GalaSoft.MvvmLight;
using database;
using GalaSoft.MvvmLight.Command;
using prog5_ninja.Repositories;
using prog5_ninja.View;

namespace prog5_ninja.ViewModel
{
    public class ShopViewModel : ViewModelBase
    {
        private category _selectedCategory;
        public category SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                if(_selectedCategory == value) return;
                _selectedCategory = value;

                RaisePropertyChanged(nameof(ItemsInCategory));
            }
        }

        private equipment _selectedItem;
        public equipment SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (_selectedItem == value) return;
                _selectedItem = value;

                RaisePropertyChanged();
            }
        }

        public List<equipment> ItemsInCategory => EquipmentRepository.Instance.ItemsInCategory(SelectedCategory);

        public RelayCommand EquipItemCommand { get; set; }

        public RelayCommand ManageNinjasCommand { get; set; }
        public RelayCommand ManageEquipmentCommand { get; set; }

        public RelayCommand SaveCommand { get; set; }
        public RelayCommand ExitCommand { get; set; }

        public ShopViewModel()
        {
            EquipItemCommand = new RelayCommand(EquipItem);

            ManageNinjasCommand = new RelayCommand(ManageNinjasDialog);
            ManageEquipmentCommand = new RelayCommand(ManageEquipmentDialog);

            SaveCommand = new RelayCommand(SaveChanges);
            ExitCommand = new RelayCommand(Application.Current.Shutdown);

            EquipmentRepository.Instance.PropertyChanged += (sender, e) =>
            {
                if(e.PropertyName == "All") EquipmentChanged();
            };
        }

        private void EquipmentChanged()
        {
            RaisePropertyChanged(nameof(ItemsInCategory));
            RaisePropertyChanged(nameof(SelectedItem));
        }

        private void EquipItem()
        {
            NinjaRepository.Instance.EquipItem(SelectedItem);
        }

        private void ManageNinjasDialog()
        {
            new NinjaManagementDialog().ShowDialog();
        }

        private void ManageEquipmentDialog()
        {
            new EquipmentManagementDialog().ShowDialog();
        }

        private void SaveChanges()
        {
            Database.Instance.Context.SaveChanges();
        }
    }
}
