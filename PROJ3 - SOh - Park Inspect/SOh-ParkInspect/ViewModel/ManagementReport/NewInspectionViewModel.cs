using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Controls;
using System.Windows.Input;
using Database;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using SOh_ParkInspect.Repository.Interface;

namespace SOh_ParkInspect.ViewModel.ManagementReport
{
    public class NewInspectionViewModel : ViewModelBase
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly ICustomerRepository _customerRepository;

        private DateTime _beginDateTime;

        private WebBrowser _browser;

        private DateTime _endDateTime;

        private bool _selectAllCustomers;

        private bool _selectAllRegions;

        private string _selectedChart;

        private Customer _selectedCustomer;

        private string _selectedRegion;
        private string currentUri;

        public NewInspectionViewModel(ICustomerRepository customerRepository, IAddressRepository addressRepository, ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
            _customerRepository = customerRepository;
            _addressRepository = addressRepository;
            var curDir = Directory.GetCurrentDirectory();
            WebBrowserUri = $"file:///{curDir}/WebFiles/NewInspection.html";
            GenerateCommand = new RelayCommand<WebBrowser>(Generate);

            Customers = new ObservableCollection<Customer>(customerRepository.All());

            Regions = new ObservableCollection<string>();
            foreach (var address in addressRepository.All())
            {
                if (!Regions.Contains(address.Province))
                {
                    Regions.Add(address.Province);
                }
            }

            ChartTypes = new ObservableCollection<string>();
            ChartTypes.Add("Barchart");
            ChartTypes.Add("Piechart");
            EndDateTime = DateTime.Now.AddMonths(2);
        }

        public ObservableCollection<string> ChartTypes { get; set; }
        public ObservableCollection<string> Regions { get; set; }
        public ObservableCollection<Customer> Customers { get; set; }

        public string WebBrowserUri
        {
            get { return currentUri; }
            set
            {
                currentUri = value;
                RaisePropertyChanged("WebBrowserUri");
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

        public ICommand GenerateCommand { get; set; }

        public DateTime EndDateTime
        {
            get { return _endDateTime; }
            set
            {
                _endDateTime = value;
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

        public Customer SelectedCustomer
        {
            get { return _selectedCustomer; }
            set
            {
                _selectedCustomer = value;
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

        public bool SelectAllCustomers
        {
            get { return _selectAllCustomers; }
            set
            {
                _selectAllCustomers = value;
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

        public void GiveBrowser(WebBrowser browser)
        {
            _browser = browser;
        }

        private void Generate(WebBrowser browser)
        {
            Regions.Clear();
            foreach (var address in _addressRepository.All())
            {
                if (!Regions.Contains(address.Province))
                {
                    Regions.Add(address.Province);
                }
            }

            _browser = browser;

            var tasks = _taskRepository.All();
            var DataElementList = new List<NewInspectionDataElement>();

            var values = new Dictionary<string, int>();

            foreach (var item in tasks)
            {
                if (((item.Customer == SelectedCustomer) || SelectAllCustomers) && (item.DatetimeCreated != null) && (item.DateTimeEnd != null) && (item.DateTimeStart != null) && (item.ParkingLot?.Address != null))
                {
                    if ((item.DatetimeCreated < EndDateTime) && (item.DatetimeCreated > BeginDateTime) && (item.ParkingLot.Address.Province.Equals(SelectedRegion) || SelectAllRegions))
                    {
                        var month = item.DatetimeCreated.ToString("MMMMM");
                        if (!values.ContainsKey(month))
                        {
                            values.Add(month, 0);
                        }
                        values[month]++;
                    }
                }
            }

            foreach (var item in values)
            {
                DataElementList.Add(new NewInspectionDataElement
                                    {
                                        month = item.Key,
                                        amount = item.Value.ToString()
                                    });
            }

            var o = JsonConvert.SerializeObject(DataElementList);
            browser.InvokeScript("go", o);

            Customers.Clear();
            _customerRepository.All().ForEach(Customers.Add);
        }
    }

    internal class NewInspectionDataSet
    {
        public string ChartType;
        public List<NewInspectionDataElement> data;
    }

    internal class NewInspectionDataElement
    {
        public string amount;
        public string month;
    }
}