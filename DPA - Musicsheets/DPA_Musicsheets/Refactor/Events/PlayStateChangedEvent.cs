using DPA_Musicsheets.Refactor.EventHandler.Events;

namespace DPA_Musicsheets.Refactor.Events
{
    public class PlayStateChangedEvent : PayloadEvent<Enums.PlayState>
    {
        public PlayStateChangedEvent(Enums.PlayState payload) : base(payload)
        {
        }
    }
}
