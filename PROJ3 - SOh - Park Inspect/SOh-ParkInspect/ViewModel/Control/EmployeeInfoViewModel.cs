using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Database;
using GalaSoft.MvvmLight.Command;
using SOh_ParkInspect.Enum;
using SOh_ParkInspect.Helper;
using SOh_ParkInspect.Model;
using SOh_ParkInspect.Repository.Interface;

namespace SOh_ParkInspect.ViewModel.Control
{
    public class EmployeeInfoViewModel : ParkInspectViewModel
    {
        private readonly IJobTitleRepository _jobTitleRepository;
        private readonly IUserRepository _userRepository;
        private readonly RouterService _router;

        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public ObservableCollection<JobTitle> JobTitles { get; set; }

        private JobTitle _selectedJobTitle;
        public JobTitle SelectedJobTitle
        {
            get { return _selectedJobTitle; }
            set
            {
                _selectedJobTitle = value;
                RaisePropertyChanged();
            }
        }

        private Employee _selectedEmployee;
        public Employee SelectedEmployee
        {
            get { return _selectedEmployee; }
            set
            {
                _selectedEmployee = value;
                RaisePropertyChanged();
            }
        }

        public EmployeeInfoViewModel(IUserRepository userRepository, IJobTitleRepository jobTitleRepository, RouterService router)
        {
            _userRepository = userRepository;
            _jobTitleRepository = jobTitleRepository;
            _router = router;

            JobTitles = new ObservableCollection<JobTitle>();

            SaveCommand = new RelayCommand(SaveUser);
            CancelCommand = new RelayCommand(_router.GoBack);
            JobTitles = new ObservableCollection<JobTitle>(_jobTitleRepository.All());
        }

        public override void OnEnter()
        {
            if (Settings.IsOfflineMode)
            {
                MessageBox.Show("Dit scherm is niet beschikbaar in offline mode.");
                _router.GoBack();
                return;
            }

            SelectedEmployee = ViewBag.Employee;

            if (SelectedEmployee == null)
            {
                MessageBox.Show("Geen medewerker geselecteerd.");
                _router.GoBack();
                return;
            }

            JobTitles.Clear();
            _jobTitleRepository.All().ForEach(JobTitles.Add);

            SelectedJobTitle = JobTitles.FirstOrDefault(j => j.Name.Equals(SelectedEmployee.JobTitle.Name));
        }

        private void SaveUser()
        {
            var error = CanSave();
            if (error != string.Empty)
            {
                MessageBox.Show(error);
                return;
            }

            SelectedEmployee.JobTitle = SelectedJobTitle;
            if (_userRepository.Update(SelectedEmployee))
            {
                MessageBox.Show("Wijzigingen zijn opgeslagen.");

                _router.GoTo(ControlType.AccountControl, null, true);
                return;
            }

            MessageBox.Show("Wijzigingen zijn niet opgeslagen.");
        }

        private string CanSave()
        {
            if (string.IsNullOrWhiteSpace(SelectedEmployee.FirstName) ||
                string.IsNullOrWhiteSpace(SelectedEmployee.LastName) ||
                string.IsNullOrWhiteSpace(SelectedEmployee.Address.Street) ||
                string.IsNullOrWhiteSpace(SelectedEmployee.Address.Number) ||
                string.IsNullOrWhiteSpace(SelectedEmployee.Address.ZipCode) ||
                string.IsNullOrWhiteSpace(SelectedEmployee.Address.City) ||
                string.IsNullOrWhiteSpace(SelectedEmployee.Address.Province) ||
                string.IsNullOrWhiteSpace(SelectedEmployee.Address.Country) ||
                string.IsNullOrWhiteSpace(SelectedEmployee.TelephoneNumber) ||
                string.IsNullOrWhiteSpace(SelectedEmployee.Email) ||
                SelectedEmployee.DateOfEmployment == null ||
                SelectedJobTitle == null)
            {
                return "Niet alle velden zijn ingevuld!";
            }

            if (SelectedJobTitle == null)
            {
                return "Functie mag niet leeg zijn!";
            }

            return string.Empty;
        }
    }
}
