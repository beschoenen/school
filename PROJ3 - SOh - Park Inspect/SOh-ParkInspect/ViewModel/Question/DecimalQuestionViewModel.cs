using System.Collections.Generic;
using System.Windows.Controls;
using Database;
using DecimalQuestionControl = SOh_ParkInspect.Control.Question.DecimalQuestionControl;

namespace SOh_ParkInspect.ViewModel.Question
{
    public class DecimalQuestionViewModel : IQuestionWrapper
    {
        public Database.Question Question { get; }
        public string Answer { get; set; }

        public ICollection<AnswerSetValue> AnswerSet { get; }

        public UserControl Control => new DecimalQuestionControl
        {
            DataContext = this
        };

        public bool IsValid => !string.IsNullOrWhiteSpace(Answer);

        public DecimalQuestionViewModel(Database.Question question)
        {
            Question = question;
            AnswerSet = Question.AnswerSetValues;
        }

        public Answer GetAnswer()
        {
            var answerString = Answer;

            if (Question.AnswerPrefix != null)
            {
                answerString = $"{Question.AnswerPrefix} {answerString}";
            }

            if (Question.AnswerSuffix != null)
            {
                answerString = $"{answerString} {Question.AnswerSuffix}";
            }

            return new Answer { Text = answerString };
        }
    }
}