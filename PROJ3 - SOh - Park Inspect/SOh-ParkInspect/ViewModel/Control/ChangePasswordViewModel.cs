using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using SOh_ParkInspect.Helper;
using SOh_ParkInspect.Model;
using SOh_ParkInspect.Repository.Interface;

namespace SOh_ParkInspect.ViewModel.Control
{
    public class ChangePasswordViewModel : ParkInspectViewModel
    {
        private readonly IUserRepository _repository;
        private readonly RouterService _router;

        public ICommand ChangePasswordCommand { get; set; }
        public ICommand BackCommand { get; set; }

        public ChangePasswordViewModel(IUserRepository repository, RouterService router)
        {
            _repository = repository;
            _router = router;

            ChangePasswordCommand = new RelayCommand<List<object>>(ChangePassword);
            BackCommand = new RelayCommand(_router.GoBack);
        }

        public override void OnEnter()
        {
            if (Settings.IsOfflineMode)
            {
                MessageBox.Show("Dit scherm is niet beschikbaar in offline mode.");
                _router.GoBack();
                return;
            }
        }

        private void ChangePassword(List<object> passwordBoxes)
        {
            var oldPasswordBox = (PasswordBox) passwordBoxes[0];
            var newPasswordOneBox = (PasswordBox) passwordBoxes[1];
            var newPasswordTwoBox = (PasswordBox) passwordBoxes[2];

            var someEmpty = false;
            new List<PasswordBox> { oldPasswordBox, newPasswordOneBox, newPasswordTwoBox }.ForEach(p =>
            {
                if (string.IsNullOrWhiteSpace(p.Password))
                {
                    someEmpty = true;
                }
            });

            if (someEmpty) return;

            // If the new passwords are the same
            if (!newPasswordOneBox.Password.Equals(newPasswordTwoBox.Password)) return;

            if (!_repository.ChangePassword(Settings.CurrentUser?.Email, oldPasswordBox, newPasswordOneBox))
            {
                MessageBox.Show("Er ging iets fout tijdens het aanpassen van je wachtwoord!");
                return;
            }

            MessageBox.Show("Je wachtwoord is aangepast.");
            _router.GoBack();
        }
    }
}