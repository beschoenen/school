using System.Collections.Generic;
using System.Linq;
using Database;
using SOh_ParkInspect.Repository.Interface;

namespace SOh_ParkInspect.Repository
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly Context _context;

        public QuestionRepository(Context context)
        {
            _context = context;
        }

        public List<Question> All()
        {
            return _context.Questions.ToList();
        }

        public Question Find(int id)
        {
            return _context.Questions.FirstOrDefault(q => q.ID == id);
        }
    }
}