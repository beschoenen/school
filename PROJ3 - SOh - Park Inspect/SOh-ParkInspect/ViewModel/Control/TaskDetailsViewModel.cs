using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Database;
using GalaSoft.MvvmLight.Command;
using SOh_ParkInspect.Enum;
using SOh_ParkInspect.Helper;

namespace SOh_ParkInspect.ViewModel.Control
{
    public class TaskDetailsViewModel : ParkInspectViewModel
    {
        private readonly RouterService _router;

        public ICommand GoBackCommand { get; set; }
        public ICommand LinkInspectorCommand { get; set; }
        public ICommand ConvertToPdfCommand { get; set; }

        public ObservableCollection<Inspection> Inspections { get; set; }
        public ObservableCollection<Employee> Inspectors { get; set; }

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

        private string _remark;
        public string Remarks
        {
            get { return _remark; }
            set
            {
                _remark = value;
                RaisePropertyChanged();
            }
        }

        public Task Task { get; set; }

        public TaskDetailsViewModel(RouterService router)
        {
            _router = router;

            GoBackCommand = new RelayCommand(_router.GoBack);
            ConvertToPdfCommand = new RelayCommand(ConvertToPdf);
            LinkInspectorCommand = new RelayCommand(LinkInspector);

            Inspections = new ObservableCollection<Inspection>();
            Inspectors = new ObservableCollection<Employee>();
        }

        public override void OnEnter()
        {
            if (ViewBag?.Task == null)
            {
                MessageBox.Show("Er is geen inspectie geselecteerd!");
                _router.GoBack();
                return;
            }

            Task = ViewBag.Task;
            Inspections.Clear();
            Task.Inspections.ToList().ForEach(Inspections.Add);

            Inspectors.Clear();

            Task.Inspections.SelectMany(x => x.InspectionInspectors.Select(i => i.Employee)).Distinct().ToList().ForEach(Inspectors.Add);
            Remarks = Task.Remarks;

            RaisePropertyChanged(nameof(Task));
        }

        private void ConvertToPdf()
        {
            if (SelectedInspection == null) return;

            if (SelectedInspection.DateTimePlanned > DateTime.Now)
            {
                MessageBox.Show("Deze inspectie is nog niet voltooid!");
                return;
            }

            var converter = new PdfConverter { IncludeAnswers = true };

            converter.Convert(_selectedInspection);
        }

        private void LinkInspector()
        {
            if (SelectedInspection == null) return;

            dynamic viewBag = new ViewBag();
            viewBag.Inspection = SelectedInspection;

            _router.GoTo(ControlType.LinkInspectorControl, viewBag);
        }
    }
}