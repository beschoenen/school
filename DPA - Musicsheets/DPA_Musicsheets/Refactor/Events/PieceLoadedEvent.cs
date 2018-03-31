using DPA_Musicsheets.Refactor.EventHandler.Events;
using DPA_Musicsheets.Refactor.Models.Block;

namespace DPA_Musicsheets.Refactor.Events
{
    public class PieceLoadedEvent : PayloadEvent<Piece>
    {
        public PieceLoadedEvent(Piece payload) : base(payload)
        {
        }
    }
}
