using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database;
using SOh_ParkInspect.Repository.Interface;

namespace SOh_ParkInspect.Repository
{
    class ChecklistQuestionRepository : IChecklistQuestionRepository
    {
        private readonly Context _context;

        public ChecklistQuestionRepository(Context context)
        {
            _context = context;
        }

        public List<ChecklistQuestion> All()
        {
            return _context.ChecklistQuestions.ToList();
        }

        public List<ChecklistQuestion> GetChecklistQuestions(Checklist checklist)
        {
            if (checklist == null) return new List<ChecklistQuestion>();
            return _context.ChecklistQuestions.Where(c => c.ChecklistID == checklist.ID).ToList();
        }

        public bool AddOrUpdate(ChecklistQuestion checklistQuestion)
        {
            if (checklistQuestion == null) return false;
            _context.ChecklistQuestions.AddOrUpdate(checklistQuestion);
            _context.SaveChanges();
            return true;
        }

        public bool Remove(ChecklistQuestion checklistQuestion)
        {
            if (checklistQuestion == null) return false;
            _context.ChecklistQuestions.Remove(checklistQuestion);
            _context.SaveChanges();
            return true;
        }

        public bool RemoveRange(List<ChecklistQuestion> checklistQuestions)
        {
            if (checklistQuestions == null || checklistQuestions.Count == 0) return false;
            _context.ChecklistQuestions.RemoveRange(checklistQuestions);
            _context.SaveChanges();
            return true;
        }
    }
}