using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Database;
using GalaSoft.MvvmLight.CommandWpf;
using SOh_ParkInspect.Helper;
using SOh_ParkInspect.Model;
using SOh_ParkInspect.Repository.Interface;
using static CryptSharp.Crypter;

namespace SOh_ParkInspect.ViewModel.Control
{
    public class ClientInfoViewModel : ParkInspectViewModel
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly RouterService _router;

        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        private Customer _customer;

        public Customer Customer
        {
            get { return _customer; }
            set
            {
                _customer = value;
                RaisePropertyChanged();
            }
        }

        private string _errorMessage;

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                RaisePropertyChanged();
            }
        }

        public ClientInfoViewModel(ICustomerRepository customerRepository, RouterService router)
        {
            _customerRepository = customerRepository;
            _router = router;

            SaveCommand = new RelayCommand(Save);
            CancelCommand = new RelayCommand(_router.GoBack);
        }

        private void Save()
        {
            if (!CanSave()) return;
            if (!_customerRepository.Update(Customer))
            {
                MessageBox.Show("Kon klant niet opslaan!");
                return;
            }

            MessageBox.Show("Klant opgeslagen!");
            _router.GoBack();
        }

        private bool CanSave()
        {
            Customer.Name = Customer.Name.Trim();
            Customer.Address.City = Customer.Address.City.Trim();
            Customer.Address.Country = Customer.Address.Country.Trim();
            Customer.Address.Number = Customer.Address.Number.Trim();
            Customer.Address.Street = Customer.Address.Street.Trim();
            Customer.Address.ZipCode = Customer.Address.ZipCode.Trim();

            if (string.IsNullOrWhiteSpace(Customer.Name))
            {
                ErrorMessage = "Klantnaam mag niet leeg zijn.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(Customer.Address.City))
            {
                ErrorMessage = "Plaatsnaam van adres mag niet leeg zijn.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(Customer.Address.Country))
            {
                ErrorMessage = "Land van adres mag niet leeg zijn.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(Customer.Address.Number))
            {
                ErrorMessage = "Huisnummer van adres mag niet leeg zijn.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(Customer.Address.Street))
            {
                ErrorMessage = "Straatnaam van adres mag niet leeg zijn.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(Customer.Address.ZipCode))
            {
                ErrorMessage = "Postcode van adres mag niet leeg zijn.";
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
                return;
            }

            Customer = ViewBag?.Customer == null ? new Customer
            {
                Name = string.Empty,
                Contact = string.Empty,
                Email = string.Empty,
                PhoneNumber = string.Empty,
                Remarks = String.Empty,
                Hash = MD5.Crypt($"{DateTime.Now}-Customer"),
                Address = new Address
                {
                    Hash = MD5.Crypt($"{DateTime.Now}-{DateTime.UtcNow}-Address"),
                    Remarks = string.Empty,
                    City = string.Empty,
                    Country = string.Empty,
                    Number = string.Empty,
                    Province = string.Empty,
                    Street = string.Empty,
                    ZipCode = string.Empty
                }
            } : ViewBag.Customer;
        }
    }
}