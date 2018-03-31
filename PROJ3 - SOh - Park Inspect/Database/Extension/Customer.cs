namespace Database
{
    public partial class Customer
    {
        public Customer GetCleanModel()
        {
            return new Customer
            {
                ID = ID,
                Name = Name,
                Contact = Contact,
                PhoneNumber = PhoneNumber,
                Email = Email,
                Remarks = Remarks,
                Hash = Hash
            };
        }
    }
}
