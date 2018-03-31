using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using Database;
using GalaSoft.MvvmLight.CommandWpf;
using SOh_ParkInspect.Enum;
using SOh_ParkInspect.Helper;
using SOh_ParkInspect.Model;
using SOh_ParkInspect.Repository.Interface;

namespace SOh_ParkInspect.ViewModel.Control
{
    public class AccountViewModel : ParkInspectViewModel
    {
        private readonly IUserRepository _repository;
        private readonly RouterService _router;

        public ICommand ChangePasswordCommand { get; set; }
        public ICommand ChangeUserInfoCommand { get; set; }
        public ICommand ViewInspectionCommand { get; set; }
        public ICommand ViewAvailabilityCommand { get; set; }
        public ICommand ExecuteInspectionCommand { get; set; }

        public ObservableCollection<Inspection> Inspections { get; set; }

        private Employee _user;
        public Employee User
        {
            get { return _user; }
            set
            {
                _user = value;
                RaisePropertyChanged();
            }
        }

        private Inspection _selectedInspection;

        public Inspection SelectedInspection
        {
            get { return _selectedInspection; }
            set
            {
                _selectedInspection = value;
                RaisePropertyChanged();
            }
        }

        public AccountViewModel(RouterService router, IUserRepository repository)
        {
            _router = router;
            _repository = repository;

            ChangePasswordCommand = new RelayCommand(ChangePassword, () => Settings.IsOnlineMode);
            ChangeUserInfoCommand = new RelayCommand(ChangeUserInfo, () => Settings.IsOnlineMode);
            ViewAvailabilityCommand = new RelayCommand(ViewAvailability, () => Settings.IsOnlineMode);
            ExecuteInspectionCommand = new RelayCommand(ExecuteInspection, () => SelectedInspection != null);
            ViewInspectionCommand = new RelayCommand(ViewInspection);

            Inspections = new ObservableCollection<Inspection>();
        }

        private void ChangePassword()
        {
            _router.GoTo(ControlType.ChangePasswordControl);
        }

        private void ChangeUserInfo()
        {
            dynamic viewBag = new ViewBag();
            viewBag.Employee = User;

            _router.GoTo(ControlType.EmployeeInfoControl, viewBag);
        }

        private void ExecuteInspection()
        {
            if (SelectedInspection == null) return;

            dynamic viewBag = new ViewBag();
            viewBag.Inspection = SelectedInspection;

            _router.GoTo(ControlType.ExecuteInspectionControl, viewBag);
        }

        private void ViewInspection()
        {
            if (SelectedInspection == null) return;

            dynamic viewBag = new ViewBag();
            viewBag.Inspection = SelectedInspection.Task;

            _router.GoTo(ControlType.TaskDetailsControl, viewBag);
        }

        private void ViewAvailability()
        {
            _router.GoTo(ControlType.EmployeeAvailabilityControl);
        }

        public override void OnEnter()
        {
            User = _repository.Find(Settings.CurrentUser.ID);

            Inspections.Clear();

            User.InspectionInspectors
                .Where(x => x.Inspection.DateTimePlanned > DateTime.Now.AddDays(-7))
                .Select(x => x.Inspection).ToList().ForEach(Inspections.Add);
        }
    }
}