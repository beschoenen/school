using System;
using System.Collections.Generic;
using System.Linq;
using Database;
using SOh_ParkInspect.Repository.Interface;

namespace SOh_ParkInspect.Repository.Dummy
{
    public class DummyAvailabilityRepository : IAvailabilityRepository
    {
        private readonly List<Availability> _availabilities;

        public DummyAvailabilityRepository()
        {
            _availabilities = new List<Availability>
            {
                new Availability
                {
                    EmployeeID = 0,
                    StartDateTime = DateTime.Now,
                    EndDateTime = DateTime.Now.AddDays(1),
                    Available = true,
                    Remark = "Daan beschikbaar"
                },
                new Availability
                {
                    EmployeeID = 0,
                    StartDateTime = DateTime.Now.AddHours(2),
                    EndDateTime = DateTime.Now.AddHours(5),
                    Available = true,
                    Remark = "Vincent beschikbaar"
                },
                new Availability
                {
                    EmployeeID = 0,
                    StartDateTime = DateTime.Now.AddHours(-4),
                    EndDateTime = DateTime.Now.AddHours(-1),
                    Available = true,
                    Remark = "Mohamed beschikbaar"
                }
            };
        }

        public List<Availability> All()
        {
            return _availabilities;
        }

        public List<Availability> All(Employee employee)
        {
            return _availabilities.Where(a => a.EmployeeID == employee.ID).ToList();
        }

        public void Save(Employee employee, List<Availability> availabilities)
        {
            All(employee).ForEach(x => _availabilities.Remove(x));
            _availabilities.AddRange(availabilities);
        }
    }
}