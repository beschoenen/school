using System.Collections.Generic;
using Database;

namespace SOh_ParkInspect.Repository.Interface
{
    public interface IScheduleRepository
    {
        List<WeeklySchedule> All();

        List<WeeklySchedule> Find(string name);
    }
}