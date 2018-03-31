using System.Collections.Generic;
using Database;

namespace SOh_ParkInspect.Repository.Interface
{
    public interface ITaskRepository
    {
        List<Task> All();

        bool Add(Task task);

        bool Delete(Task task);
    }
}