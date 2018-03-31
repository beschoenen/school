using System;
using System.Collections.Generic;
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

namespace ALGA
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, Sierpinski.SierpinskiCanvas
    {
        public MainWindow()
        {
            InitializeComponent();
            redraw();
        }

        private Random r = new Random();

        public void drawSquare(double x, double y, double width, double height)
        {
            Rectangle square = new Rectangle();
            square.Fill = new SolidColorBrush(Color.FromRgb(0, 0, (byte)(r.Next()%256)));
            square.Width = width;
            square.Height = height;
            Canvas.SetLeft(square, x);
            Canvas.SetTop(square, y);
            canvas.Children.Add(square);
        }

        public void redraw()
        {
            if (canvas != null)
            {
                canvas.Children.Clear();
                Sierpinski.draw_sierpinski_carpet((int)slider.Value, 0, 0, canvas.ActualWidth, canvas.ActualHeight, this);
            }
        }

        private void canvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            redraw();
        }

        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            redraw();
        }
    }
}
