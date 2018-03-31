using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Web;

namespace WCF.Action
{
    public class SleepAction : BaseAction
    {
        public SleepAction()
        {
            Duration = 15;
            Name = "Sleep";
        }

        protected override void Action()
        {
            Tamagotchi.Sleep -= 25;
            Tamagotchi.Health += 10;
        }
    }
}