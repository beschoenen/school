using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SOh_ParkInspect.Control.Question
{
    /// <summary>
    ///     Interaction logic for DecimalQuestionControl.xaml
    /// </summary>
    public partial class DecimalQuestionControl : UserControl
    {
        public DecimalQuestionControl()
        {
            InitializeComponent();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            int result;

            if (!(int.TryParse(e.Text, out result) || e.Text == "-" || e.Text == "." || e.Text == ","))
            {
                e.Handled = true;
            }
        }

        private void TextBoxBase_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var decimalQuestion = (TextBox) sender;
            var value = decimalQuestion.Text;

            Background = string.IsNullOrWhiteSpace(value) ? Brushes.IndianRed : Brushes.LightGreen;
        }
    }
}