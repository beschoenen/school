using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SOh_ParkInspect.Control.Question
{
    /// <summary>
    ///     Interaction logic for NumericQuestionControl.xaml
    /// </summary>
    public partial class NumericQuestionControl : UserControl
    {
        public NumericQuestionControl()
        {
            InitializeComponent();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            int result;

            if (!(int.TryParse(e.Text, out result) || e.Text == "-"))
            {
                e.Handled = true;
            }
        }

        private void TextBoxBase_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = (TextBox) sender;
            var textValue = textBox.Text;

            Background = string.IsNullOrWhiteSpace(textValue) ? Brushes.IndianRed : Brushes.LightGreen;
        }
    }
}