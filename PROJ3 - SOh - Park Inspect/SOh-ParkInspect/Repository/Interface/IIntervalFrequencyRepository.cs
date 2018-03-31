using System.Collections.Generic;
using Database;

namespace SOh_ParkInspect.Repository.Interface
{
    public interface IIntervalFrequencyRepository
    {
        List<IntervalFrequency> All();

        IntervalFrequency Find(string intervalFrequency);
    }
}