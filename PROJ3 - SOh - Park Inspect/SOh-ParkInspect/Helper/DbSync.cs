using System;
using System.Data.Entity.Migrations;
using System.Linq;
using Database;
using SOh_ParkInspect.Model;

namespace SOh_ParkInspect.Helper
{
    public static class DbSync
    {
        private static OnlineDatabase _onlineDatabase;
        private static OnlineDatabase OnlineDatabase => _onlineDatabase ?? (_onlineDatabase = new OnlineDatabase());

        private static OfflineDatabase _offlineDatabase;
        private static OfflineDatabase OfflineDatabase => _offlineDatabase ?? (_offlineDatabase = new OfflineDatabase());

        public static bool CanUpload => Settings.IsOnlineMode && CanConnectToLocalDb && OfflineDatabase.Answers.Any();
        public static bool HasInspectionLocal => CanConnectToLocalDb && OfflineDatabase.Inspections.Any();

        private static bool? _canConnectoLocalDb;
        public static bool CanConnectToLocalDb
        {
            get
            {
                if(_canConnectoLocalDb != null) return _canConnectoLocalDb.Value;

                try
                {
                    OfflineDatabase.Database.CommandTimeout = 1;
                    OfflineDatabase.Database.Connection.Open();
                    _canConnectoLocalDb = true;
                }
                catch (Exception)
                {
                    _canConnectoLocalDb = false;
                }

                return _canConnectoLocalDb.Value;
            }
        }

        /////////////
        // Sync Tasks

        public static bool Download(int inspectionId)
        {
            if (Settings.IsOfflineMode || !CanConnectToLocalDb) return false;

            OfflineDatabase.Clear();

            var inspection = OnlineDatabase.Inspections.AsNoTracking().First(i => i.ID == inspectionId);

            DownloadBaseTables();

            Download(inspection);

            return false;
        }

        public static bool Upload()
        {
            if (Settings.IsOfflineMode || !CanConnectToLocalDb) return false;

            OfflineDatabase.Inspections.ToList().ForEach(UploadTables);

            OfflineDatabase.Clear();

            return true;
        }

        /////////////////
        // Helper methods

        public static bool HasDownloadedInspection(Inspection inspection)
        {
            return CanConnectToLocalDb && OfflineDatabase.Inspections.Any(x => x.Hash == inspection.Hash);
        }

        private static void UploadTables(Inspection inspection)
        {
            var onlineInspection = OnlineDatabase.Inspections.FirstOrDefault(x => x.Hash == inspection.Hash);

            if (onlineInspection == null) return;

            onlineInspection.DateTimeDone = inspection.DateTimeDone;
            onlineInspection.DateTimeStarted = inspection.DateTimeStarted;
            
            inspection.Answers.ToList().ForEach(x =>
            {
                var answer = x.GetCleanModel();
                answer.Employee = OnlineDatabase.Employees.FirstOrDefault(a => a.Hash == x.Employee.Hash);
                answer.Inspection = onlineInspection;
                answer.Question = OnlineDatabase.Questions.FirstOrDefault(a => a.Hash == x.Question.Hash);

                if (x.Photo != null)
                {
                    var photo = x.Photo.GetCleanModel();
                    photo.Inspection = onlineInspection;
                    photo.Answer = answer;
                }

                OnlineDatabase.Answers.Add(answer);
            });

            OnlineDatabase.Inspections.AddOrUpdate(onlineInspection);

            OnlineDatabase.SaveChanges();
        }

        private static void DownloadBaseTables()
        {
            // AnswerSetValues
            OnlineDatabase.AnswerSetValues.Select(x => x.Value).Distinct().ToList().ForEach(x => { OfflineDatabase.AnswerSetValues.Add(new AnswerSetValue { Value = x }); });

            // JobTitles
            OfflineDatabase.JobTitles.AddRange(OnlineDatabase.JobTitles.AsNoTracking());

            // QuestionTypes
            OfflineDatabase.QuestionTypes.AddRange(OnlineDatabase.QuestionTypes.AsNoTracking());

            // Adresses
            OnlineDatabase.Addresses.AsNoTracking().ToList().ForEach(x => OfflineDatabase.Addresses.Add(x.GetCleanModel()));

            OfflineDatabase.SaveChanges();

            // Employees
            OnlineDatabase.Employees.AsNoTracking().ToList().ForEach(x =>
            {
                var employee = x.GetCleanModel();
                employee.Address = OfflineDatabase.Addresses.FirstOrDefault(a => a.Hash == x.Address.Hash);
                employee.JobTitle = OfflineDatabase.JobTitles.FirstOrDefault(j => j.Name == x.JobTitle.Name);

                OfflineDatabase.Employees.AddOrUpdate(employee);
            });

            // Customer
            OnlineDatabase.Customers.AsNoTracking().ToList().ForEach(x =>
            {
                var customer = x.GetCleanModel();
                customer.Address = OfflineDatabase.Addresses.FirstOrDefault(a => a.Hash == x.Address.Hash);

                OfflineDatabase.Customers.Add(customer);
            });

            OfflineDatabase.SaveChanges();
        }

        private static void Download(Inspection inspection)
        {
            var newInspection = inspection.GetCleanModel();
            var inspectionTask = inspection.Task;

            // Checklist
            var checklist = new Checklist
            {
                ID = inspectionTask.Checklist.ID,
                Name = inspectionTask.Checklist.Name,
                DateTimeCreated = inspectionTask.Checklist.DateTimeCreated,
                Remarks = inspectionTask.Checklist.Remarks,
                Hash = inspectionTask.Checklist.Hash
            };

            checklist.Inspections.Add(newInspection);

            OfflineDatabase.Checklists.Add(checklist);

            // ParkingLot
            var parkingLot = inspectionTask.ParkingLot.GetCleanModel();
            parkingLot.Address = OfflineDatabase.Addresses.FirstOrDefault(x => x.Hash == inspectionTask.ParkingLot.Address.Hash);

            OfflineDatabase.ParkingLots.Add(parkingLot);

            // ChecklistQuestions
            inspection.Checklist.ChecklistQuestions.ToList().ForEach(x =>
            {
                var question = x.Question.GetCleanModel();
                question.QuestionType = OfflineDatabase.QuestionTypes.FirstOrDefault(a => a.Name == x.Question.QuestionType.Name);

                // AnswerSetValues
                x.Question.AnswerSetValues.ToList().ForEach(b =>
                {
                    var asv = OfflineDatabase.AnswerSetValues.FirstOrDefault(k => k.Value == b.Value) ?? new AnswerSetValue { Value = b.Value };

                    question.AnswerSetValues.Add(asv);
                });

                checklist.ChecklistQuestions.Add(new ChecklistQuestion { Question = question });
            });

            // InspectionInspector
            inspection.InspectionInspectors.ToList().ForEach(x =>
            {
                newInspection.InspectionInspectors.Add(new InspectionInspector
                {
                    Employee = OfflineDatabase.Employees.FirstOrDefault(b => b.Hash == x.Employee.Hash),
                    Remarks = x.Remarks,
                    LastUpdated = x.LastUpdated
                });
            });

            // Task
            var task = inspectionTask.GetCleanModel();
            task.ParkingLot = parkingLot;
            task.Checklist = checklist;
            task.Inspections.Add(newInspection);
            task.Customer = OfflineDatabase.Customers.FirstOrDefault(x => x.Hash == inspectionTask.Customer.Hash);

            OfflineDatabase.Tasks.Add(task);
            OfflineDatabase.SaveChanges();
        }
    }
}