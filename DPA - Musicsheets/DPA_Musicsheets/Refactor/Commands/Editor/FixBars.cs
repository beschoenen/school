using DPA_Musicsheets.Refactor.EventHandler;
using DPA_Musicsheets.Refactor.Events;
using DPA_Musicsheets.Refactor.MusicConverters.Lilypond;
using DPA_Musicsheets.Refactor.MusicLoaders.Lilypond;
using System;
using System.Text.RegularExpressions;

namespace DPA_Musicsheets.Refactor.Commands.Editor
{
    public class FixBars : AbstractEditorCommand
    {
        private bool _fixAllBars;

        public FixBars(bool fixAllBars)
        {
            _fixAllBars = fixAllBars;
        }

        public override void execute()
        {
            if (_fixAllBars)
            {
                try
                {
                    // Fix bars
                    LilypondLoader lilypondLoader = new LilypondLoader();
                    lilypondLoader.IsFixingBars = true;
                    Piece = lilypondLoader.LoadLilypond(LilypondText);

                    // Piece changed
                    EventBus.Fire(new PieceChangedEvent(Piece));
                }
                catch (Exception) { }
            }
            else
            {
                // Lilypond text of parts
                string begin = LilypondText.Substring(0, SelectionStart);
                string select = LilypondText.Substring(SelectionStart, SelectionLength);
                string end = LilypondText.Substring(SelectionStart + SelectionLength);

                // Regex
                string regexWithoutBar = "[a-gr][,']*[\\d][\\d]*[.]*[~]*";

                // Matches
                int totalNotesBeginning = Regex.Matches(begin, regexWithoutBar).Count;
                int totalNotesSelection = Regex.Matches(select, regexWithoutBar).Count;
                int totalNotesEnding = Regex.Matches(end, regexWithoutBar).Count;

                // Fix all bars
                LilypondLoader lilypondLoader = new LilypondLoader();
                lilypondLoader.IsFixingBars = true;
                LilypondConverter lilypondConverter = new LilypondConverter();

                try
                {
                    string lilypondTextWithoutBars = LilypondText.Replace("|", " ").Replace("\n", "\n ").Replace("\n  ", "\n ");
                    Piece = lilypondLoader.LoadLilypond(lilypondTextWithoutBars);
                    string newLilypondText = lilypondConverter.Convert(Piece);

                    // Matches renewed text
                    MatchCollection matchesWithoutBar = Regex.Matches(newLilypondText, regexWithoutBar);
                    int firstIndexSelection = matchesWithoutBar[totalNotesBeginning].Index;
                    int firstIndexEnding = matchesWithoutBar[totalNotesBeginning + totalNotesSelection].Index;

                    // Selection renewed text 
                    int selectionLength = firstIndexEnding - firstIndexSelection;
                    string newSelectionText = newLilypondText.Substring(firstIndexSelection, selectionLength);

                    // Combine old and new lilypond text
                    LilypondText = begin + newSelectionText + end;
                    lilypondLoader = new LilypondLoader();
                    Piece = lilypondLoader.LoadLilypond(LilypondText);

                    // Piece changed
                    EventBus.Fire(new PieceChangedEvent(Piece));
                }
                catch (Exception) { }
            }
        }
    }
}
