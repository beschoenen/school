using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using Database;
using GalaSoft.MvvmLight.CommandWpf;
using SOh_ParkInspect.Control.Template;
using SOh_ParkInspect.Helper;
using SOh_ParkInspect.Repository.Interface;

namespace SOh_ParkInspect.ViewModel.Template
{
    public class TemplateChoiceViewModel : ParkInspectViewModel, ITemplateWrapper
    {
        private string _newAnswerText;
        public string NewAnswerText
        {
            get { return _newAnswerText; }
            set
            {
                _newAnswerText = value;
                base.RaisePropertyChanged();
            }
        }

        private AnswerSetValue _selectedExistingAnswer;
        public AnswerSetValue SelectedExistingAnswer
        {
            get { return _selectedExistingAnswer; }
            set
            {
                _selectedExistingAnswer = value;
                base.RaisePropertyChanged();
            }
        }

        private AnswerSetValue _selectedAnswer;

        public AnswerSetValue SelectedAnswer
        {
            get { return _selectedAnswer; }
            set
            {
                _selectedAnswer = value;
                base.RaisePropertyChanged();
            }
        }

        public Database.Question Question { get; }
        public bool IsEditing { get; set; }

        public ObservableCollection<AnswerSetValue> ExistingAnswers { get; set; }
        public ObservableCollection<AnswerSetValue> AnswerSetValues { get; set; }

        public UserControl Control => new TemplateChoiceControl { DataContext = this };

        public ICommand DeleteAnswerCommand { get; set; }
        public ICommand AddNewAnswerCommand { get; set; }
        public ICommand AddExistingAnswerCommand { get; set; }

        public TemplateChoiceViewModel(Database.Question question, IAnswerSetValueRepository answerSetValueRepository)
        {
            Question = question;

            AddExistingAnswerCommand = new RelayCommand(AddExistingAnswer, () => SelectedExistingAnswer != null);
            AddNewAnswerCommand = new RelayCommand(AddNewAnswer);
            DeleteAnswerCommand = new RelayCommand(DeleteAnswer);

            ExistingAnswers = new ObservableCollection<AnswerSetValue>(answerSetValueRepository.All());
            AnswerSetValues = new ObservableCollection<AnswerSetValue>(Question.AnswerSetValues);
        }

        private void AddExistingAnswer()
        {
            if (SelectedExistingAnswer == null) return;
            if (Question.AnswerSetValues.Any(value => value.Value.Equals(SelectedExistingAnswer.Value))) return;

            Question.AnswerSetValues.Add(SelectedExistingAnswer);
            AnswerSetValues.Add(SelectedExistingAnswer);

            SelectedAnswer = SelectedExistingAnswer;
        }

        private void AddNewAnswer()
        {
            if (string.IsNullOrWhiteSpace(NewAnswerText)) return;

            NewAnswerText = NewAnswerText.Trim();

            if (Question.AnswerSetValues.Any(value => value.Value.Equals(NewAnswerText))) return;

            var newAnswer = new AnswerSetValue { Value = NewAnswerText };

            Question.AnswerSetValues.Add(newAnswer);
            AnswerSetValues.Add(newAnswer);

            SelectedAnswer = newAnswer;
        }

        private void DeleteAnswer()
        {
            if (SelectedAnswer == null) return;

            Question.AnswerSetValues.Remove(SelectedAnswer);
            AnswerSetValues.Remove(SelectedAnswer);
        }
    }
}