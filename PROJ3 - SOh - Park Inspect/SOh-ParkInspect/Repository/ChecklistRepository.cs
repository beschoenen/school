using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using Database;
using SOh_ParkInspect.Repository.Interface;

namespace SOh_ParkInspect.Repository
{
    public class ChecklistRepository : IChecklistRepository
    {
        private readonly Context _context;

        public ChecklistRepository(Context context)
        {
            _context = context;
        }

        public List<Checklist> All()
        {
            return _context.Checklists.ToList();
        }

        public Checklist Find(int id)
        {
            return _context.Checklists.FirstOrDefault(c => c.ID == id);
        }

        public Checklist Find(string name)
        {
            return _context.Checklists.FirstOrDefault(c => c.Name == name);
        }

        public bool Add(Checklist checklist)
        {
            try
            {
                _context.Checklists.AddOrUpdate(checklist);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }
    }
}