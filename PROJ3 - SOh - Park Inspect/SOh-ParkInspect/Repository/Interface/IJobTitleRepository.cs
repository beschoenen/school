using System.Collections.Generic;
using Database;

namespace SOh_ParkInspect.Repository.Interface
{
    public interface IJobTitleRepository
    {
        List<JobTitle> All();
    }
}