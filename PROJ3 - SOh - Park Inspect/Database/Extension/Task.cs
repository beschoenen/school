using System.Linq;

namespace Database
{
    public partial class Task
    {
        public Task GetCleanModel()
        {
            return new Task
            {
                ID = ID,
                DateTimeStart = DateTimeStart,
                DateTimeEnd = DateTimeEnd,
                DatetimeCreated = DatetimeCreated,
                Remarks = Remarks,
                Hash = Hash
            };
        }

        public int CompletedInspections => Inspections.Count(x => x.DateTimeDone != null);

        public int NumberOfInspectors => Inspections.SelectMany(x => x.InspectionInspectors).Select(i => i.Employee).Count();
    }
}
