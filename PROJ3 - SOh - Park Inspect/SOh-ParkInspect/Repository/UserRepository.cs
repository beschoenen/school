using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Windows.Controls;
using Database;
using SOh_ParkInspect.Repository.Interface;
using static BCrypt.Net.BCrypt;

namespace SOh_ParkInspect.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly Context _context;

        public UserRepository(Context context)
        {
            _context = context;
        }

        public List<Employee> All()
        {
            return _context.Employees.ToList();
        }

        public Employee Find(string email)
        {
            return _context.Employees.FirstOrDefault(e => e.Email == email);
        }

        public Employee Find(int id)
        {
            return _context.Employees.FirstOrDefault(e => e.ID == id);
        }

        public bool Add(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();

            return true;
        }

        public bool Update(Employee employee)
        {
            try
            {
                _context.Employees.AddOrUpdate(employee);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool Delete(Employee employee)
        {
            _context.Employees.Remove(employee);
            _context.SaveChanges();

            return true;
        }

        public bool ChangePassword(string email, PasswordBox oldPasswordBox, PasswordBox newPasswordBox)
        {
            var employee = Find(email);

            if (employee == null || !Verify(oldPasswordBox.Password, employee.Password))
            {
                return false;
            }

            employee.Password = HashPassword(newPasswordBox.Password);

            return Update(employee);
        }
    }
}