namespace ALGAdungeon.Source.Helpers
{
    public class TreeConnection
    {
        private readonly Room _r1;
        private readonly Room _r2;

        public TreeConnection(Room r1, Room r2)
        {
            _r1 = r1;
            _r2 = r2;
        }

        public Room GetRoom1()
        {
            return _r1;
        }

        public Room GetRoom2()
        {
            return _r2;
        }
    }
}
