using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
    public class TaskOverviewViewModel : ParkInspectViewModel
    {
        private readonly ITaskRepository _taskRepository;
        private readonly RouterService _router;

        public ICommand SearchCommand { get; set; }
        public ICommand NewInspectionCommand { get; set; }
        public ICommand EditCommand { get; set; }

        public ObservableCollection<Task> Tasks { get; set; }

        private Task _selectedTask;
        public Task SelectedTask
        {
            get { return _selectedTask; }
            set
            {
                _selectedTask = value;
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

        public TaskOverviewViewModel(ITaskRepository taskRepository, RouterService router)
        {
            _taskRepository = taskRepository;
            _router = router;

            SearchCommand = new RelayCommand(Search);
            NewInspectionCommand = new RelayCommand(NewInspection);
            EditCommand = new RelayCommand(Edit, () => SelectedTask != null);

            Tasks = new ObservableCollection<Task>();
        }

        private void Search()
        {
            Tasks.Clear();

            var search = _taskRepository.All();

            if (!string.IsNullOrWhiteSpace(SearchString))
            {
                search.Where(t => t.Customer != null)
                      .Where(t =>
                                 t.Customer.Name.ToLower().Contains(SearchString.ToLower()) ||
                                 t.Customer.Email.ToLower().Contains(SearchString.ToLower()) ||
                                 t.ParkingLot.Address.City.ToLower().Contains(SearchString.ToLower()) ||
                                 t.ParkingLot.Address.Street.ToLower().Contains(SearchString.ToLower()) ||
                                 t.ParkingLot.Address.ZipCode.ToLower().Contains(SearchString.ToLower()) ||
                                 t.ParkingLot.Address.Number.ToLower().Contains(SearchString.ToLower()) ||
                                 t.ParkingLot.Address.Country.ToLower().Contains(SearchString.ToLower())
                      ).ToList().ForEach(Tasks.Add);
                return;
            }
            
            search.ForEach(Tasks.Add);
        }

        private void Edit()
        {
            if (SelectedTask == null) return;

            dynamic viewBag = new ViewBag();
            viewBag.Task = SelectedTask;

            _router.GoTo(ControlType.TaskDetailsControl, viewBag);
        }

        private void NewInspection()
        {
            _router.GoTo(ControlType.CreateTaskControl);
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