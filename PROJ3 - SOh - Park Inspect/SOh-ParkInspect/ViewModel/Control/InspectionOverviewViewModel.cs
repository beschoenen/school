using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
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
    public class InspectionOverviewViewModel : ParkInspectViewModel
    {
        private readonly IUserRepository _userRepository;
        private readonly RouterService _router;

        public ICommand ExecuteInspectionCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand DownloadInspectionCommand { get; set; }

        public ObservableCollection<Inspection> Inspections { get; set; }

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

        public InspectionOverviewViewModel(IUserRepository userRepository, RouterService router)
        {
            _router = router;
            _userRepository = userRepository;

            SearchCommand = new RelayCommand(Search);
            DownloadInspectionCommand = new RelayCommand(DownloadInspection, CanDownloadInspection);
            ExecuteInspectionCommand = new RelayCommand(ExecuteInspection, CanAnswerInspection);

            Inspections = new ObservableCollection<Inspection>();
        }

        private bool CanDownloadInspection()
        {
            return SelectedInspection != null && Settings.IsOnlineMode && !DbSync.HasDownloadedInspection(SelectedInspection) && CanAnswerInspection();
        }

        private bool CanAnswerInspection()
        {
            return !(SelectedInspection?.Answers?.Any(x => x.Employee.Hash == Settings.CurrentUser.Hash)) ?? false;
        }

        private void DownloadInspection()
        {
            if (DbSync.HasInspectionLocal)
            {
                var response = MessageBox.Show("Er is al een inspectie gedownload, als u doorgaat zal uw lokale inspectie worden verwijderd!", "Waarschuwing", MessageBoxButton.OKCancel, MessageBoxImage.Asterisk);

                if (response != MessageBoxResult.OK) return;
            }

            Mouse.OverrideCursor = Cursors.Wait;

            if (DbSync.Download(SelectedInspection.ID))
            {
                MessageBox.Show("De inspectie is gedownload.");
            }

            Mouse.OverrideCursor = null;
        }

        private void Search()
        {
            Inspections.Clear();

            var search = _userRepository.Find(Settings.CurrentUser.ID).InspectionInspectors
                                        .Where(x => x.Inspection.DateTimePlanned > DateTime.Now.AddDays(-7))
                                        .Select(x => x.Inspection).ToList();

            if (!string.IsNullOrWhiteSpace(SearchString))
            {
                search.Where(t =>
                                 t.Task.Customer.Name.ToLower().Contains(SearchString.ToLower()) ||
                                 t.Task.Customer.Email.ToLower().Contains(SearchString.ToLower()) ||
                                 t.Task.ParkingLot.Address.City.ToLower().Contains(SearchString.ToLower()) ||
                                 t.Task.ParkingLot.Address.Street.ToLower().Contains(SearchString.ToLower()) ||
                                 t.Task.ParkingLot.Address.ZipCode.ToLower().Contains(SearchString.ToLower()) ||
                                 t.Task.ParkingLot.Address.Number.ToLower().Contains(SearchString.ToLower()) ||
                                 t.Task.ParkingLot.Address.Country.ToLower().Contains(SearchString.ToLower())
                ).ToList().ForEach(Inspections.Add);
                return;
            }

            search.ForEach(Inspections.Add);
        }

        private void ExecuteInspection()
        {
            dynamic viewBag = new ViewBag();
            viewBag.Inspection = SelectedInspection;

            _router.GoTo(ControlType.ExecuteInspectionControl, viewBag);
        }

        public override void OnEnter()
        {
            Search();
        }
    }
}