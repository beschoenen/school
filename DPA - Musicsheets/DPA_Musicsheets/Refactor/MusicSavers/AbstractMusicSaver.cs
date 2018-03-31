using DPA_Musicsheets.Refactor.Models.Block;

namespace DPA_Musicsheets.Refactor.MusicSavers
{
    public abstract class AbstractMusicSaver
    {
        public string Filter => $"{FilterName}|*{Extension}";
        public string FilterName { get; set; }
        public string Extension { get; set; }
        public string FilePath { get; set; }

        public abstract string Save(Piece piece);
    }
}
