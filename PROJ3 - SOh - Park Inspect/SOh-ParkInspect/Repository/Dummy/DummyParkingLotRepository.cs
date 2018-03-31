using System.Collections.Generic;
using Database;
using SOh_ParkInspect.Repository.Interface;

namespace SOh_ParkInspect.Repository.Dummy
{
    public class DummyParkingLotRepository : IParkingLotRepository
    {
        private readonly List<ParkingLot> _parkingLots;

        public DummyParkingLotRepository()
        {
            _parkingLots = new List<ParkingLot>
            {
                new ParkingLot
                {
                    ID = 1,
                    GeoLatitude = 51.688068,
                    GeoLongitude = 5.287249,
                    Address = new Address
                    {
                        City = "Den Bosch",
                        Country = "Nederland",
                        Street = "Onderwijsboulevard",
                        Number = "37",
                        ZipCode = "5789HA"
                    },
                    Tasks = new List<Task>
                    {
                        new Task
                        {
                            ID = 1
                        }
                    }
                },
                new ParkingLot
                {
                    ID = 2,
                    GeoLatitude = 51.683632,
                    GeoLongitude = 5.280692,
                    Address = new Address
                    {
                        City = "Den Bosch",
                        Country = "Nederland",
                        Street = "Frederiklaan",
                        Number = "123ab",
                        ZipCode = "1234GD"
                    },
                    Tasks = new List<Task>
                    {
                        new Task
                        {
                            ID = 3
                        },
                        new Task
                        {
                            ID = 4
                        }
                    }
                },
                new ParkingLot
                {
                    ID = 3,
                    GeoLatitude = 51.442645,
                    GeoLongitude = 5.486224,
                    Address = new Address
                    {
                        City = "Amsterdam",
                        Country = "Nederland",
                        Street = "De Dam",
                        Number = "1",
                        ZipCode = "4321ZX"
                    },
                    Tasks = new List<Task>
                    {
                        new Task
                        {
                            ID = 2
                        },
                        new Task
                        {
                            ID = 5
                        }
                    }
                },
                new ParkingLot
                {
                    ID = 4,
                    GeoLatitude = 51.617959,
                    GeoLongitude = 5.525639,
                    Address = new Address
                    {
                        City = "Veghel",
                        Country = "Nederland",
                        Street = "Pater van Den Es laan",
                        Number = "4",
                        ZipCode = "5462 GG"
                    },
                    Tasks = new List<Task>
                    {
                        new Task
                        {
                            ID = 8
                        },
                        new Task
                        {
                            ID = 14
                        }
                    }
                }
                ,
                new ParkingLot
                {
                    ID = 5,
                    GeoLatitude = 51.380191,
                    GeoLongitude = 5.579426,
                    Address = new Address
                    {
                        City = "Heeze",
                        Country = "Nederland",
                        Street = "Vullinghspark",
                        Number = "9",
                        ZipCode = "5591 DA"
                    },
                    Tasks = new List<Task>
                    {
                        new Task
                        {
                            ID = 11
                        }
                    }
                }
            };
        }

        public List<ParkingLot> All()
        {
            return _parkingLots;
        }

        public bool Add(ParkingLot parkingLot)
        {
            _parkingLots.Add(parkingLot);

            return true;
        }
    }
}