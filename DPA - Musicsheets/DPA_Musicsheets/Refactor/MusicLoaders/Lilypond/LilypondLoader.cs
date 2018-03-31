using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using DPA_Musicsheets.Refactor.Builders;
using DPA_Musicsheets.Refactor.Models;
using DPA_Musicsheets.Refactor.Models.Base;
using DPA_Musicsheets.Refactor.Models.Block;
using static DPA_Musicsheets.Refactor.Enums;

namespace DPA_Musicsheets.Refactor.MusicLoaders.Lilypond
{
    public class LilypondLoader : AbstractMusicLoader
    {
        private readonly Stack<BlockElement> _elementStack;

        public Piece Piece { get; set; }
        public bool IsFixingBars { get; set; }

        public LilypondLoader()
        {
            FilterName = "Lilypond";
            Extension = ".ly";

            Piece = new Piece(24);
            _elementStack = new Stack<BlockElement>();
            _elementStack.Push(Piece);
        }

        public Dictionary<string, Action<string>> SpecialMethods => new Dictionary<string, Action<string>>
        {
            { "\\relative", HandleStaffs },
            { "\\clef", HandleClef },
            { "\\time", HandleTime },
            { "\\tempo", HandleTempo },
            { "\\repeat", HandleRepeat },
            { "\\alternative", HandleAlternative }
        };

        public Dictionary<string, Action<string>> Specials => new Dictionary<string, Action<string>>
        {
            { "{", HandleBlockOpen },
            { "}", HandleBlockClose },
            { "|", HandleBar }
        };

        public override Piece Load()
        {
            Piece = new Piece(24);
            _elementStack.Clear();
            _elementStack.Push(Piece);

            return LoadLilypond(File.ReadAllText(FilePath));
        }

        public Piece LoadLilypond(string lilypondText)
        {
            foreach (var row in lilypondText.Split('\n'))
            {
                var line = row.Trim();

                if (SpecialMethods.Any(e => line.StartsWith(e.Key)))
                {
                    var special = line.Split(' ')[0];
                    SpecialMethods.FirstOrDefault(m => m.Key == special).Value(line);
                    continue;
                }

                foreach (var token in line.Split(' '))
                {
                    var item = token.Trim();

                    if (Specials.Any(e => item == e.Key))
                    {
                        Specials.FirstOrDefault(m => m.Key == item).Value(line);
                        continue;
                    }

                    if (item == string.Empty) continue;

                    _elementStack.Peek().AddElement(CreateMusicElement(item), IsFixingBars);
                }
            }

            return Piece;
        }

        private MusicElement CreateMusicElement(string token)
        {
            NoteType noteType;
            Enum.TryParse(token.Substring(0, 1).ToUpper(), out noteType);

            var dots = token.Count(c => c == '.');
            var number = int.Parse(Regex.Match(token, @"\d+").Value);
            var durationType = (DurationType) number;

            if (new Regex(@"[a-g][,'eis]*[0-9]+[.]*").IsMatch(token))
            {
                var builder = new NoteBuilder(noteType);

                // Octave
                var increaseOctaves = token.Count(c => c == '\'');
                var decreaseOctaves = token.Count(c => c == ',') * -1;
                builder.ChangeOctave(increaseOctaves + decreaseOctaves);

                // Sharp or flat
                if (token.Contains("is"))
                {
                    builder = builder.Sharp();
                }
                else if (token.Contains("token"))
                {
                    builder = builder.Flat();
                }

                if (token.Contains("~"))
                {
                    builder = builder.IsTied();
                }

                builder.AddDots(dots).Duration(durationType);
                
                return builder.Build();
            }

            if (new Regex(@"r.*?[0-9][.]*").IsMatch(token))
            {
                var builder = new RestBuilder();

                builder.AddDots(dots).Duration(durationType);

                return builder.Build();
            }

            return null;
        }

        private void HandleStaffs(string line)
        {
            // TODO
            if (_elementStack.Count > 0)
            {
                _elementStack.Peek().Staffs = NoteType.C;
            }
        }

        private void HandleClef(string line)
        {
            var clef = line.Split(' ').Last();

            if (_elementStack.Count > 0)
            {
                _elementStack.Peek().Clef = clef == "bass" ? ClefType.F : ClefType.G;
            }
        }

        private void HandleTime(string line)
        {
            var time = line.Split(' ').Last().Split('/');

            _elementStack.Peek().TimeSignature = new TimeSignature(
                int.Parse(time[0]), 
                int.Parse(time[1])
            );
        }

        private void HandleTempo(string line)
        {
            var tempo = line.Split(' ').Last().Split('=');

            _elementStack.Peek().Tempo = new Tempo(
                int.Parse(tempo[0]), 
                int.Parse(tempo[1])
            );
        }

        private void HandleRepeat(string line)
        {
            var pieces = line.TrimEnd('{').Trim().Split(' ');
            var repeats = 2;

            try
            {
                int.TryParse(pieces.Last(), out repeats);
            }
            catch (Exception) { }

            var block = new Repeat
            {
                Volta = line.Contains("volta"),
                Repeats = repeats
            };

            AddBlock(block);
        }

        private void HandleAlternative(string line)
        {
            AddBlock(new Alternative());
        }

        private void HandleBlockOpen(string line)
        {
            AddBlock(new MusicBlock());
        }

        private void HandleBlockClose(string line)
        {
            _elementStack.Pop();
        }

        private void HandleBar(string line)
        {
            _elementStack.Peek().AddElement(new Bar(), IsFixingBars);
        }

        private void AddBlock(BlockElement block)
        {
            var currentBlock = _elementStack.Peek();

            block.Tempo = currentBlock.Tempo;
            block.TimeSignature = currentBlock.TimeSignature;

            currentBlock.AddElement(block, IsFixingBars);
            _elementStack.Push(block);
        }
    }
}