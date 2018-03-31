using DPA_Musicsheets.Refactor.EventHandler;
using DPA_Musicsheets.Refactor.Events;
using DPA_Musicsheets.Refactor.Models.Block;
using DPA_Musicsheets.Refactor.MusicLoaders.Lilypond;
using DPA_Musicsheets.Refactor.MusicSavers.Lilypond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DPA_Musicsheets.Refactor.Commands.Dialog
{
    public class SaveFile : AbstractDialogCommand
    {
        private string _filter;

        public SaveFile(string filter)
        {
            _filter = filter;
        }

        public override void execute()
        {
            if (Piece == null) return;

            LilypondLoader lilypondLoader = new LilypondLoader();
            lilypondLoader.IsFixingBars = true;
            var piece = lilypondLoader.LoadLilypond(LilypondText);
            var message = FileHelper.Instance.WriteFile(piece, _filter);

            if (message != null)
            {
                MessageBox.Show(message);
            }

            EventBus.Fire(new PieceSavedEvent());
        }
    }
}
