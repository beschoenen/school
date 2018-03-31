using System.Collections.Generic;
using System.Windows.Controls;
using Database;
using SingleChoiceQuestionControl = SOh_ParkInspect.Control.Question.SingleChoiceQuestionControl;

namespace SOh_ParkInspect.ViewModel.Question
{
    public class SingleChoiceQuestionViewModel : IQuestionWrapper
    {
        public Database.Question Question { get; }
        public string Answer { get; set; }

        public ICollection<AnswerSetValue> AnswerSet { get; }

        public UserControl Control => new SingleChoiceQuestionControl
        {
            DataContext = this
        };

        public bool IsValid => !string.IsNullOrWhiteSpace(Answer);

        public SingleChoiceQuestionViewModel(Database.Question question)
        {
            Question = question;
            AnswerSet = Question.AnswerSetValues;
        }

        public Answer GetAnswer()
        {
            return new Answer { Text = Answer };
        }
    }
}