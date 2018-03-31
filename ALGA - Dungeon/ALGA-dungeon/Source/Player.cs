using System;
using ALGAdungeon.Source.Helpers;
using ALGAdungeon.Source.Types;

namespace ALGAdungeon.Source
{
    public class Player
    {
        public Room Position { get; private set; }

        public int Health { get; set; }

        public Player(Room room)
        {
            Health = 100;

            Position = room;
            Position.IsExplored = true;
        }

        public string Move(DirectionType direction)
        {
            switch (direction)
            {
                case DirectionType.North:
                    Position = Position.Top.Walk(direction);
                    break;
                case DirectionType.East:
                    Position = Position.Right.Walk(direction);
                    break;
                case DirectionType.South:
                    Position = Position.Bottom.Walk(direction);
                    break;
                case DirectionType.West:
                    Position = Position.Left.Walk(direction);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }

            return $"You walked to the {direction}.";
        }

        public void MoveTo(Room position)
        {
            Position = position;
        }

        public bool OnRoom(Room room)
        {
            return Equals(Position.Coordinate, room.Coordinate);
        }

        public string Attack()
        {
            var attacks = 0;
            var playerAttacks = 0;
            var enemyAttacks = 0;

            while (Position.Enemy.Health > 1 && Health > 1)
            {
                attacks++;

                var attackPower = 6 * new BetterRandom().Next(1, 10);
                playerAttacks += attackPower;

                var damage = Position.Enemy.Attack(attackPower);
                enemyAttacks += damage;
                Health -= damage;
            }

            if (Position.Enemy.Health < 1)
            {
                Position.Enemy = null;
            }

            return $"You attacked the enemy {attacks} time(s) " +
                   $"and did {playerAttacks} damage.\n" +
                   $"The enemy did {enemyAttacks} damage to you.";
        }
    }
}