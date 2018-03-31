using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using SOh_ParkInspect.Enum;
using SOh_ParkInspect.Helper;
using SOh_ParkInspect.Model;
using SOh_ParkInspect.Repository.Interface;
using static BCrypt.Net.BCrypt;

namespace SOh_ParkInspect.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        private IUserRepository _repository;

        public ICommand LoginCommand { get; set; }

        public string Email { get; set; }

        public LoginViewModel(IUserRepository repository)
        {
            _repository = repository;

            LoginCommand = new RelayCommand<List<object>>(Login);
        }

        private void Login(List<object> objects)
        {
            var passwordBox = objects[0] as PasswordBox;
            var window = objects[1] as Window;
            var offlineCheckbox = objects[2] as CheckBox;

            if (string.IsNullOrWhiteSpace(passwordBox?.Password) || string.IsNullOrWhiteSpace(Email)) return;

            // If online mode, but no internet
            if (!(offlineCheckbox?.IsChecked ?? false) && !Internet.IsAvailable)
            {
                if (DbSync.CanConnectToLocalDb)
                {
                    MessageBox.Show("De lokale database is niet beschikbaar!", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var result = MessageBox.Show("U heeft geen internetverbinding, wilt u verder gaan in offline modus?", "Waarschuwing", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    if (offlineCheckbox != null)
                    {
                        offlineCheckbox.IsChecked = true;
                    }
                }
                else return;
            }

            // Set offline mode & Reregister repositories
            ViewModelLocator.RegisterRepositories(offlineCheckbox?.IsChecked ?? false ? RepositoryType.Offline : RepositoryType.Online);

            _repository = SimpleIoc.Default.GetInstanceWithoutCaching<IUserRepository>();

            var password = passwordBox.Password;
            var user = _repository.Find(Email);

            if (user == null || !Verify(password, user.Password))
            {
                MessageBox.Show("Uw gebruikersnaam of wachtwoord is incorrect.");
                return;
            }

            Settings.CurrentUser = user;
            Settings.OfflineMode = offlineCheckbox?.IsChecked ?? false;

            window?.Close();
        }
    }
}