using System;
using System.Collections.Generic;
using System.Linq;
using Database;
using SOh_ParkInspect.Repository.Interface;

namespace SOh_ParkInspect.Repository.Dummy
{
    public class DummyChecklistRepository : IChecklistRepository
    {
        private readonly List<Checklist> _checklists;

        public DummyChecklistRepository()
        {
            _checklists = new List<Checklist>
            {
                new Checklist
                {
                    DateTimeCreated = DateTime.Now.AddDays(-3),
                    Name = "Vragenlijst 3",
                    Parent = new Checklist
                    {
                        DateTimeCreated = DateTime.Now.AddDays(-4),
                        Name = "Vragenlijst 2",
                        Parent = new Checklist
                        {
                            DateTimeCreated = DateTime.Now.AddDays(-7),
                            Name = "Vragenlijst 1"
                        }
                    },
                    ChecklistQuestions = new List<ChecklistQuestion>
                    {
                        new ChecklistQuestion
                        {
                            Question = new Question
                            {
                                Text = "Hoe breed is een parkeervak?",
                                AnswerSuffix = " cm",
                                QuestionType = new QuestionType { Name = "Tekst" }
                            }
                        }
                    }
                }
            };
        }

        public List<Checklist> All()
        {
            return _checklists;
        }

        public bool Add(Checklist checklist)
        {
            _checklists.Add(checklist);

            return true;
        }

        public Checklist Find(int id)
        {
            return _checklists.FirstOrDefault(c => c.ID == id);
        }

        public Checklist Find(string name)
        {
            return _checklists.FirstOrDefault(c => c.Name == name);
        }
    }
}