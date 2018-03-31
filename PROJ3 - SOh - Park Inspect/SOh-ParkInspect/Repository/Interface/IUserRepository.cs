using System.Collections.Generic;
using System.Windows.Controls;
using Database;

namespace SOh_ParkInspect.Repository.Interface
{
    public interface IUserRepository
    {
        List<Employee> All();

        Employee Find(int id);

        Employee Find(string email);

        bool Add(Employee employee);

        bool Update(Employee employee);

        bool Delete(Employee employee);

        bool ChangePassword(string email, PasswordBox oldPasswordBox, PasswordBox newPasswordBox);
    }
}