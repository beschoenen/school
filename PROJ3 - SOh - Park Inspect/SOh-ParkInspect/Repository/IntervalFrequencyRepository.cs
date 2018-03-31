using System.Collections.Generic;
using System.Linq;
using Database;
using SOh_ParkInspect.Repository.Interface;

namespace SOh_ParkInspect.Repository
{
    public class IntervalFrequencyRepository : IIntervalFrequencyRepository
    {
        private readonly Context _context;

        public IntervalFrequencyRepository(Context context)
        {
            _context = context;
        }

        public List<IntervalFrequency> All()
        {
            return _context.IntervalFrequencies.ToList();
        }

        public IntervalFrequency Find(string intervalFrequency)
        {
            return _context.IntervalFrequencies.Where(i => i.Name == intervalFrequency).ToList()[0];
        }
    }
}