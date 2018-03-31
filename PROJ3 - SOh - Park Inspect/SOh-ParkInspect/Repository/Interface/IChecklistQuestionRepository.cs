using System.Collections.Generic;
using Database;

namespace SOh_ParkInspect.Repository.Interface
{
    public interface IChecklistQuestionRepository
    {
        List<ChecklistQuestion> All();

        List<ChecklistQuestion> GetChecklistQuestions(Checklist checklist);

        bool AddOrUpdate(ChecklistQuestion checklistQuestion);

        bool Remove(ChecklistQuestion checklistQuestion);
        bool RemoveRange(List<ChecklistQuestion> checklistQuestions);
    }
}