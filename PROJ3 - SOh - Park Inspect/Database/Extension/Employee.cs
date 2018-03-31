namespace Database
{
    public partial class Employee
    {
        public Employee GetCleanModel()
        {
            return new Employee
            {
                ID = ID,
                Email = Email,
                Password = Password,
                FirstName = FirstName,
                LastNamePrefix = LastNamePrefix,
                LastName = LastName,
                TelephoneNumber = TelephoneNumber,
                DateOfBirth = DateOfBirth,
                DateOfEmployment = DateOfEmployment,
                DateOfResignation = DateOfResignation,
                Remarks = Remarks,
                Hash = Hash
            };
        }
    }
}
