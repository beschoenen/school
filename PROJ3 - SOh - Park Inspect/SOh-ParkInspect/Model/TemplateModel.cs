using System;
using System.Collections.Generic;
using System.Linq;
using Database;

namespace SOh_ParkInspect.Model
{
    public class TemplateModel
    {
        //Insert data from DB in this model.
        //TODO: Check if this is correct

        public int Id { get; private set; }
        public string Name { get; private set; }
        public DateTime DateTimeCreated { get; private set; }
        public virtual List<ChecklistQuestion> ChecklistQuestions { get; private set; }
        public virtual Checklist PreviousChecklist { get; private set; }
        public virtual List<Task> Tasks { get; private set; }

        public TemplateModel(int id,
                             string name,
                             DateTime dateTimeCreated,
                             IEnumerable<ChecklistQuestion> checklistQuestions,
                             Checklist previousChecklist,
                             IEnumerable<Task> tasks)
        {
            Id = id;
            Name = name;
            DateTimeCreated = dateTimeCreated;
            ChecklistQuestions = checklistQuestions.ToList();
            PreviousChecklist = previousChecklist;
            Tasks = tasks.ToList();
        }
    }
}