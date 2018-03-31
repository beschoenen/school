using System.Linq;
using System.Windows;
using database;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Practices.ServiceLocation;
using prog5_ninja.Model;
using prog5_ninja.Repositories;

namespace prog5_ninja.ViewModel
{
    public class EquipmentModificationViewModel : ViewModelBase
    {
        private EquipmentCrudModel _equipment;
        public EquipmentCrudModel Equipment
        {
            get { return _equipment; }
            set
            {
                _equipment = value;
                RaisePropertyChanged();
            }
        }

        public RelayCommand<Window> SaveEquipmentCommand { get; set; }

        public EquipmentModificationViewModel()
        {
            SaveEquipmentCommand = new RelayCommand<Window>(SaveEquipment);
        }

        private void SaveEquipment(Window window)
        {
            var message = ValidateInput();
            if (message != null)
            {
                MessageBox.Show(message, "Input error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            if (Equipment.CategoryHasChanged())
            {
                var result = MessageBox.Show("The category of this item has changed, it will be removed from all your ninjas!",
                    "Category Changed", MessageBoxButton.OKCancel, MessageBoxImage.Exclamation);

                switch (result)
                {
                    case MessageBoxResult.OK:
                        EquipmentRepository.Instance.RemoveItemFromNinjas(Equipment.OriginalEquipment);
                        break;
                    case MessageBoxResult.Cancel:
                        Equipment.ResetCategory();
                        RaisePropertyChanged(nameof(Equipment));
                        return;
                }
            }

            EquipmentRepository.Instance.SaveModel(Equipment.GetModel());

            window.Close();
        }

        private string ValidateInput()
        {
            if (Equipment.Name == string.Empty)
            {
                return "Name can't be empty!";
            }

            if (EquipmentRepository.Instance.All.Where(e => Equipment.Name != Equipment.OriginalEquipment.name).Any(e => e.name == Equipment.Name))
            {
                return "An item with that name already exists!";
            }

            if (Equipment.Category == null)
            {
                return "Please select a category!";
            }

            if (Equipment.Image == string.Empty)
            {
                return "Image can't be empty!";
            }

            if (Equipment.Value <= 0)
            {
                return "Value has to be greater than 0";
            }

            return null;
        }
    }
}
