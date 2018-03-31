using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Database;
using GalaSoft.MvvmLight.CommandWpf;
using SOh_ParkInspect.Helper;
using SOh_ParkInspect.Repository.Interface;

namespace SOh_ParkInspect.ViewModel.Control
{
    public class LinkInspectorViewModel : ParkInspectViewModel
    {
        private readonly IInspectionRepository _inspectionRepository;
        private readonly IUserRepository _userRepository;
        private readonly RouterService _router;
        private Inspection _inspection;

        private Employee _selectedInspector;
        private Employee _unSelectedInspector;

        public LinkInspectorViewModel(IUserRepository userRepository, IInspectionRepository inspectionRepository, RouterService router)
        {
            _inspectionRepository = inspectionRepository;
            _userRepository = userRepository;

            _router = router;

            SelectedInspectors = new ObservableCollection<Employee>();
            UnSelectedInspectors = new ObservableCollection<Employee>();

            AddCommand = new RelayCommand(Add, () => UnSelectedInspector != null);
            RemoveCommand = new RelayCommand(Remove, () => SelectedInspector != null);
            SaveCommand = new RelayCommand(Save);
            CancelCommand = new RelayCommand(_router.GoBack);
        }

        public ObservableCollection<Employee> SelectedInspectors { get; set; }
        public ObservableCollection<Employee> UnSelectedInspectors { get; set; }

        public Employee SelectedInspector
        {
            get { return _selectedInspector; }
            set
            {
                _selectedInspector = value;
                RaisePropertyChanged();
            }
        }

        public Employee UnSelectedInspector
        {
            get { return _unSelectedInspector; }
            set
            {
                _unSelectedInspector = value;
                RaisePropertyChanged();
            }
        }

        public ICommand AddCommand { get; set; }
        public ICommand RemoveCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        
        private void Add()
        {
            SelectedInspectors.Add(UnSelectedInspector);
            UnSelectedInspectors.Remove(UnSelectedInspector);
            UnSelectedInspector = null;
        }

        private void Remove()
        {
            UnSelectedInspectors.Add(SelectedInspector);
            SelectedInspectors.Remove(SelectedInspector);
            SelectedInspector = null;
        }

        private void Save()
        {
            var iiList = new List<InspectionInspector>();

            foreach (var e in SelectedInspectors)
            {
                var ii = new InspectionInspector
                {
                    Employee = _userRepository.Find(e.ID),
                    Inspection = _inspectionRepository.Find(_inspection.ID),
                    LastUpdated = DateTime.Now
                };
                e.InspectionInspectors.Add(ii);
                _userRepository.Update(e);
                iiList.Add(ii);
            }
            _inspection.InspectionInspectors = iiList;
            
            _inspectionRepository.Update(_inspection);

            MessageBox.Show("De wijzigingen zijn opgeslagen!");
            _router.GoBack();
        }

        public override void OnEnter()
        {
            if (ViewBag?.Inspection == null)
            {
                MessageBox.Show("Er is geen inspectie geselecteerd!");
                _router.GoBack();
                return;
            }

            _inspection = ViewBag.Inspection;

            var employees = _inspection.InspectionInspectors?.Select(x => x.Employee).ToList() ?? new List<Employee>();


            SelectedInspectors.Clear();
            UnSelectedInspectors.Clear();

            _userRepository.All().ForEach(UnSelectedInspectors.Add);

            UnSelectedInspectors
                .Where(x => employees.Contains(x)).ToList()
                .ForEach(x =>
                         {
                             SelectedInspectors.Add(x);
                             UnSelectedInspectors.Remove(x);
                         });
        }
    }
}