namespace DPA_Musicsheets.Refactor.Models
{
    public class Tempo
    {
        public int Beat { get; }
        public int Count { get; }

        public Tempo(int beat, int count)
        {
            Beat = beat;
            Count = count;
        }

        public override string ToString()
        {
            return $"{Beat}={Count}";
        }
    }
}
