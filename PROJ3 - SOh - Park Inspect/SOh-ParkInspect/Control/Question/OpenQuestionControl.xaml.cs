using System.Windows.Controls;
using System.Windows.Media;

namespace SOh_ParkInspect.Control.Question
{
    /// <summary>
    ///     Interaction logic for OpenQuestionView.xaml
    /// </summary>
    public partial class OpenQuestionControl : UserControl
    {
        public OpenQuestionControl()
        {
            InitializeComponent();
        }

        private void TextBoxBase_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var openQuestion = (TextBox) sender;
            var value = openQuestion.Text;

            Background = string.IsNullOrWhiteSpace(value) ? Brushes.IndianRed : Brushes.LightGreen;
        }
    }
}