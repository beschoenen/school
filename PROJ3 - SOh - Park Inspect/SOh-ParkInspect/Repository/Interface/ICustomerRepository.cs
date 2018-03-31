using System.Collections.Generic;
using Database;

namespace SOh_ParkInspect.Repository.Interface
{
    public interface ICustomerRepository
    {
        List<Customer> All();

        Customer Find(int id);

        bool Add(Customer customer);

        bool Update(Customer customer);
    }
}