namespace ALGA
{
    public class Number : Expression
    {
        private readonly int _number;

        public Number(int number)
        {
           _number = number;
        }

        public override string ToString()
        {
            return _number.ToString();
        }

        public override int evaluate()
        {
            return _number;
        }
    }
}
