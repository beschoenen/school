using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using Database;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using SOh_ParkInspect.Repository.Interface;

namespace SOh_ParkInspect.ViewModel.ManagementReport
{
    public class InspectionDurationViewModel : ViewModelBase
    {
        private readonly IAddressRepository _addressRepository;
        private readonly ITaskRepository _taskRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICustomerRepository _customerRepository;

        private DateTime _beginDateTime;
        private WebBrowser _browser;
        private DateTime _endDateTime;

        private string _selectedChart;

        private Employee _selectedEmployee;
        private string _selectedRegion;

        private Employee _selectedRemoveEmployee;
        private string currentUri;
        public ObservableCollection<string> ChartTypes { get; set; }
        public ICommand GenerateCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand RemoveCommand { get; set; }
        public ObservableCollection<string> Regions { get; set; }
        public ObservableCollection<Employee> Inspectors { get; set; }
        public ObservableCollection<Employee> SelectedInspectors { get; set; }

        public ObservableCollection<Customer> Customers { get; set; }

        public Employee SelectedEmployee
        {
            get { return _selectedEmployee; }
            set
            {
                _selectedEmployee = value;
                RaisePropertyChanged();
            }
        }

        public Employee SelectedRemoveEmployee
        {
            get { return _selectedRemoveEmployee; }
            set
            {
                _selectedRemoveEmployee = value;
                RaisePropertyChanged();
            }
        }

        public DateTime EndDateTime
        {
            get { return _endDateTime; }
            set
            {
                _endDateTime = value;
                base.RaisePropertyChanged();
            }
        }

        public DateTime BeginDateTime
        {
            get { return _beginDateTime; }
            set
            {
                _beginDateTime = value;
                base.RaisePropertyChanged();
            }
        }

        public string SelectedRegion
        {
            get { return _selectedRegion; }
            set
            {
                _selectedRegion = value;
                base.RaisePropertyChanged();
            }
        }

        public string SelectedChart
        {
            get { return _selectedChart; }
            set
            {
                _selectedChart = value;
                base.RaisePropertyChanged();
            }
        }

        public string WebBrowserUri
        {
            get { return currentUri; }
            set
            {
                currentUri = value;
                RaisePropertyChanged("WebBrowserUri");
            }
        }

        private bool _selectAllEmployees;

        private bool _selectAllRegions;

        public bool SelectAllEmployees
        {
            get { return _selectAllEmployees; }
            set
            {
                _selectAllEmployees = value;
                RaisePropertyChanged();
            }
        }

        public bool SelectAllRegions
        {
            get { return _selectAllRegions; }
            set
            {
                _selectAllRegions = value;
                RaisePropertyChanged();
            }
        }

        private bool _selectAllCustomers;


        public bool SelectAllCustomers
        {
            get { return _selectAllCustomers; }
            set
            {
                _selectAllCustomers = value;
                RaisePropertyChanged();
            }
        }
        
        private Customer _selectedCustomer;

        public Customer SelectedCustomer
        {
            get { return _selectedCustomer; }
            set
            {
                _selectedCustomer = value;
                base.RaisePropertyChanged();
            }
        }

        public InspectionDurationViewModel(ITaskRepository taskRepository, IUserRepository userRepository, IAddressRepository addressRepository, ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
            _taskRepository = taskRepository;
            _userRepository = userRepository;
            _addressRepository = addressRepository;
            BeginDateTime = new DateTime();
            EndDateTime = DateTime.Now;
            Regions = new ObservableCollection<string>();
            foreach (var address in _addressRepository.All())
            {
                if (!Regions.Contains(address.Province))
                {
                    Regions.Add(address.Province);
                }
            }

            Customers = new ObservableCollection<Customer>(customerRepository.All());
            Inspectors = new ObservableCollection<Employee>(_userRepository.All());
            SelectedInspectors = new ObservableCollection<Employee>();

            ChartTypes = new ObservableCollection<string> { "Barchart", "Piechart" };

            var curDir = Directory.GetCurrentDirectory();
            WebBrowserUri = $"file:///{curDir}/WebFiles/InspectionDuration.html";

            GenerateCommand = new RelayCommand<WebBrowser>(Generate);
            AddCommand = new GalaSoft.MvvmLight.CommandWpf.RelayCommand(Add);
            RemoveCommand = new GalaSoft.MvvmLight.CommandWpf.RelayCommand(Remove);
        }

        private void Generate(WebBrowser browser)
        {
            if (SelectAllCustomers == false && SelectedCustomer == null)
            {
                return;
            }
            _browser = browser;
            var DataElementList = new List<DataElement>();
            var list = _taskRepository.All();


            foreach (var item in list)
            {
                if (item.ParkingLot.Address != null && item.DateTimeEnd != null && item.DateTimeStart != null || item.Customer?.Name != null)
                {
                    bool region = true;
                    if (SelectedRegion != null && item.ParkingLot.Address != null && item.ParkingLot.Address.Province != null)
                    {
                        if(!item.ParkingLot.Address.Province.Equals(SelectedRegion))
                        {
                            region = false;
                        }
                    }
                    if ((region || SelectAllRegions == true) &&
                        item.DateTimeStart > BeginDateTime &&
                        item.DateTimeStart < EndDateTime &&
                        (item.Customer.Name == SelectedCustomer?.Name || SelectAllCustomers == true))
                    {
                        var inspections = item.Inspections;

                        List<Employee> employees = (from i in inspections from ii in i.InspectionInspectors select ii.Employee).ToList();

                        if (employees.Contains(SelectedEmployee) || SelectAllEmployees == true)
                        {
                            List<TimeSpan> spans = new List<TimeSpan>();
                            foreach (var i in item.Inspections)
                            {
                                TimeSpan span;
                                if (i.DateTimeDone != null)
                                {
                                    TimeSpan? tempSpan = i.DateTimeDone - i.DateTimeStarted;
                                    span = tempSpan.Value;
                                    spans.Add(span);
                                }
                            }
                            if (spans.Count > 0)
                            {
                                double duration = spans.Average(timeSpan => timeSpan.TotalMinutes);
                                int finalDuration = (int) duration;
                                DataElementList.Add(new DataElement
                                                    {
                                                        inspection = item.Customer.Name,
                                                        duration = finalDuration.ToString()
                                                    });
                            }
                        }
                    }
                }
            }
            var o = new DataSet
            {
                data = DataElementList,
                ChartType = SelectedChart
            };
            var test = JsonConvert.SerializeObject(o);
            browser.InvokeScript("go", test);

            Regions.Clear();
            foreach (var address in _addressRepository.All())
            {
                if (!Regions.Contains(address.Province))
                {
                    Regions.Add(address.Province);
                }
            }

            Customers.Clear();
            _customerRepository.All().ForEach(Customers.Add);
            _userRepository.All().ForEach(Inspectors.Add);
        }

        public void GiveBrowser(WebBrowser browser)
        {
            _browser = browser;
        }

        private void Add()
        {
            if (SelectedEmployee == null)
            {
                return;
            }

            SelectedInspectors.Add(SelectedEmployee);
            Inspectors.Remove(SelectedEmployee);
            SelectedEmployee = null;
        }

        private void Remove()
        {
            if (SelectedRemoveEmployee == null)
            {
                return;
            }

            Inspectors.Add(SelectedRemoveEmployee);
            SelectedInspectors.Remove(SelectedRemoveEmployee);
            SelectedRemoveEmployee = null;
        }
    }
}

internal class DataSet
{
    public string ChartType;
    public List<DataElement> data;
}

internal partial class DataElement
{
    public string duration;
    public string inspection;
}