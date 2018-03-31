using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.Refactor.Models.Block;

namespace DPA_Musicsheets.Refactor.Commands.Editor
{
    public class InsertTime : AbstractEditorCommand
    {
        private int _top;
        private int _bottom;

        public InsertTime(int top, int bottom)
        {
            _top = top;
            _bottom = bottom;
        }

        public override void execute()
        {
            insert($"\\time {_top}/{_bottom}\n");
        }
    }
}
