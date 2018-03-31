using System.Collections.Generic;
using Database;
using SOh_ParkInspect.Repository.Interface;

namespace SOh_ParkInspect.Repository.Dummy
{
    internal class DummyAnswerSetValueRepository : IAnswerSetValueRepository
    {
        private readonly List<AnswerSetValue> _answerSetValues;

        public DummyAnswerSetValueRepository()
        {
            _answerSetValues = new List<AnswerSetValue>
            {
                new AnswerSetValue { Value = "Ja" },
                new AnswerSetValue { Value = "Nee" },
                new AnswerSetValue { Value = "Misschien" },
                new AnswerSetValue { Value = "Soms" },
                new AnswerSetValue { Value = "Wellicht" },
                new AnswerSetValue { Value = "A" },
                new AnswerSetValue { Value = "B" },
                new AnswerSetValue { Value = "C" },
                new AnswerSetValue { Value = "D" }
            };
        }

        public List<AnswerSetValue> All()
        {
            return _answerSetValues;
        }
    }
}