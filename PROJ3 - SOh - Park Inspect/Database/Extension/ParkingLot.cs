namespace Database
{
    public partial class ParkingLot
    {
        public ParkingLot GetCleanModel()
        {
            return new ParkingLot
            {
                ID = ID,
                GeoLatitude = GeoLatitude,
                GeoLongitude = GeoLongitude,
                NumCameras = NumCameras,
                NumParkingSpaces = NumParkingSpaces,
                DateOpened = DateOpened,
                DateClosed = DateClosed,
                HasBarrier = HasBarrier,
                HasFence = HasFence,
                ParkingFeeHourly = ParkingFeeHourly,
                ParkingFeeMax = ParkingFeeMax,
                Hash = Hash
            };
        }
    }
}
