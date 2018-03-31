namespace DPA_Musicsheets.Refactor.Models
{
    public class TimeSignature
    {
        public int Top;
        public int Bottom;

        public TimeSignature(int top, int bottom)
        {
            Top = top;
            Bottom = bottom;
        }

        public override string ToString()
        {
            return $"{Top}/{Bottom}";
        }
    }
}
