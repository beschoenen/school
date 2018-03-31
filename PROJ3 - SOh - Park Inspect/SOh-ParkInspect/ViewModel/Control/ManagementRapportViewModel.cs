using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using SOh_ParkInspect.Control.ManagementReport;
using SOh_ParkInspect.Helper;

namespace SOh_ParkInspect.ViewModel.Control
{
    public class ManagementRapportViewModel : ParkInspectViewModel
    {
        public ObservableCollection<KeyValuePair<string, UserControl>> ManagementRapportTypes { get; set; }

        private KeyValuePair<string, UserControl> _selectedRapport;

        public KeyValuePair<string, UserControl> SelectedRapport
        {
            get { return _selectedRapport; }
            set
            {
                _selectedRapport = value;
                CurrentControl = _selectedRapport.Value;
                base.RaisePropertyChanged();
            }
        }

        private UserControl _currentControl;

        public UserControl CurrentControl
        {
            get { return _currentControl; }
            set
            {
                _currentControl = value;
                base.RaisePropertyChanged();
            }
        }

        public ManagementRapportViewModel()
        {
            ManagementRapportTypes = new ObservableCollection<KeyValuePair<string, UserControl>>
            {
                new KeyValuePair<string, UserControl>("Inspecteur Beschikbaarheid", new InspectorAvailabilityReport()),
                new KeyValuePair<string, UserControl>("Inspectie uitvoer tijden", new InspectionDurationReport()),
                new KeyValuePair<string, UserControl>("Nieuwe geplande inspecties", new NewInspectionReport())
            };
        }
    }
}