using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Xceed.Wpf.Toolkit;

namespace SOh_ParkInspect.Control.Question
{
    /// <summary>
    ///     Interaction logic for DateTimeQuestionControl.xaml
    /// </summary>
    public partial class DateTimeQuestionControl : UserControl
    {
        public DateTimeQuestionControl()
        {
            InitializeComponent();
        }

        private void UpDownBase_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var dateTimePicker = (DateTimePicker) sender;
            var value = dateTimePicker.Text;

            Background = string.IsNullOrWhiteSpace(value) ? Brushes.IndianRed : Brushes.LightGreen;
        }
    }
}