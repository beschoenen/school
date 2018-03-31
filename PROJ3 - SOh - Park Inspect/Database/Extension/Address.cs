namespace Database
{
    public partial class Address
    {
        public Address GetCleanModel()
        {
            return new Address
            {
                ID = ID,
                Street = Street,
                Number = Number,
                ZipCode = ZipCode,
                City = City,
                Country = Country,
                Province = Province,
                Remarks = Remarks,
                Hash = Hash
            };
        }
    }
}
