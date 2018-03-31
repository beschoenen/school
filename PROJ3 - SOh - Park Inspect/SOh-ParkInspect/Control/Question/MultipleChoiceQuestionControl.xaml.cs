using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using Database;
using SOh_ParkInspect.ViewModel.Question;

namespace SOh_ParkInspect.Control.Question
{
    /// <summary>
    ///     Interaction logic for MultipleChoiceQuestionControl.xaml
    /// </summary>
    public partial class MultipleChoiceQuestionControl : UserControl
    {
        public MultipleChoiceQuestionControl()
        {
            InitializeComponent();
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listBox = (ListBox) sender;
            var value = listBox.SelectedItems;

            Background = value.Count == 0 ? Brushes.IndianRed : Brushes.LightGreen;

            var viewmodel = (MultipleChoiceQuestionViewModel) DataContext;
            viewmodel.SelectedAnswers = ListView.SelectedItems.Cast<AnswerSetValue>().ToList();
        }
    }
}