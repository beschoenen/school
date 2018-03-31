using System.Collections.Generic;
using Database;
using SOh_ParkInspect.Repository.Interface;

namespace SOh_ParkInspect.Repository.Dummy
{
    public class DummyQuestionTypeRepository : IQuestionTypeRepository
    {
        private readonly List<QuestionType> _questionTypes;

        public DummyQuestionTypeRepository()
        {
            _questionTypes = new List<QuestionType>
            {
                new QuestionType { Name = QuestionType.MultipleChoice },
                new QuestionType { Name = QuestionType.SingleChoice },
                new QuestionType { Name = QuestionType.Text },
                new QuestionType { Name = QuestionType.Number },
                new QuestionType { Name = QuestionType.Decimal },
                new QuestionType { Name = QuestionType.Date },
                new QuestionType { Name = QuestionType.Time },
                new QuestionType { Name = QuestionType.DateTime },
                new QuestionType { Name = QuestionType.Photo }
            };
        }

        public List<QuestionType> All()
        {
            return _questionTypes;
        }
    }
}