using DPA_Musicsheets.Refactor.Models.Base;

namespace DPA_Musicsheets.Refactor.Models.Block
{
    public class Piece : BlockElement
    {
        public Piece(int division)
        {
            Division = division;
        }

        public int Division;
    }
}