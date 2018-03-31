using System.Collections.Generic;
using Database;
using SOh_ParkInspect.Repository.Interface;

namespace SOh_ParkInspect.Repository.Dummy
{
    public class DummyAddressRepository : IAddressRepository
    {
        private readonly List<Address> addresses;

        public DummyAddressRepository()
        {
            addresses = new List<Address>
            {
                new Address
                {
                    City = "Nijmegen",
                    Province = "Gelderland"
                },
                new Address
                {
                    City = "Arnhem"
                },
                new Address
                {
                    City = "Arnhem"
                },
                new Address
                {
                    City = "Arnhem"
                }
            };
        }

        public List<Address> All()
        {
            return addresses;
        }
    }
}