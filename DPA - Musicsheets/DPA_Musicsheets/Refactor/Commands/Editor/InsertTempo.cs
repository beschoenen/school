using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.Refactor.Models.Block;

namespace DPA_Musicsheets.Refactor.Commands.Editor
{
    public class InsertTempo : AbstractEditorCommand
    {
        private bool _isNewBlock;

        public InsertTempo(bool isNewBlock)
        {
            _isNewBlock = isNewBlock;
        }

        public override void execute()
        {
            if (_isNewBlock)
            {
                insert("\n{\n  \\tempo 4=120\n\n}\n{");
            }
            else
            {
                insert("\n  \\tempo 4=120\n");
            }
        }
    }
}
