using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Database;
using Microsoft.Win32;
using SOh_ParkInspect.ViewModel.ManagementReport;
using Point = System.Windows.Point;
using Size = System.Drawing.Size;

namespace SOh_ParkInspect.Control.ManagementReport
{
    /// <summary>
    ///     Interaction logic for InspectorAvailabilityReport.xaml
    /// </summary>
    public partial class InspectorAvailabilityReport : UserControl
    {
        public InspectorAvailabilityReport()
        {
            InitializeComponent();

            var vm = (InspectorAvailabilityViewModel) DataContext;
            vm.GiveBrowser(Browser);
        }

        private void exportButton_Click(object sender, RoutedEventArgs e)
        {
            var topLeftCorner = Browser.PointToScreen(new Point(0, 0));
            var topLeftGdiPoint = new System.Drawing.Point((int) topLeftCorner.X, (int) topLeftCorner.Y);
            var size = new Size((int) Browser.ActualWidth, (int) Browser.ActualHeight);

            var screenShot = new Bitmap((int) Browser.ActualWidth, (int) Browser.ActualHeight);

            using (var graphics = Graphics.FromImage(screenShot))
            {
                graphics.CopyFromScreen(topLeftGdiPoint, new System.Drawing.Point(),
                                        size, CopyPixelOperation.SourceCopy);
            }
            var saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Png Image|*.png";
            saveFileDialog1.Title = "Save an Image File";
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName != "")
            {
                screenShot.Save(saveFileDialog1.FileName, ImageFormat.Png);
                MessageBox.Show("Saved image succesfully to: " + saveFileDialog1.FileName);
            }
        }

        /// <summary>
        ///     Load data into the JSON, Serialize it and invoke the JavaScript.
        /// </summary>
        private void generateButton_Click(object sender, RoutedEventArgs e)
        {
            //string json = JsonConvert.SerializeObject(prodList);
            //Browser.InvokeScript("go", json);
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Multiple Selection
            var viewmodel = (InspectorAvailabilityViewModel)DataContext;
            viewmodel.SelectedEmployees = EmpList.SelectedItems.Cast<Employee>().ToList();
        }
    }
}