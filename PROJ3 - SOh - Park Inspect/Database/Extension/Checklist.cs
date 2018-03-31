namespace Database
{
    public partial class Checklist
    {
        public Checklist GetCleanModel()
        {
            return new Checklist
            {
                Name = Name,
                Remarks = Remarks,
                Parent = this,
                Hash = Hash
            };
        }

        public int Version
        {
            get
            {
                var version = 1;

                var checklist = Parent;
                while (true)
                {
                    if (checklist == null) break;

                    version++;
                    checklist = checklist.Parent;
                }

                return version;
            }
        }

        public int NumberOfQuestions => ChecklistQuestions.Count;
    }
}
