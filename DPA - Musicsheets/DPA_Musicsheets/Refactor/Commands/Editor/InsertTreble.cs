using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.Refactor.Models.Block;

namespace DPA_Musicsheets.Refactor.Commands.Editor
{
    public class InsertTreble : AbstractEditorCommand
    {
        private bool _isNewBlock;
        private string _clefType;

        public InsertTreble(bool isNewBlock, string clefType)
        {
            _isNewBlock = isNewBlock;
            _clefType = clefType;
        }

        public override void execute()
        {
            string otherClef = "treble";
            if (_clefType.Equals("treble"))
            {
                otherClef = "bass";
            }

            if (_isNewBlock)
            {
                insert("\n{\n  \\clef " + _clefType + "\n\n}\n{\n \\clef " + otherClef + "\n");
            }
            else
            {
                insert("\n  \\clef " + _clefType + "\n");
            }
        }
    }
}