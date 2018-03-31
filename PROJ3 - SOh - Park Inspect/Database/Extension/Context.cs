using System.Data.Entity;

namespace Database
{
    public abstract class Context : DbContext
    {
        protected Context(string connectionString) : base(connectionString)
        {
            //
        }

        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<AnswerSetValue> AnswerSetValues { get; set; }
        public virtual DbSet<Availability> Availabilities { get; set; }
        public virtual DbSet<Checklist> Checklists { get; set; }
        public virtual DbSet<ChecklistQuestion> ChecklistQuestions { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Inspection> Inspections { get; set; }
        public virtual DbSet<InspectionInspector> InspectionInspectors { get; set; }
        public virtual DbSet<JobTitle> JobTitles { get; set; }
        public virtual DbSet<ParkingLot> ParkingLots { get; set; }
        public virtual DbSet<Photo> Photos { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<QuestionType> QuestionTypes { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<WeeklySchedule> WeeklySchedules { get; set; }
        public virtual DbSet<IntervalFrequency> IntervalFrequencies { get; set; }
        public virtual DbSet<TaskSchedule> TaskSchedules { get; set; }

        public void Clear()
        {
            Database.ExecuteSqlCommand("EXEC sp_msforeachtable \"ALTER TABLE ? NOCHECK CONSTRAINT all\"");
            Database.ExecuteSqlCommand("EXEC sp_MSForEachTable \"DELETE FROM ?\"");
            Database.ExecuteSqlCommand("EXEC sp_msforeachtable \"ALTER TABLE ? WITH CHECK CHECK CONSTRAINT all\"");
        }
    }
}