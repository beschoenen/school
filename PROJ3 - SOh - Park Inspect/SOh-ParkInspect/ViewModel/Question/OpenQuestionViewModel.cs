using System.Collections.Generic;
using System.Windows.Controls;
using Database;
using OpenQuestionControl = SOh_ParkInspect.Control.Question.OpenQuestionControl;

namespace SOh_ParkInspect.ViewModel.Question
{
    public class OpenQuestionViewModel : IQuestionWrapper
    {
        public Database.Question Question { get; }
        public string Answer { get; set; }

        public ICollection<AnswerSetValue> AnswerSet { get; }

        public UserControl Control => new OpenQuestionControl
        {
            DataContext = this
        };

        public bool IsValid => !string.IsNullOrWhiteSpace(Answer);

        public OpenQuestionViewModel(Database.Question question)
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