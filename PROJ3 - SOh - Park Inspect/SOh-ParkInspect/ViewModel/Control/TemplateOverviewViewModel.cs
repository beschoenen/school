using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Database;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using SOh_ParkInspect.Enum;
using SOh_ParkInspect.Helper;
using SOh_ParkInspect.Model;
using SOh_ParkInspect.Repository.Interface;

namespace SOh_ParkInspect.ViewModel.Control
{
    public class TemplateOverviewViewModel : ParkInspectViewModel
    {
        private readonly RouterService _router;
        private readonly IChecklistRepository _repository;

        public ICommand NewTemplateCommand { get; set; }
        public ICommand ToTemplateCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand RefreshCommand { get; set; }

        public ObservableCollection<TemplateChecklistViewModel> Checklists { get; set; }

        private TemplateChecklistViewModel _selectedChecklist;

        public TemplateChecklistViewModel SelectedChecklist
        {
            get { return _selectedChecklist; }
            set
            {
                _selectedChecklist = value;
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

        public TemplateOverviewViewModel(RouterService router, IChecklistRepository repository)
        {
            _router = router;
            _repository = repository;
            
            ToTemplateCommand = new RelayCommand(ToTemplate, () => SelectedChecklist != null);
            NewTemplateCommand = new RelayCommand(NewTemplate);
            SearchCommand = new RelayCommand(Search);
            
            Checklists = new ObservableCollection<TemplateChecklistViewModel>();
        }

        private void Search()
        {
            Checklists.Clear();

            var search = _repository.All().Select(x => new TemplateChecklistViewModel(x));

            if (!string.IsNullOrWhiteSpace(SearchString))
            {
                search.Where(t => t.Checklist.Name.ToLower().Contains(SearchString.ToLower())).ToList().ForEach(Checklists.Add);
                return;
            }

            search.ToList().ForEach(Checklists.Add);
            RaisePropertyChanged(nameof(Checklists));
        }

        private void NewTemplate()
        {
            _router.GoTo(ControlType.EditTemplateControl);
        }
 
        private void ToTemplate()
        {
            if (SelectedChecklist == null) return;
            
            dynamic viewBag = new ViewBag();
            viewBag.Template = SelectedChecklist.Checklist;

            _router.GoTo(ControlType.EditTemplateControl, viewBag);
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