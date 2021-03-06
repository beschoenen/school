﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Database
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Database : DbContext
    {
        public Database()
            : base("name=Database")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
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
        public virtual DbSet<TaskSchedule> TaskSchedules { get; set; }
        public virtual DbSet<IntervalFrequency> IntervalFrequencies { get; set; }
    }
}
