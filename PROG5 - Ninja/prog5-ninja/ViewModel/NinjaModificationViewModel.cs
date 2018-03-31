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
    public class NinjaModificationViewModel : ViewModelBase
    {
        private NinjaCrudModel _ninja;
        public NinjaCrudModel Ninja
        {
            get { return _ninja; }
            set
            {
                _ninja = value;

                RaisePropertyChanged();
            }
        }

        public RelayCommand<Window> SaveButtonClickedCommand { get; private set; }

        public NinjaModificationViewModel()
        {
            _ninja = new NinjaCrudModel();

            SaveButtonClickedCommand = new RelayCommand<Window>(SaveNinja);
        }

        public void SetModel(ninja ninja)
        {
            _ninja = new NinjaCrudModel(ninja);

            RaisePropertyChanged(nameof(Ninja));
        }

        private void SaveNinja(Window window)
        {
            var message = ValidateName();
            if (message != null)
            {
                MessageBox.Show(message, "Input error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            NinjaRepository.Instance.SaveNinja(Ninja.GetModel());

            window.Close();
        }

        private string ValidateName()
        {
            if (Ninja.Name == Ninja.OriginalNinja.name) return null;

            if (Ninja.Name == string.Empty)
            {
                return "Please give your ninja a name!";
            }

            if (NinjaRepository.Instance.All.Any(e => e.name == Ninja.Name))
            {
                return "A ninja with this name already exists, please pick another name!";
            }

            return null;
        }
    }
}
