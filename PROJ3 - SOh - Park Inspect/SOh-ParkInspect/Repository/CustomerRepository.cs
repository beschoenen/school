using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Linq;
using Database;
using SOh_ParkInspect.Repository.Interface;

namespace SOh_ParkInspect.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly Context _context;

        public CustomerRepository(Context context)
        {
            _context = context;
        }

        public List<Customer> All()
        {
            return _context.Customers.ToList();
        }

        public Customer Find(int id)
        {
            return _context.Customers.FirstOrDefault(c => c.ID == id);
        }

        public bool Add(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();

            return true;
        }

        public bool Update(Customer customer)
        {
            _context.Customers.AddOrUpdate(customer);
            _context.SaveChanges();

            return true;
        }
    }
}