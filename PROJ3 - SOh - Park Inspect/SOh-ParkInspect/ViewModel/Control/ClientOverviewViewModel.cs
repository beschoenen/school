using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Database;
using GalaSoft.MvvmLight.CommandWpf;
using SOh_ParkInspect.Enum;
using SOh_ParkInspect.Helper;
using SOh_ParkInspect.Repository.Interface;

namespace SOh_ParkInspect.ViewModel.Control
{
    public class ClientOverviewViewModel : ParkInspectViewModel
    {
        private readonly RouterService _router;
        private readonly ICustomerRepository _repository;

        public ICommand ToCustomerCommand { get; set; }
        public ICommand NewCustomerCommand { get; set; }
        public ICommand SearchCommand { get; set; }

        public ObservableCollection<Customer> Customers { get; set; }

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

        private Customer _selectedCustomer;

        public Customer SelectedCustomer
        {
            get { return _selectedCustomer; }
            set
            {
                _selectedCustomer = value;
                RaisePropertyChanged();
            }
        }

        public ClientOverviewViewModel(RouterService router, ICustomerRepository repository)
        {
            _router = router;
            _repository = repository;

            ToCustomerCommand = new RelayCommand(ToClient, () => SelectedCustomer != null);
            NewCustomerCommand = new RelayCommand(NewCustomer);
            SearchCommand = new RelayCommand(Search);

            Customers = new ObservableCollection<Customer>();
        }

        private void Search()
        {
            Customers.Clear();

            var search = _repository.All();

            if (!string.IsNullOrWhiteSpace(SearchString))
            {
                search.Where(t => t.Name.ToLower().Contains(SearchString.ToLower()) || t.Email.ToLower().Contains(SearchString.ToLower())).ToList().ForEach(Customers.Add);
                return;
            }

            search.ForEach(Customers.Add);
        }

        private void NewCustomer()
        {
            _router.GoTo(ControlType.ClientInfoControl);
        }

        private void ToClient()
        {
            if (SelectedCustomer == null) return;

            dynamic viewBag = new ViewBag();
            viewBag.Customer = SelectedCustomer;

            _router.GoTo(ControlType.ClientInfoControl, viewBag);
        }

        public override void OnEnter()
        {
            Search();
        }
    }
}