using System.Collections.Generic;
using System.Linq;
using Database;
using SOh_ParkInspect.Repository.Interface;

namespace SOh_ParkInspect.Repository
{
    public class AnswerSetValueRepository : IAnswerSetValueRepository
    {
        private readonly Context _context;

        public AnswerSetValueRepository(Context context)
        {
            _context = context;
        }

        public List<AnswerSetValue> All()
        {
            return _context.AnswerSetValues.ToList();
        }
    }
}