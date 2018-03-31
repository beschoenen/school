using DPA_Musicsheets.Refactor.EventHandler.Events;
using DPA_Musicsheets.Refactor.Models.Block;

namespace DPA_Musicsheets.Refactor.Events
{
    public class PieceChangedEvent : PayloadEvent<Piece>
    {
        public PieceChangedEvent(Piece payload) : base(payload)
        {
        }
    }
}
