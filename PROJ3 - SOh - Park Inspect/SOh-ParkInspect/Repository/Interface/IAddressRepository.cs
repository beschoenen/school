using System.Collections.Generic;
using Database;

namespace SOh_ParkInspect.Repository.Interface
{
    public interface IAddressRepository
    {
        List<Address> All();
    }
}