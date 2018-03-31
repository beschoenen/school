using System;

namespace WCF.Action
{
    public class FeedAction : BaseAction
    {
        public FeedAction()
        {
            Duration = 5;
            Name = "Feed";
        }

        protected override void Action()
        {
            Tamagotchi.Hunger -= 50;
            if (new Random().Next(1, 100) >= 90)
            {
                Tamagotchi.Health -= 20;
            }
        }
    }
}