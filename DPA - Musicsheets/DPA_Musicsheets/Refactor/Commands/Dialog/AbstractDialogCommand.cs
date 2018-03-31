using DPA_Musicsheets.Refactor.Models.Block;
using DPA_Musicsheets.Refactor.MusicLoaders;

namespace DPA_Musicsheets.Refactor.Commands.Dialog
{
    public abstract class AbstractDialogCommand : Command
    {
        public Piece Piece { get; set; }
        public string LilypondText { get; set; }
        public static AbstractMusicLoader Loader { get; set; }

        public virtual void execute()
        {

        }
    }
}
