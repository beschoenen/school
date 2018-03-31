using System;
using System.Collections.Generic;
using System.Linq;
using Database;
using SOh_ParkInspect.Repository.Interface;

namespace SOh_ParkInspect.Repository.Dummy
{
    public class DummyScheduleRepository : IScheduleRepository
    {
        private readonly List<WeeklySchedule> _schedules;

        public DummyScheduleRepository()
        {
            var employees = new List<Employee>
            {
                new Employee { ID = 1, FirstName = "John", LastName = "Doe" },
                new Employee { ID = 2, FirstName = "Al", LastName = "Paccino" },
                new Employee { ID = 3, FirstName = "IDont", LastName = "Know" }
            };

            _schedules = new List<WeeklySchedule>
            {
                //1 Week of Employee[0]/EmployeeId=1
                new WeeklySchedule { EmployeeID = employees[0].ID, Day = 1, Employee = employees[0], StartTime = TimeSpan.FromHours(6), EndTime = TimeSpan.FromHours(14) },
                new WeeklySchedule { EmployeeID = employees[0].ID, Day = 2, Employee = employees[0], StartTime = TimeSpan.FromHours(8), EndTime = TimeSpan.FromHours(14) },
                new WeeklySchedule { EmployeeID = employees[0].ID, Day = 3, Employee = employees[0], StartTime = TimeSpan.FromHours(0), EndTime = TimeSpan.FromHours(0) },
                new WeeklySchedule { EmployeeID = employees[0].ID, Day = 4, Employee = employees[0], StartTime = TimeSpan.FromHours(0), EndTime = TimeSpan.FromHours(0) },
                new WeeklySchedule { EmployeeID = employees[0].ID, Day = 5, Employee = employees[0], StartTime = TimeSpan.FromHours(18), EndTime = TimeSpan.FromHours(24) },
                new WeeklySchedule { EmployeeID = employees[0].ID, Day = 6, Employee = employees[0], StartTime = TimeSpan.FromHours(0), EndTime = TimeSpan.FromHours(0) },
                new WeeklySchedule { EmployeeID = employees[0].ID, Day = 0, Employee = employees[0], StartTime = TimeSpan.FromHours(0), EndTime = TimeSpan.FromHours(0) },

                //1 Week of Employee[1]/EmployeeId=2
                new WeeklySchedule { EmployeeID = employees[1].ID, Day = 1, Employee = employees[1], StartTime = TimeSpan.FromHours(0), EndTime = TimeSpan.FromHours(0) },
                new WeeklySchedule { EmployeeID = employees[1].ID, Day = 2, Employee = employees[1], StartTime = TimeSpan.FromHours(0), EndTime = TimeSpan.FromHours(0) },
                new WeeklySchedule { EmployeeID = employees[1].ID, Day = 3, Employee = employees[1], StartTime = TimeSpan.FromHours(0), EndTime = TimeSpan.FromHours(0) },
                new WeeklySchedule { EmployeeID = employees[1].ID, Day = 4, Employee = employees[1], StartTime = TimeSpan.FromHours(8), EndTime = TimeSpan.FromHours(14) },
                new WeeklySchedule { EmployeeID = employees[1].ID, Day = 5, Employee = employees[1], StartTime = TimeSpan.FromHours(12), EndTime = TimeSpan.FromHours(18) },
                new WeeklySchedule { EmployeeID = employees[1].ID, Day = 6, Employee = employees[1], StartTime = TimeSpan.FromHours(8), EndTime = TimeSpan.FromHours(14) },
                new WeeklySchedule { EmployeeID = employees[1].ID, Day = 0, Employee = employees[1], StartTime = TimeSpan.FromHours(0), EndTime = TimeSpan.FromHours(0) },

                //1 Week of Employee[2]/EmployeeId=3
                new WeeklySchedule { EmployeeID = employees[2].ID, Day = 1, Employee = employees[2], StartTime = TimeSpan.FromHours(8), EndTime = TimeSpan.FromHours(14) },
                new WeeklySchedule { EmployeeID = employees[2].ID, Day = 2, Employee = employees[2], StartTime = TimeSpan.FromHours(8), EndTime = TimeSpan.FromHours(14) },
                new WeeklySchedule { EmployeeID = employees[2].ID, Day = 3, Employee = employees[2], StartTime = TimeSpan.FromHours(8), EndTime = TimeSpan.FromHours(14) },
                new WeeklySchedule { EmployeeID = employees[2].ID, Day = 4, Employee = employees[2], StartTime = TimeSpan.FromHours(8), EndTime = TimeSpan.FromHours(14) },
                new WeeklySchedule { EmployeeID = employees[2].ID, Day = 5, Employee = employees[2], StartTime = TimeSpan.FromHours(8), EndTime = TimeSpan.FromHours(14) },
                new WeeklySchedule { EmployeeID = employees[2].ID, Day = 6, Employee = employees[2], StartTime = TimeSpan.FromHours(0), EndTime = TimeSpan.FromHours(0) },
                new WeeklySchedule { EmployeeID = employees[2].ID, Day = 0, Employee = employees[2], StartTime = TimeSpan.FromHours(0), EndTime = TimeSpan.FromHours(0) }
            };
        }

        public List<WeeklySchedule> All()
        {
            return _schedules;
        }

        public List<WeeklySchedule> Find(string name)
        {
            return _schedules.Where(s => $"{s.Employee.FirstName} {s.Employee.LastName}" == name).ToList();
        }
    }
}