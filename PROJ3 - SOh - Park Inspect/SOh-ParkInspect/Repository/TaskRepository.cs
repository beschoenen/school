using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Database;
using SOh_ParkInspect.Repository.Interface;

namespace SOh_ParkInspect.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private readonly Context _context;

        public TaskRepository(Context context)
        {
            _context = context;
        }

        public List<Task> All()
        {
            return _context.Tasks.ToList();
        }

        public bool Add(Task task)
        {
            _context.Tasks.Add(task);
            _context.SaveChanges();

            return true;
        }

        public bool Delete(Task task)
        {
            Task t = _context.Tasks.Find(task.ID);

            _context.Tasks.Remove(t);
            _context.SaveChanges();
            return true;
        }
    }
}