using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database;
using SOh_ParkInspect.Repository.Interface;

namespace SOh_ParkInspect.Repository.Dummy
{
    class DummyChecklistQuestionRepository : IChecklistQuestionRepository
    {
        private readonly List<ChecklistQuestion> _checklistQuestions;

        public DummyChecklistQuestionRepository()
        {
            _checklistQuestions = new List<ChecklistQuestion>();
        }

        public List<ChecklistQuestion> All()
        {
            return _checklistQuestions;
        }

        public List<ChecklistQuestion> GetChecklistQuestions(Checklist checklist)
        {
            if (checklist == null) return new List<ChecklistQuestion>();
            return _checklistQuestions.FindAll(c => c.ChecklistID == checklist.ID);
        }

        public bool AddOrUpdate(ChecklistQuestion checklistQuestion)
        {
            if (checklistQuestion == null) return false;
            ChecklistQuestion cq = _checklistQuestions.Find((c => c.ChecklistID == checklistQuestion.ChecklistID && c.QuestionID == checklistQuestion.QuestionID));
            if (cq == null)
            {
                _checklistQuestions.Add(checklistQuestion);
            }
            else
            {
                cq.Order = checklistQuestion.Order;
            }
            return true;
        }

        public bool Remove(ChecklistQuestion checklistQuestion)
        {
            if (checklistQuestion == null) return false;
            _checklistQuestions.Remove(checklistQuestion);
            return true;
        }

        public bool RemoveRange(List<ChecklistQuestion> checklistQuestions)
        {
            if (checklistQuestions == null || checklistQuestions.Count == 0) return false;
            checklistQuestions.ForEach(c => _checklistQuestions.Remove(c));
            return true;
        }
    }
}