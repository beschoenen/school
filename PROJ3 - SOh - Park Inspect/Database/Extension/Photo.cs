namespace Database
{
    public partial class Photo
    {
        public Photo GetCleanModel()
        {
            return new Photo
            {
                Inspection = Inspection,
                Answer = Answer,
                Filename = Filename,
                Image = Image,
                Remarks = Remarks
            };
        }
    }
}
