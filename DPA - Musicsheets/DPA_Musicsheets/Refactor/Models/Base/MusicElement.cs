namespace DPA_Musicsheets.Refactor.Models.Base
{
    public abstract class MusicElement : Element
    {
        public Enums.NoteType NoteType;

        public Enums.DurationType DurationType;

        public Enums.AlterType AlterType;

        public int Octaves;

        public int Dots;

        public bool IsTied;
    }
}