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
    public class UserManagementViewModel : ParkInspectViewModel
    {
        private readonly IUserRepository _repository;
        private readonly RouterService _router;

        public ICommand AddUserCommand { get; set; }
        public ICommand EditUserCommand { get; set; }
        public ICommand DeleteUserCommand { get; set; }
        public ICommand SearchCommand { get; set; }

        public ObservableCollection<Employee> Employees { get; set; }

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

        private string _searchString = "";

        public string SearchString
        {
            get { return _searchString; }
            set
            {
                _searchString = value;
                RaisePropertyChanged();
            }
        }

        public UserManagementViewModel(IUserRepository repository, RouterService router)
        {
            _repository = repository;
            _router = router;

            Employees = new ObservableCollection<Employee>();

            AddUserCommand = new RelayCommand(AddUser);
            EditUserCommand = new RelayCommand(EditUser);
            DeleteUserCommand = new RelayCommand(DeleteUser);
            SearchCommand = new RelayCommand(Search);
        }

        private void Search()
        {
            Employees.Clear();

            var search = _repository.All();

            if (!string.IsNullOrWhiteSpace(SearchString))
            {
                search.Where(u => $"{u.FirstName} {u.LastName}".Contains(SearchString)).ToList().ForEach(Employees.Add);
                return;
            }

            search.ForEach(Employees.Add);
        }

        private void AddUser()
        {
            _router.GoTo(ControlType.RegisterInspectorControl);
        }

        private void EditUser()
        {
            if (SelectedEmployee == null) return;

            dynamic viewBag = new ViewBag();
            viewBag.Employee = SelectedEmployee;

            _router.GoTo(ControlType.EmployeeInfoControl, viewBag);
        }

        private void DeleteUser()
        {
            if (SelectedEmployee == null) return;

            if (MessageBox.Show("Weet u zeker dat u deze gebruiker wilt verwijderen", "Verwijderen", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
            {
                return;
            }

            if (!_repository.Delete(SelectedEmployee))
            {
                MessageBox.Show("Geselecteerde medewerker kon niet worden verwijderd.");
                return;
            }

            Employees.Remove(SelectedEmployee);
            SelectedEmployee = null;
        }

        public override void OnEnter()
        {
            if (Settings.IsOfflineMode)
            {
                MessageBox.Show("Dit scherm is niet beschikbaar in offline mode.");

                _router.GoBack();
                return;
            }

            Search();
        }
    }
}