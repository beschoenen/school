using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WCF.Action
{
    public class HugAction : BaseAction
    {
        public HugAction()
        {
            Duration = 3;
            Name = "Hug";
        }

        protected override void Action()
        {
            Tamagotchi.Hunger -= 10;
            Tamagotchi.Sleep -= 10;
            Tamagotchi.Boredom -= 10;
            Tamagotchi.Health += 10;
        }
    }
}