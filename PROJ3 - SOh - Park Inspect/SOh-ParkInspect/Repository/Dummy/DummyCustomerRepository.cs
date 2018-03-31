using System.Collections.Generic;
using System.Linq;
using Database;
using SOh_ParkInspect.Repository.Interface;

namespace SOh_ParkInspect.Repository.Dummy
{
    public class DummyCustomerRepository : ICustomerRepository
    {
        private readonly List<Customer> _customers;

        public DummyCustomerRepository()
        {
            _customers = new List<Customer>
            {
                new Customer
                {
                    ID = 1,
                    Address = new Address
                    {
                        City = "Eindhoven",
                        Country = "Nederland",
                        Street = "Ten Hagestraat",
                        Number = "6A",
                        ZipCode = "5611 EG"
                    },
                    Email = "quincy@qpark.nl",
                    Contact = "Quincy Park",
                    PhoneNumber = "123456789",
                    Name = "Q-Park"
                },
                new Customer
                {
                    ID = 2,
                    Address = new Address
                    {
                        City = "Den Bosch",
                        Country = "Nederland",
                        Street = "Hekellaan",
                        Number = "25",
                        ZipCode = "5211 LZ"
                    },
                    Email = "jan@stjan.nl",
                    Contact = "Maarten Janszoon",
                    PhoneNumber = "987654321",
                    Name = "St.-Jan"
                },
                new Customer
                {
                    ID = 3,
                    Address = new Address
                    {
                        City = "Eindhoven",
                        Country = "Nederland",
                        Street = "Ten Hagestraat",
                        Number = "6A",
                        ZipCode = "5611 EG"
                    },
                    Email = "quincy@qpark.nl",
                    Contact = "Quincy Park",
                    PhoneNumber = "123456789",
                    Name = "Mohamed"
                },
                                new Customer
                {
                    ID = 3,
                    Address = new Address
                    {
                        City = "Eindhoven",
                        Country = "Nederland",
                        Street = "Ten Hagestraat",
                        Number = "6A",
                        ZipCode = "5611 EG"
                    },
                    Email = "quincy@qpark.nl",
                    Contact = "Quincy Park",
                    PhoneNumber = "123456789",
                    Name = "Joep"
                },
                                                new Customer
                {
                    ID = 3,
                    Address = new Address
                    {
                        City = "Eindhoven",
                        Country = "Nederland",
                        Street = "Ten Hagestraat",
                        Number = "6A",
                        ZipCode = "5611 EG"
                    },
                    Email = "quincy@qpark.nl",
                    Contact = "Quincy Park",
                    PhoneNumber = "123456789",
                    Name = "Daan"
                },
            };
        }

        public List<Customer> All()
        {
            return _customers;
        }

        public Customer Find(int id)
        {
            return _customers.FirstOrDefault(e => e.ID == id);
        }

        public bool Update(Customer customer)
        {
            _customers.Remove(Find(customer.ID));
            _customers.Add(customer);

            return true;
        }

        public bool Add(Customer customer)
        {
            _customers.Add(customer);

            return true;
        }
    }
}