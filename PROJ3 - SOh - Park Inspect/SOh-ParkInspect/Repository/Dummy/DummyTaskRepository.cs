using System;
using System.Collections.Generic;
using Database;
using SOh_ParkInspect.Repository.Interface;

namespace SOh_ParkInspect.Repository.Dummy
{
    public class DummyTaskRepository : ITaskRepository
    {
        private readonly List<Task> _tasks;

        public DummyTaskRepository()
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

            _tasks = new List<Task>
            {
                new Task
                {
                    ParkingLot = new ParkingLot
                    {
                        Address = new Address
                        {
                            Street = "testtest",
                            ZipCode = "6524AT",
                            City = "Nijmegen",
                            Country = "Nederland",
                            Province = "Gelderland",
                            Number = "5"
                        }
                    },
                    Customer = new Customer
                    {
                        Name = "Mohamed",
                        Email = "Allahu@Akbar.cs"
                    },
                    DateTimeEnd = DateTime.Now,
                    DateTimeStart = DateTime.Now.AddMinutes(-60),
                    DatetimeCreated = DateTime.Now,
                    Inspections = new List<Inspection>
                    {
                        new Inspection
                        {
                            Checklist = new Checklist
                            {
                                Name = "lala",
                                ChecklistQuestions = new List<ChecklistQuestion>
                                {
                                    q1, q1
                                }
                            },
                            Answers = new List<Answer>
                            {
                                a1, a2
                            },
                            DateTimePlanned = DateTime.Now,
                            DateTimeDone = DateTime.Now.AddMinutes(50),
                            InspectionInspectors = new List<InspectionInspector>
                            {
                                new InspectionInspector
                                {
                                    InspectorID = 1
                                }
                            }
                        },
                        new Inspection
                        {
                            DateTimePlanned = DateTime.Now,
                            DateTimeDone = DateTime.Now.AddMinutes(100),
                            InspectionInspectors = new List<InspectionInspector>
                            {
                                new InspectionInspector
                                {
                                    InspectorID = 1
                                }
                            }
                        }

                    }
                },
                new Task
                {
                    ParkingLot = new ParkingLot
                    {
                        Address = new Address
                        {
                            Street = "Nijhoffstraat",
                            ZipCode = "6524AT",
                            City = "Nijmegen",
                            Country = "Nederland",
                            Province = "Gelderland",
                            Number = "5"
                        }
                    },
                    Customer = new Customer
                    {
                        Name = "Daan",
                        Email = "Allahu@Akbar.cs"
                    },
                    DateTimeEnd = DateTime.Now,
                    DateTimeStart = DateTime.Now.AddMinutes(-50),
                    DatetimeCreated = DateTime.Now,
                    Inspections = new List<Inspection>
                    {
                        new Inspection
                        {
                            Checklist = new Checklist
                            {
                                Name = "lala",
                                ChecklistQuestions = new List<ChecklistQuestion>
                                {
                                    q1, q1
                                }
                            },
                            Answers = new List<Answer>
                            {
                                a1, a2
                            },
                            DateTimePlanned = DateTime.Now,
                            DateTimeDone = DateTime.Now.AddMinutes(30),
                            InspectionInspectors = new List<InspectionInspector>
                            {
                                new InspectionInspector
                                {
                                    InspectorID = 1
                                }
                            }
                        },
                        new Inspection
                        {
                            DateTimePlanned = DateTime.Now,
                            DateTimeDone = DateTime.Now.AddMinutes(20),
                            InspectionInspectors = new List<InspectionInspector>
                            {
                                new InspectionInspector
                                {
                                    InspectorID = 1
                                }
                            }
                        }

                    }
                },
                new Task
                {
                    ParkingLot = new ParkingLot
                    {
                        Address = new Address
                        {
                            Street = "Nijhoffstraat",
                            ZipCode = "6524AT",
                            City = "Nijmegen",
                            Country = "Nederland",
                            Province = "Gelderland",
                            Number = "5"
                        }
                    },
                    Customer = new Customer
                    {
                        Name = "Vincent",
                        Email = "Allahu@Akbar.cs"
                    },
                    DateTimeEnd = DateTime.Now,
                    DateTimeStart = DateTime.Now.AddMinutes(-40),
                    DatetimeCreated = DateTime.Now
                },
                new Task
                {
                    ParkingLot = new ParkingLot
                    {
                        Address = new Address
                        {
                            Street = "Nijhoffstraat",
                            ZipCode = "6524AT",
                            City = "Nijmegen",
                            Country = "Nederland",
                            Province = "Gelderland",
                            Number = "5"
                        }
                    },
                    Customer = new Customer
                    {
                        Name = "Joep",
                        Email = "Allahu@Akbar.cs"
                    },
                    DateTimeEnd = DateTime.Now,
                    DateTimeStart = DateTime.Now.AddMinutes(-40),
                    DatetimeCreated = DateTime.Now.AddMonths(-4),
                                        Inspections = new List<Inspection>
                    {
                        new Inspection
                        {
                            Checklist = new Checklist
                            {
                                Name = "lala",
                                ChecklistQuestions = new List<ChecklistQuestion>
                                {
                                    q1, q1
                                }
                            },
                            Answers = new List<Answer>
                            {
                                a1, a2
                            },
                            DateTimePlanned = DateTime.Now,
                            DateTimeDone = DateTime.Now.AddMinutes(6),
                            InspectionInspectors = new List<InspectionInspector>
                            {
                                new InspectionInspector
                                {
                                    InspectorID = 1
                                }
                            }
                        },
                        new Inspection
                        {
                            DateTimePlanned = DateTime.Now,
                            DateTimeDone = DateTime.Now.AddMinutes(80),
                            InspectionInspectors = new List<InspectionInspector>
                            {
                                new InspectionInspector
                                {
                                    InspectorID = 1
                                }
                            }
                        }

                    }
                },
                new Task
                {
                    ParkingLot = new ParkingLot
                    {
                        Address = new Address
                        {
                            Street = "Nijhoffstraat",
                            ZipCode = "6524AT",
                            City = "Arnhem",
                            Country = "Nederland",
                            Province = "Limburg",
                            Number = "5"
                        }
                    },
                    Customer = new Customer
                    {
                        Name = "Allahu",
                        Email = "Allahu@Akbar.cs"
                    },
                    DateTimeEnd = DateTime.Now,
                    DateTimeStart = DateTime.Now.AddMinutes(-40),
                    DatetimeCreated = DateTime.Now.AddMonths(-2)
                },
                                new Task
                {
                    ParkingLot = new ParkingLot
                    {
                        Address = new Address
                        {
                            Street = "Nijhoffstraat",
                            ZipCode = "6524AT",
                            City = "Arnhem",
                            Country = "Nederland",
                            Province = "Gelderland",
                            Number = "5"
                        }
                    },
                    Customer = new Customer
                    {
                        Name = "God",
                        Email = "Allahu@Akbar.cs"
                    },
                    DateTimeEnd = DateTime.Now,
                    DateTimeStart = DateTime.Now.AddMinutes(-40),
                    DatetimeCreated = DateTime.Now.AddMonths(-3)
                },
                new Task
                {
                    ID = 12,
                    DateTimeStart = new DateTime(2016, 04, 2),
                    ParkingLot = new ParkingLot
                    {
                        Address = new Address
                        {
                            Street = "Nijhoffstraat",
                            ZipCode = "6524AT",
                            City = "Nijmegen",
                            Province = "Gelderland",
                            Country = "Nederland",
                            Number = "5"
                        }
                    },
                    Customer = new Customer
                    {
                        Name = "Christus",
                        Email = "Allahu@Akbar.cs"
                    }
                },
                new Task
                {
                    ID = 13,
                    DateTimeStart = new DateTime(2016, 04, 3),
                    ParkingLot = new ParkingLot
                    {
                        Address = new Address
                        {
                            Street = "Nijhofawdfstraat",
                            ZipCode = "652awd4AT",
                            City = "Nijmegeawdn",
                            Country = "Nededawrland",
                            Province = "Gelderland",
                            Number = "awd5"
                        }
                    },
                    Customer = new Customer
                    {
                        Name = "Appel",
                        Email = "Allahu@Akbar.cs"
                    }
                },
                new Task
                {
                    ID = 1,
                    DateTimeStart = new DateTime(2016, 04, 3),
                    ParkingLot = new ParkingLot { ID = 1 }
                },
                new Task
                {
                    ID = 2,
                    DateTimeStart = new DateTime(2016, 04, 3),
                    ParkingLot = new ParkingLot { ID = 3 }
                },
                new Task
                {
                    ID = 3,
                    DateTimeStart = new DateTime(2016, 04, 3),
                    ParkingLot = new ParkingLot { ID = 2 }
                },
                new Task
                {
                    ID = 4,
                    DateTimeStart = new DateTime(2016, 04, 3),
                    ParkingLot = new ParkingLot { ID = 2 }
                },
                new Task
                {
                    ID = 5,
                    DateTimeStart = new DateTime(2016, 1, 3),
                    ParkingLot = new ParkingLot { ID = 3 }
                },
                new Task
                {
                    ID = 6,
                    DateTimeStart = new DateTime(2016, 8, 3),
                    ParkingLot = new ParkingLot { ID = 3 }
                },
                new Task
                {
                    ID = 7,
                    DateTimeStart = new DateTime(2016, 04, 24),
                    ParkingLot = new ParkingLot { ID = 3 }
                },
                new Task
                {
                    ID = 8,
                    DateTimeStart = new DateTime(2016, 7, 3),
                    ParkingLot = new ParkingLot { ID = 4 }
                },
                new Task
                {
                    ID = 9,
                    DateTimeStart = new DateTime(2015, 04, 3),
                    ParkingLot = new ParkingLot { ID = 3 }
                },
                new Task
                {
                    ID = 10,
                    DateTimeStart = new DateTime(2014, 04, 3),
                    ParkingLot = new ParkingLot { ID = 3 }
                },
                new Task
                {
                    ID = 11,
                    DateTimeStart = new DateTime(2016, 05, 2),
                    ParkingLot = new ParkingLot { ID = 5 }
                },
                new Task
                {
                    ID = 14,
                    DateTimeStart = new DateTime(2016, 05, 2),
                    ParkingLot = new ParkingLot { ID = 4 }
                }
            };
        }

        public bool Add(Task task)
        {
            _tasks.Add(task);

            return true;
        }

        public bool Delete(Task task)
        {
            throw new NotImplementedException();
        }

        public List<Task> All()
        {
            return _tasks;
        }
    }
}