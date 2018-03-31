using System.Collections.Generic;
using System.Linq;
using Database;
using SOh_ParkInspect.Repository.Interface;

namespace SOh_ParkInspect.Repository
{
    public class JobTitleRepository : IJobTitleRepository
    {
        private readonly Context _context;

        public JobTitleRepository(Context context)
        {
            _context = context;
        }

        public List<JobTitle> All()
        {
            return _context.JobTitles.ToList();
        }
    }
}