using System.Collections.Generic;
using Database;

namespace SOh_ParkInspect.Repository.Interface
{
    public interface IAvailabilityRepository
    {
        List<Availability> All();

        List<Availability> All(Employee employee);

        void Save(Employee employee, List<Availability> availabilities);
    }
}