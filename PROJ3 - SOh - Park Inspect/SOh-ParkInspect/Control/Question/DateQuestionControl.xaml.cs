using System.Windows.Controls;
using System.Windows.Media;

namespace SOh_ParkInspect.Control.Question
{
    /// <summary>
    ///     Interaction logic for DateQuestionControl.xaml
    /// </summary>
    public partial class DateQuestionControl : UserControl
    {
        public DateQuestionControl()
        {
            InitializeComponent();
        }

        private void DatePicker_OnSelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var datePicker = (DatePicker) sender;
            var dateValue = datePicker.Text;

            Background = string.IsNullOrWhiteSpace(dateValue) ? Brushes.IndianRed : Brushes.LightGreen;
        }
    }
}