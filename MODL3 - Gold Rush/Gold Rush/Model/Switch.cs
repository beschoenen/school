using System;
using Gold_Rush.Enum;

namespace Gold_Rush.Model
{
    public class Switch : Track
    {
        public SwitchState SwitchState { get; set; }

        public SwitchType SwitchType { get; set; }

        public Track Top { get; set; }

        public Track Bottom { get; set; }

        public Track Tail { get; set; }

        public override Track Next
        {
            get
            {
                if (SwitchType == SwitchType.Merge) return Tail;

                return SwitchState == SwitchState.Top ? Top : Bottom;
            }
        }

        public int Id { get; private set; }

        public Switch(int id, SwitchType type)
        {
            Id = id;
            SwitchState = SwitchState.Top;
            SwitchType = type;
        }

        private bool CanBeSwitched()
        {
            return Cart == null;
        }

        public void ChangeState()
        {
            if (!CanBeSwitched()) return;

            if (SwitchState == SwitchState.Bottom)
            {
                SwitchState = SwitchState.Top;
                Next = Top;
            }
            else
            {
                SwitchState = SwitchState.Bottom;
                Next = Bottom;
            }
        }
    }
}