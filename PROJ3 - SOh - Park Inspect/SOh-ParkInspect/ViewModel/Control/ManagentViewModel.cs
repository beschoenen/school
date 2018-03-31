using System.Windows;
using System.Windows.Controls;
using SOh_ParkInspect.Control;
using SOh_ParkInspect.Helper;
using SOh_ParkInspect.Model;

namespace SOh_ParkInspect.ViewModel.Control
{
    public class ManagentViewModel : ParkInspectViewModel
    {
        private readonly RouterService _router;

        public UserControl UserManagement { get; set; }
        public UserControl TemplateOverview { get; set; }
        public UserControl TaskOverview { get; set; }
        public UserControl Heatmap { get; set; }
        public UserControl ClientOverview { get; set; }
        public UserControl ReportOverview { get; set; }

        public bool GoBack { get; set; }

        public ManagentViewModel(RouterService router)
        {
            _router = router;

            UserManagement = new UserManagementControl();
            TemplateOverview = new TemplateOverviewControl();
            TaskOverview = new TaskOverviewControl();
            ReportOverview = new ManagementReportControl();
            Heatmap = new HeatMapControl();
            ClientOverview = new ClientOverviewControl();
        }

        public override void OnEnter()
        {
            if (Settings.IsOfflineMode)
            {
                MessageBox.Show("Dit scherm is niet beschikbaar in offline mode.");

                _router.GoBack();
                return;
            }

            RaisePropertyChanged(nameof(GoBack));
            GoBack = true;
        }
    }
}
