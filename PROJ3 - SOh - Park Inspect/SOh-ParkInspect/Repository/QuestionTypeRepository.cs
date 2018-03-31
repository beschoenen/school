using System.Collections.Generic;
using System.Linq;
using Database;
using SOh_ParkInspect.Repository.Interface;

namespace SOh_ParkInspect.Repository
{
    public class QuestionTypeRepository : IQuestionTypeRepository
    {
        private readonly Context _context;

        public QuestionTypeRepository(Context context)
        {
            _context = context;
        }

        public List<QuestionType> All()
        {
            return _context.QuestionTypes.ToList();
        }
    }
}