using System.Collections.Generic;
using System.Linq;
using Database;
using SOh_ParkInspect.Repository.Interface;

namespace SOh_ParkInspect.Repository.Dummy
{
    public class DummyQuestionRepository : IQuestionRepository
    {
        private readonly List<Question> _questions;

        public DummyQuestionRepository()
        {
            _questions = new List<Question>
            {
                new Question
                {
                    Text = "Welke dag is het vandaag?",
                    QuestionType = new QuestionType { Name = "Datum" }
                },
                new Question
                {
                    Text = "Is het antwoord nee?",
                    QuestionType = new QuestionType { Name = "Multiple Choice" },
                    AnswerSetValues = new List<AnswerSetValue>
                    {
                        new AnswerSetValue { Value = "Ja" },
                        new AnswerSetValue { Value = "Nee" },
                        new AnswerSetValue { Value = "Misschien" }
                    }
                },
                new Question
                {
                    Text = "Is het antwoord niet ja?",
                    QuestionType = new QuestionType { Name = "Single Choice" },
                    AnswerSetValues = new List<AnswerSetValue>
                    {
                        new AnswerSetValue { Value = "Ja" },
                        new AnswerSetValue { Value = "Nee" },
                        new AnswerSetValue { Value = "Misschien" }
                    }
                }
            };
        }

        public List<Question> All()
        {
            return _questions;
        }

        public Question Find(int id)
        {
            return _questions.FirstOrDefault(q => q.ID == id);
        }
    }
}