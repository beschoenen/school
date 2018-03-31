using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using Database;
using GalaSoft.MvvmLight.CommandWpf;
using SOh_ParkInspect.Enum;
using SOh_ParkInspect.Helper;
using SOh_ParkInspect.Model;
using SOh_ParkInspect.Repository.Interface;
using static CryptSharp.Crypter;

namespace SOh_ParkInspect.ViewModel.Control
{
    public class CreateTaskViewModel : ParkInspectViewModel
    {
        private readonly RouterService _router;
        private readonly IChecklistRepository _checklistRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IParkingLotRepository _parkingLotRepository;
        private readonly ITaskRepository _taskRepository;

        private bool _isEditing;

        private bool _canEditStartDate;
        private bool _isRepeating;
        private string _saveErrorMessage;
        private string _searchCustomerText = "";
        private string _searchLocationText = "";
        private string _searchChecklistText = "";
        private Checklist _selectedChecklist;
        private Customer _selectedCustomer;
        private ParkingLot _selectedParkingLot;
        private TaskSchedule _selectedTaskSchedule;

        public string SaveErrorMessage
        {
            get { return _saveErrorMessage; }
            set
            {
                _saveErrorMessage = value;
                RaisePropertyChanged();
            }
        }

        public ParkingLot SelectedParkingLot
        {
            get { return _selectedParkingLot; }
            set
            {
                _selectedParkingLot = value;
                RaisePropertyChanged();
            }
        }

        public Customer SelectedCustomer
        {
            get { return _selectedCustomer; }
            set
            {
                _selectedCustomer = value;
                RaisePropertyChanged();
            }
        }

        public Task SelectedTask { get; set; }

        public Checklist SelectedChecklist
        {
            get { return _selectedChecklist; }
            set
            {
                _selectedChecklist = value;
                RaisePropertyChanged();
            }
        }

        public string SearchCustomerText
        {
            get { return _searchCustomerText; }
            set
            {
                _searchCustomerText = value;
                RaisePropertyChanged();
            }
        }

        public string SearchChecklistText
        {
            get { return _searchChecklistText; }
            set
            {
                _searchChecklistText = value;
                RaisePropertyChanged();
            }
        }

        public string SearchLocationText
        {
            get { return _searchLocationText; }
            set
            {
                _searchLocationText = value;
                RaisePropertyChanged();
            }
        }

        public DateTime StartDate
        {
            get { return SelectedTask?.DateTimeStart ?? DateTime.Today; }
            set
            {
                if (value >= SetMidnight(EndDate) && IsRepeating)
                {
                    MessageBox.Show("Startdatum mag niet na de einddatum liggen!");
                }
                else
                {
                    SelectedTask.DateTimeStart = value;
                    RaisePropertyChanged();
                }
            }
        }

        public DateTime EndDate
        {
            get { return SelectedTask?.DateTimeEnd ?? DateTime.Today.AddDays(1); }
            set
            {
                if (value <= SetMidnight(StartDate))
                {
                    MessageBox.Show("Einddatum mag niet voor de startdatum liggen!");
                }
                else
                {
                    SelectedTask.DateTimeEnd = value;
                    RaisePropertyChanged();
                }
            }
        }

        public bool CanEditStartDate
        {
            get { return _canEditStartDate; }
            set
            {
                _canEditStartDate = value;
                RaisePropertyChanged();
            }
        }

        public bool IsRepeating
        {
            get { return _isRepeating; }
            set
            {
                if (!value)
                {
                    InspectionsDates.Clear();
                    InspectionsDates.Add(StartDate);
                }
                else
                {
                    EndDate = SetMidnight(StartDate).AddDays(1);
                }
                _isRepeating = value;
                RaisePropertyChanged();
            }
        }

        public TaskSchedule SelectedTaskSchedule
        {
            get { return _selectedTaskSchedule; }
            set
            {
                _selectedTaskSchedule = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<IntervalFrequency> IntervalFrequencies { get; set; }
        public ObservableCollection<Customer> Customers { get; set; }
        public ObservableCollection<DateTime> InspectionsDates { get; set; }
        public ObservableCollection<ParkingLot> ParkingLots { get; set; }
        public ObservableCollection<Checklist> Checklists { get; set; }
        public ObservableCollection<TaskSchedule> TaskSchedules { get; set; }

        public ICommand GenerateInspectionDatesCommand { get; set; }
        public ICommand SaveInspectionCommand { get; set; }
        public ICommand CancelInspectionCommand { get; set; }

        public ICommand ClearTaskScheduleCommand { get; set; }
        public ICommand AddTaskScheduleCommand { get; set; }
        public ICommand RemoveTaskScheduleCommand { get; set; }

        public ICommand SearchCommand { get; set; }
        public ICommand ClearSearchCommand { get; set; }

        public ICommand NewChecklistCommand { get; set; }
        public ICommand NewCustomerCommand { get; set; }
        public ICommand NewParkingLotCommand { get; set; }
        public ICommand EditChecklistCommand { get; set; }
        public ICommand EditCustomerCommand { get; set; }
        public ICommand EditParkingLotCommand { get; set; }

        public CreateTaskViewModel(RouterService router,
                                   IIntervalFrequencyRepository intervalFrequencyRepository,
                                   ICustomerRepository customerRepository,
                                   ITaskRepository taskRepository,
                                   IChecklistRepository checklistRepository,
                                   IParkingLotRepository parkingLotRepository)
        {
            _customerRepository = customerRepository;
            _taskRepository = taskRepository;
            _checklistRepository = checklistRepository;
            _parkingLotRepository = parkingLotRepository;
            _router = router;

            GenerateInspectionDatesCommand = new RelayCommand(CalculateInspectionDates);
            SaveInspectionCommand = new RelayCommand(SaveInspection);
            CancelInspectionCommand = new RelayCommand(_router.GoBack);

            ClearTaskScheduleCommand = new RelayCommand(ClearTaskSchedule);
            AddTaskScheduleCommand = new RelayCommand(AddTaskSchedule);
            RemoveTaskScheduleCommand = new RelayCommand(RemoveTaskSchedule);

            SearchCommand = new RelayCommand<string>(Search);
            ClearSearchCommand = new RelayCommand<string>(ClearSearch);

            NewChecklistCommand = new RelayCommand(NewChecklist);
            NewCustomerCommand = new RelayCommand(NewCustomer);
            NewParkingLotCommand = new RelayCommand(NewParkingLot);
            EditChecklistCommand = new RelayCommand(EditChecklist);
            EditCustomerCommand = new RelayCommand(EditCustomer);
            EditParkingLotCommand = new RelayCommand(EditParkingLot);

            IntervalFrequencies = new ObservableCollection<IntervalFrequency>(intervalFrequencyRepository.All());
            Customers = new ObservableCollection<Customer>();
            InspectionsDates = new ObservableCollection<DateTime>();
            ParkingLots = new ObservableCollection<ParkingLot>();
            Checklists = new ObservableCollection<Checklist>();
            TaskSchedules = new ObservableCollection<TaskSchedule>();
        }

        private void EditCustomer()
        {
            if (SelectedCustomer == null) return;

            _isEditing = true;
            dynamic viewBag = new ViewBag();
            viewBag.Customer = SelectedCustomer;

            _router.GoTo(ControlType.ClientInfoControl, viewBag);
        }

        private void NewCustomer()
        {
            _isEditing = true;
            _router.GoTo(ControlType.ClientInfoControl);
        }

        private void EditParkingLot()
        {
            if (SelectedParkingLot == null) return;

            _isEditing = true;
            dynamic viewBag = new ViewBag();
            viewBag.ParkingLot = SelectedParkingLot;
            
            _router.GoTo(ControlType.LocationInfoControl, viewBag);
        }

        private void NewParkingLot()
        {
            _isEditing = true;
            _router.GoTo(ControlType.LocationInfoControl);
        }

        private void EditChecklist()
        {
            if (SelectedChecklist == null) return;

            _isEditing = true;
            dynamic viewBag = new ViewBag();
            viewBag.Template = SelectedChecklist;

            _router.GoTo(ControlType.EditTemplateControl, viewBag);
        }

        private void NewChecklist()
        {
            _isEditing = true;
            _router.GoTo(ControlType.EditTemplateControl);
        }

        private void ClearTaskSchedule()
        {
            TaskSchedules.Clear();
            CanEditStartDate = true;
        }

        private void AddTaskSchedule()
        {
            var taskSchedule = new TaskSchedule
            {
                IntervalFrequency = IntervalFrequencies.FirstOrDefault(f => f.Name == IntervalFrequency.Day),
                InspectionDateTime = StartDate,
                Interval = 1,
                Task = SelectedTask
            };

            TaskSchedules.Add(taskSchedule);

            SelectedTaskSchedule = taskSchedule;
            CalculateInspectionDates();
            CanEditStartDate = false;
        }

        private void RemoveTaskSchedule()
        {
            if (SelectedTaskSchedule == null) return;

            TaskSchedules.Remove(SelectedTaskSchedule);
            CalculateInspectionDates();
            if (TaskSchedules.Count <= 0) CanEditStartDate = true;
        }

        public void SaveInspection()
        {
            if (!CheckInput()) return;

            if (SelectedTask == null) SelectedTask = new Task();

            SelectedTask.Checklist = SelectedChecklist;
            SelectedTask.Customer = SelectedCustomer;
            SelectedTask.DateTimeStart = StartDate;
            SelectedTask.DatetimeCreated = DateTime.Now;
            SelectedTask.Inspections = new List<Inspection>();
            InspectionsDates.ToList().ForEach(i => SelectedTask.Inspections.Add(NewInspectionObject(i)));
            SelectedTask.ParkingLot = SelectedParkingLot;
            SelectedTask.Remarks = SelectedTask.Remarks.Trim();
            SelectedTask.TaskSchedules = TaskSchedules.ToList();
            SelectedTask.DateTimeEnd = IsRepeating ? EndDate : SelectedTask.Inspections.Max(x => x.DateTimePlanned);

            if (_taskRepository.Add(SelectedTask))
            {
                MessageBox.Show($"Inspectieopdracht voor klant {SelectedCustomer.Name} met {SelectedTask.Inspections.Count} inspecties is opgeslagen");
                _router.GoTo(ControlType.TaskOverviewControl);
                return;
            }

            SaveErrorMessage = "Opslaan van inspectieopdracht mislukt.";
        }

        private Inspection NewInspectionObject(DateTime dateTime)
        {
            return new Inspection
            {
                DateTimePlanned = dateTime,
                Checklist = SelectedChecklist,
                Task = SelectedTask,
                Hash = MD5.Crypt($"{dateTime.ToLongDateString()}-{DateTime.Now}")
            };
        }

        public bool CheckInput()
        {
            SaveErrorMessage = string.Empty;
            CalculateInspectionDates();

            if (SelectedCustomer == null)
            {
                SaveErrorMessage = "U heeft geen klant geselecteerd.";
                return false;
            }
            if (InspectionsDates.Count <= 0)
            {
                SaveErrorMessage = "Er zijn geen inspecties ingepland.";
                return false;
            }
            if (SelectedParkingLot == null)
            {
                SaveErrorMessage = "U heeft geen te controleren parkeerplaats geselecteerd.";
                return false;
            }
            if (SelectedChecklist == null)
            {
                SaveErrorMessage = "U heeft nog geen vragenljst geselecteerd.";
                return false;
            }
            return true;
        }

        public void CalculateInspectionDates()
        {
            var dateTimes = new List<DateTime>();

            InspectionsDates.Clear();
            if (!IsRepeating) InspectionsDates.Add(StartDate);
            else
            {
                foreach (var schedule in TaskSchedules)
                {
                    var dt = schedule.InspectionDateTime;

                    while (dt <= EndDate)
                    {
                        if (!dateTimes.Any(t => t.Equals(dt))) dateTimes.Add(dt);
                        dt = AddTimeSpan(dt, schedule.IntervalFrequency.Name, schedule.Interval);
                    }
                }
                dateTimes.Sort();
                dateTimes.ForEach(i => InspectionsDates.Add(i));
            }
        }

        public void Search(string listType)
        {
            if (listType == "checklist")
            {
                Checklists.Clear();
                _checklistRepository.All()
                                    .Where(c =>
                                               c.Name.ToLower().Contains(SearchChecklistText.ToLower()) ||
                                               c.Remarks.ToLower().Contains(SearchChecklistText.ToLower())
                                    ).ToList().ForEach(c => Checklists.Add(c));
            }
            else if (listType == "parkinglot")
            {
                ParkingLots.Clear();
                _parkingLotRepository.All()
                                     .Where(p =>
                                                p.Address.City.ToLower().Contains(SearchLocationText.ToLower()) ||
                                                p.Address.Country.ToLower().Contains(SearchLocationText.ToLower()) ||
                                                p.Address.ZipCode.ToLower().Contains(SearchLocationText.ToLower()) ||
                                                p.Address.Street.ToLower().Contains(SearchLocationText.ToLower())
                                     ).ToList().ForEach(p => ParkingLots.Add(p));
                SelectFirstComboBoxItem();
            }
            else if (listType == "customer")
            {
                Customers.Clear();
                _customerRepository.All()
                                   .Where(c =>
                                              c.Name.ToLower().Contains(SearchCustomerText.ToLower()) ||
                                              c.Email.ToLower().Contains(SearchCustomerText.ToLower()) ||
                                              c.Contact.ToLower().Contains(SearchCustomerText.ToLower()) ||
                                              c.PhoneNumber.ToLower().Contains(SearchCustomerText.ToLower()) ||
                                              c.Address.Street.ToLower().Contains(SearchCustomerText.ToLower()) ||
                                              c.Address.ZipCode.ToLower().Contains(SearchCustomerText.ToLower()) ||
                                              c.Address.City.ToLower().Contains(SearchCustomerText.ToLower()) ||
                                              c.Address.Country.ToLower().Contains(SearchCustomerText.ToLower())
                                   ).ToList().ForEach(c => Customers.Add(c));
            }
            SelectFirstComboBoxItem();
        }

        public void ClearSearch(string type)
        {
            switch (type)
            {
                case "checklist":
                    SearchChecklistText = string.Empty;
                    Checklists.Clear();
                    _checklistRepository.All().ForEach(c => Checklists.Add(c));
                    break;
                case "customer":
                    SearchCustomerText = string.Empty;
                    Customers.Clear();
                    _customerRepository.All().ForEach(c => Customers.Add(c));
                    break;
                case "parkinglot":
                    SearchLocationText = string.Empty;
                    ParkingLots.Clear();
                    _parkingLotRepository.All().ForEach(p => ParkingLots.Add(p));
                    break;
            }

            SelectFirstComboBoxItem();
        }

        private void SelectFirstComboBoxItem()
        {
            if (Customers.Count > 0 && SelectedCustomer == null) SelectedCustomer = Customers[0];
            if (ParkingLots.Count > 0 && SelectedParkingLot == null) SelectedParkingLot = ParkingLots[0];
            if (Checklists.Count > 0 && SelectedChecklist == null) SelectedChecklist = Checklists[0];
        }

        public override void OnEnter()
        {
            if (Settings.IsOfflineMode)
            {
                MessageBox.Show("Dit scherm is niet beschikbaar in offline mode.");

                _router.GoBack();
                return;
            }

            if (_isEditing)
            {
                _isEditing = false;
                return;
            }

            if (ViewBag?.Task != null)
            {
                SelectedTask = ViewBag.Task;
            }
            else
            {
                SelectedTask = new Task
                {
                    Inspections = new List<Inspection>(),
                    TaskSchedules = new List<TaskSchedule>(),
                    DateTimeStart = DateTime.Today,
                    Hash = MD5.Crypt($"{DateTime.Now.ToLongDateString()}-{SelectedTask?.GetHashCode()}")
                };

                EndDate = DateTime.Today.AddDays(1);
                IsRepeating = false;
                CanEditStartDate = true;
            }

            if (SelectedTask.Remarks == null) SelectedTask.Remarks = "";

            SelectedTask.TaskSchedules.ToList().ForEach(t => TaskSchedules.Add(t));

            Customers.Clear();
            ParkingLots.Clear();
            Checklists.Clear();

            StartDate = DateTime.Today;
            EndDate = DateTime.Today.AddDays(1);

            _customerRepository.All().ForEach(Customers.Add);
            _checklistRepository.All().ForEach(Checklists.Add);
            _parkingLotRepository.All().ForEach(ParkingLots.Add);

            CalculateInspectionDates();
            SelectFirstComboBoxItem();
        }

        private static DateTime SetMidnight(DateTime dateTime)
        {
            return dateTime.Date;
        }

        public static DateTime AddTimeSpan(DateTime dateTime, string frequency, int interval)
        {
            switch (frequency)
            {
                case IntervalFrequency.Minute:
                    dateTime = dateTime.AddMinutes(interval);
                    break;
                case IntervalFrequency.Hour:
                    dateTime = dateTime.AddHours(interval);
                    break;
                case IntervalFrequency.Day:
                    dateTime = dateTime.AddDays(interval);
                    break;
                case IntervalFrequency.Week:
                    dateTime = dateTime.AddDays(7*interval);
                    break;
                case IntervalFrequency.Month:
                    dateTime = dateTime.AddMonths(interval);
                    break;
                case IntervalFrequency.Year:
                    dateTime = dateTime.AddYears(interval);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return dateTime;
        }
    }
}