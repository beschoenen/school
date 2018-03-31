using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Database;
using GalaSoft.MvvmLight.CommandWpf;
using SOh_ParkInspect.Helper;
using SOh_ParkInspect.Model;
using SOh_ParkInspect.Repository.Interface;
using static BCrypt.Net.BCrypt;
using static CryptSharp.Crypter;

namespace SOh_ParkInspect.ViewModel.Control
{
    public class RegisterInspectorViewModel : ParkInspectViewModel
    {
        private readonly IUserRepository _userRepository;
        private readonly RouterService _router;

        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public ObservableCollection<JobTitle> JobTypes { get; set; }

        private Employee _employee;

        public Employee Employee
        {
            get { return _employee; }
            set
            {
                _employee = value;
                RaisePropertyChanged();
            }
        }

        private JobTitle _selectedJobType;

        public JobTitle SelectedJobType
        {
            get { return _selectedJobType; }
            set
            {
                _selectedJobType = value;
                RaisePropertyChanged();
            }
        }

        private string _returnMessage;

        public string ReturnMessage
        {
            get { return _returnMessage; }
            set
            {
                _returnMessage = value;
                RaisePropertyChanged();
            }
        }

        public RegisterInspectorViewModel(RouterService router, IUserRepository userRepository, IJobTitleRepository jobTitleRepository)
        {
            _router = router;
            _userRepository = userRepository;

            SaveCommand = new RelayCommand<List<object>>(Save);
            CancelCommand = new RelayCommand(_router.GoBack);

            JobTypes = new ObservableCollection<JobTitle>(jobTitleRepository.All());
        }

        private void Save(List<object> passwordBoxes)
        {
            if (!CanSave(passwordBoxes)) return;

            var passwordBox = (PasswordBox) passwordBoxes[0];

            Employee.Password = HashPassword(passwordBox.Password);
            Employee.JobTitle = SelectedJobType;

            if (_userRepository.Add(Employee))
            {
                MessageBox.Show($"Werknemer {Employee.FirstName} toegevoegd!");

                _router.GoBack();
                return;
            }

            MessageBox.Show("Er heeft zich een fout voorgedaan.");
        }

        private bool CanSave(IReadOnlyList<object> passwordBoxes)
        {
            var passwordBox = (PasswordBox) passwordBoxes[0];
            var repeatPasswordBox = (PasswordBox) passwordBoxes[1];

            if (string.IsNullOrWhiteSpace(Employee.FirstName))
            {
                ReturnMessage = "Voornaam mag niet leeg zijn.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(Employee.LastName))
            {
                ReturnMessage = "Achternaam mag niet leeg zijn.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(Employee.Address.Street))
            {
                ReturnMessage = "Straatnaam mag niet leeg zijn.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(Employee.Address.City))
            {
                ReturnMessage = "Plaatsnaam mag niet leeg zijn.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(Employee.Address.Number))
            {
                ReturnMessage = "Huisnummer mag niet leeg zijn.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(Employee.Address.ZipCode))
            {
                ReturnMessage = "Postcode mag niet leeg zijn.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(Employee.Address.Country))
            {
                ReturnMessage = "Landnaam mag niet leeg zijn.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(Employee.Address.Province))
            {
                ReturnMessage = "Provincienaam mag niet leeg zijn.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(Employee.TelephoneNumber))
            {
                ReturnMessage = "Telefoonnummer mag niet leeg zijn.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(Employee.Email))
            {
                ReturnMessage = "E-mailadres mag niet leeg zijn.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(passwordBox.Password))
            {
                ReturnMessage = "Wachtwoord mag niet leeg zijn.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(repeatPasswordBox.Password))
            {
                ReturnMessage = "Vul je wachtwoord nogmaals in ter controle.";
                return false;
            }
            if (SelectedJobType == null)
            {
                ReturnMessage = "Functienaam mag niet leeg zijn.";
                return false;
            }

            if (!passwordBox.Password.Equals(repeatPasswordBox.Password))
            {
                ReturnMessage = "De wachtwoorden komen niet overeen.";
                return false;
            }

            return true;
        }

        public override void OnEnter()
        {
            if (Settings.IsOfflineMode)
            {
                MessageBox.Show("Dit scherm is niet beschikbaar in offline mode.");

                _router.GoBack();
            }

            SelectedJobType = null;
            Employee = new Employee
            {
                DateOfBirth = new DateTime(1970, 1, 1),
                DateOfEmployment = DateTime.Today,
                Hash = MD5.Crypt($"Inspector-{DateTime.Now}"),
                Address = new Address
                {
                    Hash = MD5.Crypt($"Address-{DateTime.Now}-111")
                }
            };
        }
    }
}