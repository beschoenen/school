using System.Collections.Generic;
using Database;

namespace SOh_ParkInspect.Repository.Interface
{
    public interface IAnswerSetValueRepository
    {
        List<AnswerSetValue> All();
    }
}