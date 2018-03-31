using DPA_Musicsheets.Refactor.EventHandler;
using DPA_Musicsheets.Refactor.Events;
using DPA_Musicsheets.Refactor.Models.Block;
using DPA_Musicsheets.Refactor.MusicConverters.Lilypond;
using DPA_Musicsheets.Refactor.MusicLoaders.Lilypond;

namespace DPA_Musicsheets.Refactor.Commands.Editor
{
    public abstract class AbstractEditorCommand : Command
    {
        public Piece Piece { get; set; }
        public string LilypondText { get; set; }
        public static int SelectionStart { get; set; }
        public static int SelectionLength { get; set; }
        public static int Offset { get; set; }

        public abstract void execute();

        public void insert(string text, int? position = null, bool convert = true)
        {
            var newText = LilypondText.Insert(position ?? SelectionStart, text);

            if (!convert)
            {
                LilypondText = newText;
                return;
            }

            try
            {
                Piece = new LilypondLoader().LoadLilypond(newText);
                LilypondText = new LilypondConverter().Convert(Piece);

                EventBus.Fire(new PieceChangedEvent(Piece));
            }
            catch
            {
                LilypondText = newText;
            }
        }
    }
}
