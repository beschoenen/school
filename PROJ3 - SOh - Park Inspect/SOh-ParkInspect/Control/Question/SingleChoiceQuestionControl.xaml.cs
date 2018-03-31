using System;
using System.Windows.Controls;
using System.Windows.Media;
using SOh_ParkInspect.ViewModel.Question;

namespace SOh_ParkInspect.Control.Question
{
    /// <summary>
    ///     Interaction logic for MultipleChoiceQuestionView.xaml
    /// </summary>
    public partial class SingleChoiceQuestionControl : UserControl
    {
        public SingleChoiceQuestionControl()
        {
            InitializeComponent();
        }

        private void comboBox_DropDownClosed(object sender, EventArgs e)
        {
            var selection = (ComboBox) sender;
            var value = selection.Text;

            Background = string.IsNullOrWhiteSpace(value) ? Brushes.IndianRed : Brushes.LightGreen;

            var viewmodel = (SingleChoiceQuestionViewModel) DataContext;
            viewmodel.Answer = selection.Text;
        }
    }
}