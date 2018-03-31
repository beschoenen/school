using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using Database;
using SOh_ParkInspect.Repository.Interface;
using static BCrypt.Net.BCrypt;

namespace SOh_ParkInspect.Repository.Dummy
{
    public class DummyUserRepository : IUserRepository
    {
        private readonly List<Employee> _employees;

        public DummyUserRepository()
        {
            Answer a1;
            Answer a2;

            var q1 = new ChecklistQuestion
            {
                Question = new Question
                {
                    Text = "Is het antwoord nee?",
                    QuestionType = new QuestionType { Name = "Multiple Choice" },
                    AnswerSetValues = new List<AnswerSetValue>
                    {
                        new AnswerSetValue { Value = "Ja" },
                        new AnswerSetValue { Value = "Nee" },
                        new AnswerSetValue { Value = "Misschien" }
                    },
                    Answers = new List<Answer>()
                }
            };

            var q2 = new ChecklistQuestion
            {
                Question = new Question
                {
                    Text = "Is het antwoord ja?",
                    QuestionType = new QuestionType { Name = "Multiple Choice" },
                    AnswerSetValues = new List<AnswerSetValue>
                    {
                        new AnswerSetValue { Value = "Ja" },
                        new AnswerSetValue { Value = "Nee" },
                        new AnswerSetValue { Value = "Misschien" }
                    },
                    Answers = new List<Answer>()
                }
            };

            a1 = new Answer { Text = "Ja", Question = q1.Question };
            a2 = new Answer { Text = "Nee", Question = q2.Question };

            q1.Question.Answers.Add(a1);
            q2.Question.Answers.Add(a2);

            _employees = new List<Employee>
            {
                new Employee
                {
                    ID = 0,
                    Email = "daan@daan.nl",
                    FirstName = "Daan",
                    LastName = "Willems",
                    TelephoneNumber = "0612345678",
                    DateOfBirth = DateTime.Now,
                    DateOfEmployment = DateTime.Now,
                    WeeklySchedules = new List<WeeklySchedule>
                    {
                        new WeeklySchedule { Day = 1, StartTime = TimeSpan.FromHours(8), EndTime = TimeSpan.FromHours(14) },
                        new WeeklySchedule { Day = 2, StartTime = TimeSpan.FromHours(8), EndTime = TimeSpan.FromHours(14) },
                        new WeeklySchedule { Day = 3, StartTime = TimeSpan.FromHours(8), EndTime = TimeSpan.FromHours(14) },
                        new WeeklySchedule { Day = 4, StartTime = TimeSpan.FromHours(8), EndTime = TimeSpan.FromHours(0) },
                        new WeeklySchedule { Day = 5, StartTime = TimeSpan.FromHours(8), EndTime = TimeSpan.FromHours(4) },
                        new WeeklySchedule { Day = 6, StartTime = TimeSpan.FromHours(8), EndTime = TimeSpan.FromHours(24) },
                        new WeeklySchedule { Day = 7, StartTime = TimeSpan.FromHours(8), EndTime = TimeSpan.FromHours(0) }
                    },
                    Address = new Address
                    {
                        City = "Den Bosch",
                        Country = "Nederland",
                        Street = "StraatWeg",
                        Number = "22B",
                        ZipCode = "1234AB"
                    },
                    JobTitle = new JobTitle
                    {
                        Description = "Admin",
                        Name = "Admin"
                    },
                    Password = HashPassword("123"),
                    Availabilities = new List<Availability>
                    {
                        new Availability
                        {
                            EmployeeID = 0,
                            StartDateTime = DateTime.Now,
                            EndDateTime = DateTime.Now.AddDays(1),
                            Available = true,
                            Remark = "Daan beschikbaar"
                        }
                    },
                    InspectionInspectors = new List<InspectionInspector>
                    {
                        new InspectionInspector
                        {
                            Inspection = new Inspection
                            {
                                Task = new Task
                                {
                                    ParkingLot = new ParkingLot
                                    {
                                        Address = new Address
                                        {
                                            Street = "Test",
                                            Number = "5",
                                            ZipCode = "6524AT",
                                            City = "Nijmegen",
                                            Country = "Nederland"
                                        }
                                    }
                                },
                                DateTimePlanned = DateTime.Now
                            }
                        },
                        new InspectionInspector
                        {
                            Inspection = new Inspection
                            {
                                Task = new Task
                                {
                                    ParkingLot = new ParkingLot
                                    {
                                        Address = new Address
                                        {
                                            Street = "test 6",
                                            Number = "5",
                                            ZipCode = "6524AT",
                                            City = "Nijmegen",
                                            Country = "Nederland"
                                        }
                                    }
                                },
                                DateTimePlanned = DateTime.Now.AddDays(-2),
                                Checklist = new Checklist
                                {
                                    ChecklistQuestions = new List<ChecklistQuestion>
                                    {
                                        new ChecklistQuestion
                                        {
                                            Question = new Question
                                            {
                                                Text = "Is het antwoord nee?",
                                                QuestionType = new QuestionType { Name = QuestionType.MultipleChoice },
                                                AnswerSetValues = new List<AnswerSetValue>
                                                {
                                                    new AnswerSetValue { Value = "Ja" },
                                                    new AnswerSetValue { Value = "Nee" },
                                                    new AnswerSetValue { Value = "Misschien" }
                                                }
                                            }
                                        },
                                        new ChecklistQuestion
                                        {
                                            Question = new Question
                                            {
                                                Text = "Is het antwoord ja?",
                                                QuestionType = new QuestionType { Name = QuestionType.MultipleChoice },
                                                AnswerSetValues = new List<AnswerSetValue>
                                                {
                                                    new AnswerSetValue { Value = "Ja" },
                                                    new AnswerSetValue { Value = "Nee" },
                                                    new AnswerSetValue { Value = "Misschien" }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                new Employee
                {
                    ID = 1,
                    Email = "daan@willems.nl",
                    FirstName = "Daan",
                    LastName = "Ververda",
                    Password = HashPassword("123"),
                    Availabilities = new List<Availability>
                    {
                        new Availability
                        {
                            EmployeeID = 0,
                            StartDateTime = DateTime.Now,
                            EndDateTime = DateTime.Now.AddDays(1),
                            Available = true,
                            Remark = "Daan beschikbaar"
                        }
                    },
                    TelephoneNumber = "0612345678",
                    DateOfBirth = DateTime.Now,
                    DateOfEmployment = DateTime.Now,
                    Address = new Address
                    {
                        City = "Den Bosch",
                        Country = "Nederland",
                        Street = "StraatWeg",
                        Number = "22B",
                        ZipCode = "1234AB"
                    },
                    JobTitle = new JobTitle
                    {
                        Description = "Admin",
                        Name = "Admin"
                    },
                    WeeklySchedules = new List<WeeklySchedule>
                    {
                        new WeeklySchedule { Day = 1, StartTime = TimeSpan.FromHours(8), EndTime = TimeSpan.FromHours(14) },
                        new WeeklySchedule { Day = 2, StartTime = TimeSpan.FromHours(8), EndTime = TimeSpan.FromHours(14) },
                        new WeeklySchedule { Day = 3, StartTime = TimeSpan.FromHours(8), EndTime = TimeSpan.FromHours(14) },
                        new WeeklySchedule { Day = 4, StartTime = TimeSpan.FromHours(8), EndTime = TimeSpan.FromHours(0) },
                        new WeeklySchedule { Day = 5, StartTime = TimeSpan.FromHours(8), EndTime = TimeSpan.FromHours(4) },
                        new WeeklySchedule { Day = 6, StartTime = TimeSpan.FromHours(8), EndTime = TimeSpan.FromHours(24) },
                        new WeeklySchedule { Day = 7, StartTime = TimeSpan.FromHours(8), EndTime = TimeSpan.FromHours(0) }
                    },
                    InspectionInspectors = new List<InspectionInspector>
                    {
                        new InspectionInspector
                        {
                            Inspection = new Inspection
                            {
                                Task = new Task
                                {
                                    ParkingLot = new ParkingLot
                                    {
                                        Address = new Address
                                        {
                                            Street = "Nijhoffstraat 5",
                                            Number = "5",
                                            ZipCode = "6524AT",
                                            City = "Nijmegen",
                                            Country = "Nederland"
                                        }
                                    }
                                },
                                DateTimePlanned = DateTime.Now
                            }
                        }
                    }
                },
                new Employee
                {
                    ID = 2,
                    FirstName = "Vincent",
                    LastName = "Brouwers"
                },
                new Employee
                {
                    ID = 3,
                    FirstName = "Mohamed",
                    LastName = "Hamoudi"
                }
            };
        }

        public List<Employee> All()
        {
            return _employees;
        }

        public Employee Find(int id)
        {
            return _employees.FirstOrDefault(e => e.ID == id);
        }

        public Employee Find(string email)
        {
            return _employees.Find(x => x.Email == email);
        }

        public bool Add(Employee employee)
        {
            _employees.Add(employee);

            return true;
        }

        public bool Update(Employee employee)
        {
            var oldEmployee = Find(employee.ID);

            if (oldEmployee == null)
            {
                return false;
            }

            _employees.Remove(oldEmployee);
            _employees.Add(employee);

            return true;
        }

        public bool Delete(Employee employee)
        {
            if (employee == null)
            {
                return false;
            }

            _employees.Remove(employee);

            return true;
        }

        public bool ChangePassword(string email, PasswordBox oldPasswordBox, PasswordBox newPasswordBox)
        {
            var employee = Find(email);

            if (employee == null)
            {
                return false;
            }

            _employees.Remove(employee);

            employee.Password = HashPassword(newPasswordBox.Password);
            _employees.Add(employee);

            return true;
        }
    }
}