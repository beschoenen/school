namespace DPA_Musicsheets.Refactor
{
    public class Enums
    {
        public enum AlterType
        {
            Normal = 0,
            Sharp = 1,
            Flat = -1
        }

        public enum DurationType
        {
            None = 0,
            Whole = 1,
            Half = 2,
            Quarter = 4,
            Eighth = 8,
            Sixteenth = 16,
            ThirtySecondth = 32
        }

        public enum NoteType
        {
            C,
            D,
            E,
            F,
            G,
            A,
            B,
            R
        }

        public enum ClefType
        {
            G = 0,
            C = 1,
            F = 2
        }

        public enum TokenKind
        {
            Bar,
            Clef,
            Time,
            Tempo,
            Staff
        }

        public enum PlayState
        {
            Play,
            Pause,
            Stop
        }

        public enum Marker
        {
            RepeatStart,
            RepeatEnd,
            AlternativesStart,
            AlternativesEnd,
            AlternativeStart,
            MusicBlockStart,
            MusicBlockEnd,
            Tied
        }

        public enum ConversionState
        {
            Normal,
            Block
        }
    }
}