using System.Collections.Generic;
using System.Linq;
using Database;
using SOh_ParkInspect.Repository.Interface;

namespace SOh_ParkInspect.Repository
{
    public class ParkingLotRepository : IParkingLotRepository
    {
        private readonly Context _context;

        public ParkingLotRepository(Context context)
        {
            _context = context;
        }

        public List<ParkingLot> All()
        {
            return _context.ParkingLots.ToList();
        }

        public bool Add(ParkingLot parkingLot)
        {
            _context.ParkingLots.Add(parkingLot);
            _context.SaveChanges();

            return true;
        }
    }
}