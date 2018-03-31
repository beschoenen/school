using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.Refactor.Models.Block;

namespace DPA_Musicsheets.Refactor.Commands.Editor
{
    public class InsertAlternative : AbstractEditorCommand
    {
        public override void execute()
        {
            insert("\\alternative {\n  { }\n  { }\n}");
        }
    }
}
