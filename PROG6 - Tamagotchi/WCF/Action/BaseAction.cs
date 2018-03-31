using System;
using System.Timers;
using WCF.Service;

namespace WCF.Action
{
    public abstract class BaseAction
    {
        public Tamagotchi Tamagotchi { get; private set; }
        public Timer Timer { get; private set; }

        public int Duration { get; protected set; }
        public string Name { get; protected set; }
        public DateTime StartTime { get; private set; }

        public BaseAction Start(Tamagotchi tamagotchi, System.Action callback)
        {
            Tamagotchi = tamagotchi;

            Timer = new Timer(Duration * 1000) {Enabled = true};
            Timer.Elapsed += (sender, e) =>
            {
                Action();
                Cancel();
                callback();
            };
            
            Timer.Start();
            StartTime = DateTime.Now;

            return this;
        }

        public void Cancel()
        {
            Timer.Stop();
            Timer.Dispose();
        }

        protected abstract void Action();
    }
}
