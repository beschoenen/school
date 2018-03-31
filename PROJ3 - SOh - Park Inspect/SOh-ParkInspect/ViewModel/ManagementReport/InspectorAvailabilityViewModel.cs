using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using Database;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using SOh_ParkInspect.Control.ManagementReport;
using SOh_ParkInspect.Repository.Interface;

namespace SOh_ParkInspect.ViewModel.ManagementReport
{
    public class InspectorAvailabilityViewModel : ViewModelBase
    {
        private readonly IAvailabilityRepository _availabilityRepository;
        private readonly IScheduleRepository _scheduleRepository;
        public readonly List<Availability> tempEmpList;
        private WebBrowser _browser;
        public ICollection<Employee> SelectedEmployees { get; set; }
        public ObservableCollection<string> AnswerCollection { get; private set; }


        private string currentUri;

        public ICommand GenerateCommand { get; set; }

//        public ObservableCollection<string> SelectedItems { get; }
        public ObservableCollection<Employee> EmployeeList { get; }

        public List<string> IdList { get; set; }

        public UserControl ManagementControl => new InspectorAvailabilityReport();

        public string WebBrowserUri
        {
            get { return currentUri; }
            set
            {
                currentUri = value;
                RaisePropertyChanged();
            }
        }

        public InspectorAvailabilityViewModel(IUserRepository userRepository ,IAvailabilityRepository availabilityRepository, IScheduleRepository scheduleRepository)
        {
            _availabilityRepository = availabilityRepository;
            _scheduleRepository = scheduleRepository;
//            SelectedItems = selectedItems;

            AnswerCollection = new ObservableCollection<string>();
            SelectedEmployees = new ObservableCollection<Employee>();

            EmployeeList = new ObservableCollection<Employee>();

            userRepository.All()
                .Where(x => x != null)
                .Select(x => x).Distinct()
                .ToList().ForEach(EmployeeList.Add);

            //SelectedItems.CollectionChanged += SelectedItems_CollectionChanged;

            var curDir = Directory.GetCurrentDirectory();
            WebBrowserUri = $"file:///{curDir}/WebFiles/AvailabilityInspectors.html";

            GenerateCommand = new RelayCommand<WebBrowser>(generate);
        }
//
//        private void SelectedItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
//        {
//            if (e.Action == NotifyCollectionChangedAction.Add)
//            {
//                foreach (var str in SelectedItems)
//                {
//                    Debug.WriteLine("New item added {0}", str);
//                }
//            }
//        }

        private void generate(WebBrowser browser)
        {
            _browser = browser;
            var DataElementList = new List<DataElement>();

            if (SelectedEmployees != null)
            {
                foreach (var e in SelectedEmployees)
                {
                    DataElement element = new DataElement();
                    if (e != null)
                    {
                        element.inspector = e.FirstName+" "+e.LastName;
                    }

                    foreach (var aitem in e.Availabilities)
                    {
                        if (aitem.Available)
                        {
                            if (aitem.EndDateTime != null)
                            {
                                element.availability = (int)Math.Round(aitem.EndDateTime.Value.Subtract(aitem.StartDateTime).TotalHours); // TODO null check
                            }
                        }
                    }

                    foreach (var sitem in e.WeeklySchedules)
                    {
                        element.planned += (int)Math.Round(sitem.EndTime.Subtract(sitem.StartTime).TotalHours);
                    }
                    DataElementList.Add(element);
                }

                var o = new DataSet
                {
                    data = DataElementList
                };

                var test = JsonConvert.SerializeObject(o);
                browser.InvokeScript("go", test); 
            }
        }


        public void GiveBrowser(WebBrowser browser)
        {
            _browser = browser;
        }
    }
}

internal partial class DataElement
{
    public int availability;
    public string inspector;
    public int planned;
}