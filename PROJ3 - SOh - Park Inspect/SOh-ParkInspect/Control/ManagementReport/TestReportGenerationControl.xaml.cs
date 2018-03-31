using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SOh_ParkInspect.Control.ManagementReport
{
    /// <summary>
    /// Interaction logic for TestReportGenerationControl.xaml
    /// </summary>
    public partial class TestReportGenerationControl
    {
        public TestReportGenerationControl()
        {
            InitializeComponent();
        }

        private void exportButton_Click(object sender, RoutedEventArgs e)
        {
            var topLeftCorner = Browser1.PointToScreen(new System.Windows.Point(0, 0));
            var topLeftGdiPoint = new System.Drawing.Point((int)topLeftCorner.X, (int)topLeftCorner.Y);
            var size = new System.Drawing.Size((int)Browser1.ActualWidth, (int)Browser1.ActualHeight);

            var screenShot = new Bitmap((int)Browser1.ActualWidth, (int)Browser1.ActualHeight);

            using (var graphics = Graphics.FromImage(screenShot))
            {
                graphics.CopyFromScreen(topLeftGdiPoint, new System.Drawing.Point(),
                    size, CopyPixelOperation.SourceCopy);
            }
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
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
        /// Load data into the JSON, Serialize it and invoke the JavaScript.
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
