using System.Collections.Generic;
using System.Linq;
using Database;
using SOh_ParkInspect.Repository.Interface;

namespace SOh_ParkInspect.Repository
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly Context _context;

        public ScheduleRepository(Context context)
        {
            _context = context;
        }

        public List<WeeklySchedule> All()
        {
            return _context.WeeklySchedules.ToList();
        }

        public List<WeeklySchedule> Find(string name)
        {
            return _context.WeeklySchedules.Where(s => $"{s.Employee.FirstName} {s.Employee.LastName}" == name).ToList();
        }
    }
}