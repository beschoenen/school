using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using Database;
using SOh_ParkInspect.Control.Question;

namespace SOh_ParkInspect.ViewModel.Question
{
    public class MultipleChoiceQuestionViewModel : IQuestionWrapper
    {
        public Database.Question Question { get; }
        public string Answer { get; set; }

        public ICollection<AnswerSetValue> AnswerSet { get; set; }
        public ICollection<AnswerSetValue> SelectedAnswers { get; set; }

        public UserControl Control => new MultipleChoiceQuestionControl
        {
            DataContext = this
        };

        public bool IsValid => SelectedAnswers.Count > 0;

        public MultipleChoiceQuestionViewModel(Database.Question question)
        {
            Question = question;
            AnswerSet = Question.AnswerSetValues;

            SelectedAnswers = new ObservableCollection<AnswerSetValue>();
        }

        public Answer GetAnswer()
        {
            foreach (var answer in SelectedAnswers)
            {
                if (Answer == null)
                {
                    Answer = answer.Value;
                }
                else
                {
                    Answer += " & " + answer.Value;
                }
            }

            return new Answer { Text = Answer };
        }
    }
}