using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Linq;
using Database;
using SOh_ParkInspect.Repository.Interface;

namespace SOh_ParkInspect.Repository
{
    public class InspectionRepository : IInspectionRepository
    {
        private readonly Context _context;

        public InspectionRepository(Context context)
        {
            _context = context;
        }

        public List<Inspection> All()
        {
            return _context.Inspections.ToList();
        }

        public Inspection Find(int id)
        {
            return _context.Inspections.FirstOrDefault(i => i.ID == id);
        }

        public List<Inspection> FromUser(int id)
        {
            return _context.Inspections.Where(x => x.InspectionInspectors.Select(e => e.Employee.ID).Contains(id)).ToList();
        }

        public bool Add(Inspection inspection)
        {
            _context.Inspections.Add(inspection);
            _context.SaveChanges();

            return true;
        }

        public bool Add(IEnumerable<Inspection> inspections)
        {
            _context.Inspections.AddRange(inspections);
            _context.SaveChanges();

            return true;
        }

        public bool Update(Inspection inspection)
        {
            _context.Inspections.AddOrUpdate(inspection);
            _context.SaveChanges();

            return true;
        }

        public bool Remove(Inspection inspection)
        {
            _context.Inspections.Remove(inspection);
            _context.SaveChanges();

            return true;
        }
    }
}