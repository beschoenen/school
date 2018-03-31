using System;
using System.Collections.Generic;
using DPA_Musicsheets.Refactor.Models;
using DPA_Musicsheets.Refactor.Models.Base;
using DPA_Musicsheets.Refactor.Models.Block;

namespace DPA_Musicsheets.Refactor.MusicConverters.Lilypond
{
    public class LilypondConverter : AbstractMusicConverter<string>
    {
        private int _indent;

        private string _indentString => new string(' ', _indent);
        private string _indentInside => new string(' ', _indent + 2);

        private BlockElement _currentBlock;

        private Dictionary<Type, Func<Element, string>> Actions => new Dictionary<Type, Func<Element, string>>
        {
            {typeof(Alternative), HandleAlternative},
            {typeof(MusicBlock), HandleBlock},
            {typeof(Repeat), HandleRepeat},
            {typeof(Bar), HandleBar},
            {typeof(MusicElement), HandleMusicElement}
        };

        public override string Convert(Piece piece)
        {
            if (piece == null) return string.Empty;
            return HandlePiece(piece).Replace("\n\n", "\n").Trim('\n');
        }

        private string HandleBlockElement(BlockElement block)
        {
            _currentBlock = block;

            var lilypond = string.Empty;

            block.Elements.ForEach(element =>
            {
                foreach (var keyValuePair in Actions)
                {
                    if (element == null) continue;
                    if (!keyValuePair.Key.IsAssignableFrom(element.GetType())) continue;

                    lilypond += keyValuePair.Value(element);
                    break;
                }
            });

            return lilypond;
        }

        private string MakeBlock(BlockElement block)
        {
            var lilypond = string.Empty;

            if (_currentBlock != null && _currentBlock != block)
            {
                if (_currentBlock.Clef != block.Clef)
                {
                    var clef = block.Clef == Enums.ClefType.F ? "bass" : "treble";
                    lilypond += $"\\clef {clef}\n{_indentInside}";
                }
                if (!_currentBlock.TimeSignature.ToString().Equals(block.TimeSignature.ToString()))
                {
                    lilypond += $"\\time {block.TimeSignature}\n{_indentInside}";
                }
                if (!_currentBlock.Tempo.ToString().Equals(block.Tempo.ToString()))
                {
                    lilypond += $"\\tempo {block.Tempo.Beat}={block.Tempo.Count}\n{_indentInside}";
                }
            }

            _indent += 2;

            lilypond += HandleBlockElement(block);

            _indent -= 2;

            return lilypond.TrimEnd(' ').TrimEnd('\n');
        }

        private string HandlePiece(Piece piece)
        {
            var clef = piece.Clef == Enums.ClefType.F ? "bass" : "treble";

            return
                $"\\relative {piece.Staffs.ToString().ToLower()}' {{\n" +
                $"{_indentInside}\\clef {clef}\n" +
                $"{_indentInside}\\time {piece.TimeSignature}\n" +
                $"{_indentInside}\\tempo {piece.Tempo}\n" +
                $"{_indentInside}{MakeBlock(piece)}\n" +
                $"{_indentString}}}\n{_indentString}";
        }

        private string HandleAlternative(Element element)
        {
            if (!(element is Alternative alternative))
            {
                throw new InvalidOperationException();
            }

            return
                "\\alternative {\n" +
                $"{_indentInside}{MakeBlock(alternative)}\n" +
                $"{_indentString}}}\n{_indentString}";
        }

        private string HandleRepeat(Element element)
        {
            if (!(element is Repeat repeat))
            {
                throw new InvalidOperationException();
            }

            var volta = repeat.Volta ? "volta " : "";

            return
                $"\\repeat {volta}{repeat.Repeats} {{\n" +
                $"{_indentInside}{MakeBlock(repeat)}\n" +
                $"{_indentString}}}\n{_indentString}";
        }

        private string HandleBlock(Element element)
        {
            if (!(element is BlockElement block))
            {
                throw new InvalidOperationException();
            }

            return
                "{\n" +
                $"{_indentInside}{MakeBlock(block)}\n" +
                $"{_indentString}}}\n{_indentString}";
        }

        private string HandleBar(Element element)
        {
            return $"|\n{_indentString}";
        }

        private string HandleMusicElement(Element element)
        {
            if (!(element is MusicElement musicElement))
            {
                throw new InvalidOperationException();
            }

            var note = musicElement.NoteType.ToString().ToLower();

            switch (musicElement.AlterType)
            {
                case Enums.AlterType.Sharp:
                    note += "is";
                    break;
                case Enums.AlterType.Flat:
                    if (!(note.Equals("a") || note.Equals("e")))
                    {
                        note += "e";
                    }
                    note += "s";
                    break;
            }

            note += new string(musicElement.Octaves < 0 ? ',' : '\'', Math.Abs(musicElement.Octaves));

            note += (int) musicElement.DurationType;

            if (musicElement.Dots > 0)
            {
                note += new string('.', musicElement.Dots);
            }

            if (musicElement.IsTied)
            {
                note += "~";
            }

            return $"{note} ";
        }
    }
}