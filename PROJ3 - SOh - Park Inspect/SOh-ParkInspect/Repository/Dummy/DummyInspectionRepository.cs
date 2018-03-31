using System;
using System.Collections.Generic;
using System.Linq;
using Database;
using SOh_ParkInspect.Repository.Interface;

namespace SOh_ParkInspect.Repository.Dummy
{
    public class DummyInspectionRepository : IInspectionRepository
    {
        private readonly List<Inspection> _inspections;

        public DummyInspectionRepository()
        {
            _inspections = new List<Inspection>
            {
                new Inspection
                {
                    Task = new Task
                    {
                        DateTimeStart = new DateTime(2016, 04, 3),
                        ParkingLot = new ParkingLot
                        {
                            Address = new Address
                            {
                                Street = "Nijhofawdfstraat",
                                ZipCode = "652awd4AT",
                                City = "Nijmegeawdn",
                                Country = "Nededawrland",
                                Number = "awd5"
                            }
                        },
                        Customer = new Customer
                        {
                            Name = "Mohamed",
                            Email = "Allahu@Akbar.cs"
                        }
                    },
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
                    Task = new Task
                    {
                        DateTimeStart = new DateTime(2016, 04, 3),
                        ParkingLot = new ParkingLot
                        {
                            Address = new Address
                            {
                                Street = "Nijhofawdfstraat",
                                ZipCode = "652awd4AT",
                                City = "Nijmegeawdn",
                                Country = "Nededawrland",
                                Number = "awd5"
                            }
                        },
                        Customer = new Customer
                        {
                            Name = "Mohamed",
                            Email = "Allahu@Akbar.cs"
                        }
                    }
                }
            };
        }

        public List<Inspection> All()
        {
            return _inspections;
        }

        public Inspection Find(int id)
        {
            return _inspections.FirstOrDefault(i => i.ID == id);
        }

        public List<Inspection> FromUser(int id)
        {
            return _inspections.Where(x => x.InspectionInspectors.Select(e => e.Employee.ID).Contains(id)).ToList();
        }

        public bool Remove(Inspection inspection)
        {
            _inspections.Remove(inspection);

            return true;
        }

        public bool Add(Inspection inspection)
        {
            _inspections.Add(inspection);

            return true;
        }

        public bool Add(IEnumerable<Inspection> inspections)
        {
            _inspections.AddRange(inspections);

            return true;
        }

        public bool Update(Inspection inspection)
        {
            _inspections.Remove(_inspections.FirstOrDefault(i => i.ID == inspection.ID));
            _inspections.Add(inspection);

            return true;
        }
    }
}