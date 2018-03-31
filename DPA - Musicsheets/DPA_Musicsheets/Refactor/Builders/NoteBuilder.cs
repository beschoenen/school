using DPA_Musicsheets.Refactor.Models;

namespace DPA_Musicsheets.Refactor.Builders
{
    public class NoteBuilder : AbstractBuilder<NoteBuilder, Note>
    {
        public NoteBuilder(Enums.NoteType note)
        {
            Element = new Note {NoteType = note};
        }

        public NoteBuilder Sharp()
        {
            Element.AlterType = Enums.AlterType.Sharp;

            return this;
        }

        public NoteBuilder Flat()
        {
            Element.AlterType = Enums.AlterType.Flat;

            return this;
        }

        public NoteBuilder IncreaseOctave()
        {
            Element.Octaves++;
            return this;
        }

        public NoteBuilder DecreaseOctave()
        {
            Element.Octaves--;
            return this;
        }

        public NoteBuilder ChangeOctave(int value)
        {
            Element.Octaves += value;
            return this;
        }

        public NoteBuilder IsTied(bool isTied = true)
        {
            Element.IsTied = isTied;
            return this;
        }
    }
}