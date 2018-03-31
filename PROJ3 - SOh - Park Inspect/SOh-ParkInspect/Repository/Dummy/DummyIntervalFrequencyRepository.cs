using System.Collections.Generic;
using System.Linq;
using Database;
using SOh_ParkInspect.Repository.Interface;

namespace SOh_ParkInspect.Repository.Dummy
{
    public class DummyIntervalFrequencyRepository : IIntervalFrequencyRepository
    {
        private readonly List<IntervalFrequency> _intervalFrequencies;

        public DummyIntervalFrequencyRepository()
        {
            _intervalFrequencies = new List<IntervalFrequency>
            {
                new IntervalFrequency { Name = IntervalFrequency.Year },
                new IntervalFrequency { Name = IntervalFrequency.Month },
                new IntervalFrequency { Name = IntervalFrequency.Week },
                new IntervalFrequency { Name = IntervalFrequency.Day },
                new IntervalFrequency { Name = IntervalFrequency.Hour },
                new IntervalFrequency { Name = IntervalFrequency.Minute }
            };
        }

        public List<IntervalFrequency> All()
        {
            return _intervalFrequencies;
        }

        public IntervalFrequency Find(string intervalFrequency)
        {
            return _intervalFrequencies.FirstOrDefault(i => i.Name == intervalFrequency);
        }
    }
}