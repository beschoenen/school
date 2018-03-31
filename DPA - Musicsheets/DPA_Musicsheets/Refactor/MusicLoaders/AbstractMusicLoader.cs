using DPA_Musicsheets.Refactor.Models.Block;

namespace DPA_Musicsheets.Refactor.MusicLoaders
{
    public abstract class AbstractMusicLoader
    {
        public string FilterName { get; set;  }
        public string Extension { get; set;  }
        public string Filter => $"{FilterName}|*{Extension}";
        public string FilePath { get; set; }

        public abstract Piece Load();
    }
}
