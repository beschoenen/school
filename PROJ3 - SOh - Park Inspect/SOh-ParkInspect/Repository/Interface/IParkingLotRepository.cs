using System.Collections.Generic;
using Database;

namespace SOh_ParkInspect.Repository.Interface
{
    public interface IParkingLotRepository
    {
        List<ParkingLot> All();

        bool Add(ParkingLot parkingLot);
    }
}