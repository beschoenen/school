using System.Collections.Generic;
using Database;

namespace SOh_ParkInspect.Repository.Interface
{
    public interface IInspectionRepository
    {
        List<Inspection> All();

        Inspection Find(int id);

        List<Inspection> FromUser(int id);

        bool Add(Inspection inspection);

        bool Add(IEnumerable<Inspection> inspections);

        bool Update(Inspection inspection);

        bool Remove(Inspection inspection);
    }
}