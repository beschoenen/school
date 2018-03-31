using DPA_Musicsheets.Refactor.Models.Base;

namespace DPA_Musicsheets.Refactor.Models
{
    public class Token : Element
    {
        public Enums.TokenKind TokenKind { get; set; }
        public string Value { get; set; }

        public Token(Enums.TokenKind tokenKind, string value)
        {
            TokenKind = tokenKind;
            Value = value;
        }
    }
}