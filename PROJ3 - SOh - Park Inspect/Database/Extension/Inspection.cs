namespace Database
{
    public partial class Inspection
    {
        public Inspection GetCleanModel()
        {
            return new Inspection
            {
                ID = ID,
                DateTimePlanned = DateTimePlanned,
                DateTimeDone = DateTimeDone,
                DateTimeStarted = DateTimeStarted,
                AverageTravelTime = AverageTravelTime,
                Remarks = Remarks,
                Hash = Hash
            };
        }
    }
}
