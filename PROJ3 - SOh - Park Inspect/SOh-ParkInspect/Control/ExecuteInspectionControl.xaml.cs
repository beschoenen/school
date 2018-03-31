using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace SOh_ParkInspect.Control
{
    /// <summary>
    ///     Interaction logic for ClientInfoUserControl.xaml
    /// </summary>
    public partial class ExecuteInspectionControl
    {
        public ExecuteInspectionControl()
        {
            InitializeComponent();
        }

        private void DataGrid_RemovePhoto(object sender, MouseButtonEventArgs e)
        {
            var dep = (DependencyObject) e.OriginalSource;

            // iteratively traverse the visual tree
            while (dep != null &&
                   !(dep is DataGridCell) &&
                   !(dep is DataGridColumnHeader))
            {
                dep = VisualTreeHelper.GetParent(dep);
            }

            if (dep == null)
            {
                return;
            }

            if (dep is DataGridColumnHeader)
            {
                var columnHeader = dep as DataGridColumnHeader;
                // Do Nothing
            }

            if (dep is DataGridCell)
            {
                var cell = dep as DataGridCell;
                // TODO: Remove the picture i clicked here, from the PhotoList
            }
        }
    }
}