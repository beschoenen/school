using DPA_Musicsheets.Refactor.Models.Block;

namespace DPA_Musicsheets.Refactor.MusicConverters
{
    public abstract class AbstractMusicConverter<T> where T : class
    {
        public abstract T Convert(Piece piece);
    }
}
