using System.Collections.Generic;
using Database;

namespace SOh_ParkInspect.Repository.Interface
{
    public interface IChecklistRepository
    {
        List<Checklist> All();

        Checklist Find(int id);

        Checklist Find(string name);

        bool Add(Checklist checklist);
    }
}