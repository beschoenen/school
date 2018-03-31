using DPA_Musicsheets.Refactor.Models.Base;

namespace DPA_Musicsheets.Refactor.Models
{
    public class Note : MusicElement
    {
        public Note()
        {
            AlterType = Enums.AlterType.Normal;
        }
    }
}