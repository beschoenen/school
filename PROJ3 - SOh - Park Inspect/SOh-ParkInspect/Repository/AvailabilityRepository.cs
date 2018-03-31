using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using Database;
using SOh_ParkInspect.Repository.Interface;

namespace SOh_ParkInspect.Repository
{
    public class AvailabilityRepository : IAvailabilityRepository
    {
        private readonly Context _context;

        public AvailabilityRepository(Context context)
        {
            _context = context;
        }

        public List<Availability> All()
        {
            return _context.Availabilities.ToList();
        }

        public List<Availability> All(Employee employee)
        {
            return _context.Availabilities.Where(a => a.EmployeeID == employee.ID).ToList();
        }

        public void Save(Employee employee, List<Availability> availabilities)
        {
            _context.Availabilities.RemoveRange(All(employee));

            _context.Availabilities.AddRange(availabilities);
            _context.SaveChanges();
        }
    }
}