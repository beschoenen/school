using System.Windows;
using database;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using prog5_ninja.Repositories;
using prog5_ninja.View;

namespace prog5_ninja.ViewModel
{
    public class EquipmentManagementViewModel : ViewModelBase
    {
        private equipment _selectedEquipment;
        public equipment SelectedEquipment
        {
            get { return _selectedEquipment; }
            set
            {
                if (_selectedEquipment == value) return;
                _selectedEquipment = value;

                RaisePropertyChanged();
            }
        }

        public RelayCommand<Window> CreateEquipmentCommand { get; set; }
        public RelayCommand<Window> EditEquipmentCommand { get; set; }
        public RelayCommand DeleteEquipmentCommand { get; set; }

        public EquipmentManagementViewModel()
        {
            CreateEquipmentCommand = new RelayCommand<Window>(ShowCreationDialog);
            EditEquipmentCommand = new RelayCommand<Window>(ShowEditDialog);
            DeleteEquipmentCommand = new RelayCommand(ShowDeleteDialog);
        }

        private void ShowEditDialog(Window window)
        {
            new EquipmentModificationDialog(SelectedEquipment) { Owner = window }.ShowDialog();
        }

        private static void ShowCreationDialog(Window window)
        {
            new EquipmentModificationDialog { Owner = window }.ShowDialog();
        }

        private void ShowDeleteDialog()
        {
            var result = MessageBox.Show($"Do you really want to delete equipment '{SelectedEquipment.name}'?", "Delete Equipment", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);

            if (result != MessageBoxResult.Yes) return;

            EquipmentRepository.Instance.DeleteEquipment(SelectedEquipment);

            SelectedEquipment = null;
        }
    }
}
