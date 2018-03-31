using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Database;
using GalaSoft.MvvmLight.Command;
using GMap.NET;
using SOh_ParkInspect.Enum;
using SOh_ParkInspect.Helper;
using SOh_ParkInspect.Model;
using SOh_ParkInspect.Repository.Interface;

namespace SOh_ParkInspect.ViewModel.Control
{
    public class HeatMapViewModel : ParkInspectViewModel
    {
        private readonly ITaskRepository _taskRepository;
        private readonly RouterService _router;

        public ICommand OpenTaskCommand { get; set; }
        public ICommand ClearCacheCommand { get; set; }

        public List<ParkingLot> ParkingLots { get; set; }
        public List<Inspection> Tasks { get; set; }

        public Task SelectedTask { get; set; }

        private ObservableCollection<Task> _selectedTasks;
        public ObservableCollection<Task> SelectedTasks
        {
            get { return _selectedTasks; }
            set
            {
                _selectedTasks = value;
                RaisePropertyChanged();
            }
        }

        private ParkingLot _parkingLot;
        public ParkingLot ParkingLot
        {
            get { return _parkingLot; }
            set
            {
                _parkingLot = value;
                SelectedTasks = new ObservableCollection<Task>(
                    _taskRepository.All().Where(e => e.ParkingLot.ID == ParkingLot.ID).ToList()
                );
                RaisePropertyChanged();
            }
        }

        public List<AccessMode> MapModes => new List<AccessMode> { AccessMode.ServerAndCache, AccessMode.CacheOnly };

        private AccessMode _mapMode = AccessMode.ServerAndCache;
        public AccessMode MapMode
        {
            get { return _mapMode; }
            set { GMaps.Instance.Mode = value; }
        }

        public HeatMapViewModel(ITaskRepository taskRepository, IParkingLotRepository lotRepository, RouterService router)
        {
            _taskRepository = taskRepository;
            _router = router;

            ParkingLots = lotRepository.All();
            Tasks = taskRepository.All().SelectMany(x => x.Inspections).ToList();

            OpenTaskCommand = new RelayCommand(OpenTask);
            ClearCacheCommand = new RelayCommand(ClearCache);
        }

        private void OpenTask()
        {
            if (SelectedTask == null) return;

            dynamic viewBag = new ViewBag();
            viewBag.Task = SelectedTask;

            _router.GoTo(ControlType.TaskDetailsControl, viewBag);
        }

        private static void ClearCache()
        {
            GMaps.Instance.PrimaryCache?.DeleteOlderThan(DateTime.MaxValue, null);
            GMaps.Instance.MemoryCache.Clear();
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

    }
}