using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.Refactor.Models.Block;
using DPA_Musicsheets.Refactor.MusicLoaders;

namespace DPA_Musicsheets.Refactor.Commands.Dialog
{
    public class OpenFile : AbstractDialogCommand
    {
        public override void execute()
        {
            Loader = FileHelper.Instance.ReadFile();
        }
    }
}
