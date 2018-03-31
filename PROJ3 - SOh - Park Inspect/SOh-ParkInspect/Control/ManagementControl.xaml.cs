using System.Windows;
using System.Windows.Controls;
using SOh_ParkInspect.Helper;
using SOh_ParkInspect.ViewModel.Control;

namespace SOh_ParkInspect.Control
{
    /// <summary>
    ///     Interaction logic for ManagementControl.xaml
    /// </summary>
    public partial class ManagementControl
    {
        private ParkInspectViewModel _currentViewModel;

        public ManagementControl()
        {
            InitializeComponent();

            var vm = (ManagentViewModel) DataContext;
            vm.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == "GoBack" && vm.GoBack)
                {
                    _currentViewModel?.OnEnter();
                }
            };
        }

        private void Selector_OnSelected(object sender, RoutedEventArgs e)
        {
            _currentViewModel = (ParkInspectViewModel) ((ParkInspectControl) ((UserControl) ((TabItem) sender)?.Content)?.Content)?.DataContext;

            _currentViewModel?.OnEnter();
        }

        private void Selector_OnUnSelected(object sender, RoutedEventArgs e)
        {
            var viewModel = (ParkInspectViewModel) ((ParkInspectControl) ((UserControl) ((TabItem) sender)?.Content)?.Content)?.DataContext;

            viewModel?.OnLeave();
        }
    }
}