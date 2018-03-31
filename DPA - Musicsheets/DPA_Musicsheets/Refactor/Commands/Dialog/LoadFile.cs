using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.Refactor.Models.Block;
using DPA_Musicsheets.Refactor.MusicLoaders;
using DPA_Musicsheets.Refactor.EventHandler;
using DPA_Musicsheets.Refactor.Events;
using DPA_Musicsheets.Refactor.MusicLoaders.Lilypond;

namespace DPA_Musicsheets.Refactor.Commands.Dialog
{
    public class LoadFile : AbstractDialogCommand
    {
        public override void execute()
        {
            if (Loader == null) return;

            if (Loader is LilypondLoader)
            {
                LilypondLoader lilypondLoader = (LilypondLoader)Loader;
                lilypondLoader.IsFixingBars = true;
                Piece = lilypondLoader.Load();
            } else
            {
                Piece = Loader.Load();
            }

            EventBus.Fire(new PieceLoadedEvent(Piece));
        }
    }
}
