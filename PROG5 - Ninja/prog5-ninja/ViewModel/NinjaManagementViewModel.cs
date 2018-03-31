using System.Windows;
using database;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using prog5_ninja.Repositories;
using prog5_ninja.View;

namespace prog5_ninja.ViewModel
{
    public class NinjaManagementViewModel : ViewModelBase
    {
        private ninja _selectedNinja;
        public ninja SelectedNinja
        {
            get { return _selectedNinja; }
            set
            {
                if (_selectedNinja == value) return;
                _selectedNinja = value;

                RaisePropertyChanged();
            }
        }

        public RelayCommand<Window> CreateNinjaCommand { get; set; }
        public RelayCommand<Window> EditNinjaCommand { get; set; }
        public RelayCommand DeleteNinjaCommand { get; set; }

        public NinjaManagementViewModel()
        {
            CreateNinjaCommand = new RelayCommand<Window>(ShowCreationDialog);
            EditNinjaCommand = new RelayCommand<Window>(ShowEditDialog);
            DeleteNinjaCommand = new RelayCommand(ShowDeleteDialog);
        }

        private void ShowEditDialog(Window window)
        {
            new NinjaModificationDialog(SelectedNinja) { Owner = window }.ShowDialog();
        }

        private static void ShowCreationDialog(Window window)
        {
            new NinjaModificationDialog { Owner = window }.ShowDialog();
        }

        private void ShowDeleteDialog()
        {
            var result = MessageBox.Show($"Do you really want to delete ninja '{SelectedNinja.name}'?", "Delete Ninja", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);

            if (result != MessageBoxResult.Yes) return;

            NinjaRepository.Instance.DeleteNinja(SelectedNinja);

            SelectedNinja = null;
        }
    }
}
