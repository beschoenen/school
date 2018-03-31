using System;

namespace WCF.Action
{
    public class PlayAction : BaseAction
    {
        public PlayAction()
        {
            Duration = 8;
            Name = "Play";
        }

        protected override void Action()
        {
            Tamagotchi.Boredom -= 35;
            if (new Random().Next(1, 100) >= 80)
            {
                Tamagotchi.Health -= 10;
            }
        }
    }
}