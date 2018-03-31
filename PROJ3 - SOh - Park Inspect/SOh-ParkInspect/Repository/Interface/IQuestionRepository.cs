using System.Collections.Generic;
using Database;

namespace SOh_ParkInspect.Repository.Interface
{
    public interface IQuestionRepository
    {
        List<Question> All();

        Question Find(int id);
    }
}