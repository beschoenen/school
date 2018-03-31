using DPA_Musicsheets.Refactor.Models.Base;

namespace DPA_Musicsheets.Refactor.Models
{
    public class Rest : MusicElement
    {
        public Rest()
        {
            NoteType = Enums.NoteType.R;
        }
    }
}