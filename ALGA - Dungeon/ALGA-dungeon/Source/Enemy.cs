using ALGAdungeon.Source.Helpers;

namespace ALGAdungeon.Source
{
    public class Enemy
    {
        public int Level;
        public int Health;

        public Enemy()
        {
            Level = new BetterRandom().Next(1, 10);
            Health = Level * 3 + 50;
        }

        public int Attack(int power)
        {
            Health -= power;

            if (Health < 1) return 0;

            // Damage enemy did to the player
            return 2 * Level + new BetterRandom().Next(1, 10);
        }

        public override string ToString()
        {
            return $"a level {Level} enemy";
        }
    }
}