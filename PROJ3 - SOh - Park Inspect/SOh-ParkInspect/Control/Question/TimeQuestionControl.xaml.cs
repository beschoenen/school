using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using SOh_ParkInspect.ViewModel.Question;
using Xceed.Wpf.Toolkit;

namespace SOh_ParkInspect.Control.Question
{
    /// <summary>
    ///     Interaction logic for TimeQuestionControl.xaml
    /// </summary>
    public partial class TimeQuestionControl : UserControl
    {
        public TimeQuestionControl()
        {
            InitializeComponent();
        }

        private void UpDownBase_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var timePicker = (TimePicker) sender;
            var value = timePicker.Text;

            Background = string.IsNullOrWhiteSpace(value) ? Brushes.IndianRed : Brushes.LightGreen;

            var viewmodel = (TimeQuestionViewModel) DataContext;
            viewmodel.Answer = value;
        }
    }
}