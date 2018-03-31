using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database;

namespace SOh_ParkInspect.Repository
{
    public interface IAddressRepository
    {
        List<Address> GetAllAddresses();
    }
}
