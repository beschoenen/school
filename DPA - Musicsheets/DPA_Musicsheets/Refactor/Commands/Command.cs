using DPA_Musicsheets.Refactor.Models.Block;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Refactor.Commands
{
    public interface Command
    {
        Piece Piece { get; set; }
        string LilypondText { get; set; }

        void execute();
    }
}
