using System.Drawing;
using System.Drawing.Imaging;
using System.Windows;
using Microsoft.Win32;
using Point = System.Windows.Point;
using Size = System.Drawing.Size;

namespace SOh_ParkInspect.Control.ManagementReport
{
    /// <summary>
    ///     Interaction logic for ManagementReportGenerationControl.xaml
    /// </summary>
    public partial class ManagementReportGenerationControl
    {
        public ManagementReportGenerationControl()
        {
            InitializeComponent();
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
            //List<Product> prodList = new List<Product>();
            //Product product = new Product();
            //product.Name = "Apple";
            //product.Price = 5;

            //Product product1 = new Product();
            //product1.Name = "Samsung";
            //product1.Price = 3;

            //Product product2 = new Product();
            //product2.Name = "HTC";
            //product2.Price = 1;

            //prodList.Add(product);
            //prodList.Add(product1);
            //prodList.Add(product2);

            //string json = JsonConvert.SerializeObject(prodList);
            //Browser.InvokeScript("go", json);
        }
    }
}