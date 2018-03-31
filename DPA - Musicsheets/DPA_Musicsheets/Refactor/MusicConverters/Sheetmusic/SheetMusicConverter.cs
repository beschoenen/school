using System;
using System.Collections.Generic;
using System.Linq;
using DPA_Musicsheets.Refactor.Models;
using DPA_Musicsheets.Refactor.Models.Base;
using DPA_Musicsheets.Refactor.Models.Block;
using PSAMControlLibrary;

namespace DPA_Musicsheets.Refactor.MusicConverters.Sheetmusic
{
    public class SheetMusicConverter : AbstractMusicConverter<List<MusicalSymbol>>
    {
        // Start situation
        private Piece _piece;

        private Element _element;
        private List<MusicalSymbol> _symbols;
        private int _previousOctave = 4;
        private Enums.NoteType _previousNote = Enums.NoteType.C;

        // Remember element to handle
        private bool _isTied;
        private Repeat _repeat;
        private bool _isCreatingRepeat;
        private int _repeats;
        private int _repeatGroup;
        private bool _isCreatingAlternative;
        private int _alternatives;
        private int _currentAlternative;

        // Alter octaves for alternatives
        private bool _mustAlterOctave;

        private int _alterOctave;
        private bool _remembedFirstOctaveAlternative;
        private int _firstOctaveAlternative;

        // Time
        private Models.TimeSignature _currentTimeSignature;

        // Clef
        private Enums.ClefType _currentClefType;

        private Dictionary<Type, Action> ElementActionDictionary => new Dictionary<Type, Action>
        {
            {typeof(Models.Note), HandleNote},
            {typeof(Models.Rest), HandleRest},
            {typeof(Bar), HandleBar},
            {typeof(MusicBlock), HandleMusicBlock},
            {typeof(Alternative), HandleAlternatives},
            {typeof(Repeat), HandleRepeat}
        };

        public override List<MusicalSymbol> Convert(Piece piece)
        {
            _piece = piece;
            _symbols = new List<MusicalSymbol>();
            _previousOctave = 4;
            _previousNote = Enums.NoteType.C;

            _currentClefType = _piece.Clef;
            _symbols.Add(new Clef((ClefType) _piece.Clef, (int) (_piece.Clef + 1) * 2));
            _currentTimeSignature = _piece.TimeSignature;
            _symbols.Add(new PSAMControlLibrary.TimeSignature(TimeSignatureType.Numbers, (uint) _piece.TimeSignature.Top, (uint) _piece.TimeSignature.Bottom));

            RunActions(piece.Elements);

            CheckEndingRepeat();

            return _symbols;
        }

        private void RunActions(List<Element> elements)
        {
            elements.ForEach(e =>
            {
                _element = e;

                if (_element is MusicBlock)
                {
                    MusicBlock block = (MusicBlock)_element;
                    HandleClef(block.Clef);
                }

                if (!ElementActionDictionary.TryGetValue(e.GetType(), out var action)) return;

                if (action != HandleAlternatives)
                {
                    CheckEndingRepeat();
                }

                action();
            });
        }

        private void CheckEndingRepeat()
        {
            if (_repeat == null || _isCreatingAlternative || _isCreatingRepeat) return;

            // Closing bar repeat
            var barLineEnd = new Barline { RepeatSign = RepeatSignType.Backward };
            _symbols.Add(barLineEnd);

            _repeat = null;
            _repeats = 0;
            _repeatGroup = 0;
        }

        private void HandleNote()
        {
            var note = (Models.Note) _element;

            // Octave
            if (_mustAlterOctave)
            {
                _previousOctave = _alterOctave;
                _mustAlterOctave = false;
            }
            else
            {
                CalculateOctave(note);
            }

            _previousNote = note.NoteType;

            // Note tie type
            var noteTieType = NoteTieType.None;
            if (note.IsTied)
            {
                noteTieType = NoteTieType.Start;
                _isTied = true;
            }
            else if (_isTied)
            {
                noteTieType = NoteTieType.Stop;
                _isTied = false;
            }

            // Create and add new note
            var newNote = new PSAMControlLibrary.Note(note.NoteType.ToString(),
                    (int) note.AlterType, _previousOctave, (MusicalSymbolDuration) note.DurationType,
                    NoteStemDirection.Up,
                    noteTieType, new List<NoteBeamType> {NoteBeamType.Single})
                {NumberOfDots = note.Dots};

            _symbols.Add(newNote);
        }

        private void CalculateOctave(Models.Note note)
        {
            // Octaves
            var distanceWithPreviousNote = note.NoteType - _previousNote;
            if (distanceWithPreviousNote > 3) // Shorter path possible the other way around
            {
                distanceWithPreviousNote -= 7; // The number of notes in an octave
            }
            else if (distanceWithPreviousNote < -3)
            {
                distanceWithPreviousNote += 7; // The number of notes in an octave
            }

            if (distanceWithPreviousNote + (int) _previousNote >= 7)
            {
                _previousOctave++;
            }
            else if (distanceWithPreviousNote + (int) _previousNote < 0)
            {
                _previousOctave--;
            }

            // Force up or down.
            if (note.Octaves > 0)
            {
                _previousOctave += note.Octaves;
            }
            else
            {
                _previousOctave -= Math.Abs(note.Octaves);
            }

            if (_remembedFirstOctaveAlternative)
            {
                _firstOctaveAlternative = _previousOctave;
                _remembedFirstOctaveAlternative = false;
            }
        }

        private void HandleRest()
        {
            var rest = (Models.Rest) _element;
            _symbols.Add(new PSAMControlLibrary.Rest((MusicalSymbolDuration) rest.DurationType));
        }

        private void HandleBar()
        {
            var barLine = new Barline();
            if (_isCreatingAlternative)
            {
                barLine.AlternateRepeatGroup = _repeatGroup;
            }
            _symbols.Add(barLine);
        }

        private void HandleMusicBlock()
        {
            if (_isCreatingAlternative)
            {
                CreateAlternativesMusicBlock();
            }
            else
            {
                CreateOuterMusicBlock();
            }
        }

        private void CreateOuterMusicBlock()
        {
            var musicBlock = (MusicBlock) _element;
            HandleTime(musicBlock.TimeSignature);
            RunActions(musicBlock.Elements);
        }

        private void CreateAlternativesMusicBlock()
        {
            _currentAlternative++;
            var musicBlock = (MusicBlock) _element;
            HandleTime(musicBlock.TimeSignature);

            // Get difference in octaves
            var differenceOctaves = 0;

            musicBlock.Elements.ForEach(e =>
            {
                if (!(e is MusicElement)) return;

                var musicElement = (MusicElement) e;
                differenceOctaves += musicElement.Octaves;
            });

            // Check runs
            var runs = 1;
            if (_currentAlternative == 1)
            {
                runs = 1 + _repeats - _alternatives;
            }

            // Do runs
            for (var i = 1; i <= runs; i++)
            {
                // Create repeat group
                _repeatGroup++;

                // Opening bar
                if (_symbols.Last() is Barline)
                {
                    var bar = (Barline) _symbols.Last();
                    bar.AlternateRepeatGroup = _repeatGroup;
                }
                else
                {
                    var barLine = new Barline {AlternateRepeatGroup = _repeatGroup};
                    _symbols.Add(barLine);
                }

                _remembedFirstOctaveAlternative = true;

                // Elements in alternative
                RunActions(musicBlock.Elements);

                if (runs > 1 && i < runs)
                {
                    _mustAlterOctave = true;
                    _alterOctave = _firstOctaveAlternative;
                }

                // Closing bar repeat if needed
                if (_repeatGroup < _repeats)
                {
                    var barLineEnd = new Barline {RepeatSign = RepeatSignType.Backward};
                    _symbols.Add(barLineEnd);
                }
                else
                {
                    var barLineEnd = new Barline {AlternateRepeatGroup = _repeatGroup};
                    _symbols.Add(barLineEnd);
                }
            }
        }

        private void HandleRepeat()
        {
            _repeat = (Repeat) _element;
            _repeatGroup = 0;
            _repeats = _repeat.Repeats;
            _isCreatingRepeat = true;
            HandleTime(_repeat.TimeSignature);

            // Check and remove previous bar if it exists
            var index = _symbols.Count - 1;
            if (_symbols[index].Type is MusicalSymbolType.Barline)
            {
                _symbols.RemoveAt(_symbols.Count - 1);
            }

            // Add bar for repeat
            var barLine = new Barline {RepeatSign = RepeatSignType.Forward};
            _symbols.Add(barLine);

            // Repeat block
            RunActions(_repeat.Elements);

            _isCreatingRepeat = false;
        }

        private void HandleAlternatives()
        {
            if (_repeat == null) return;

            _isCreatingAlternative = true;
            _alterOctave = 0;

            var alternative = (Alternative) _element;

            HandleTime(alternative.TimeSignature);

            _alternatives = alternative.Elements.Count;
            _currentAlternative = 0;

            RunActions(alternative.Elements);

            _isCreatingAlternative = false;

            _repeat = null;
            _repeats = 0;
            _repeatGroup = 0;
        }

        private void HandleTime(Models.TimeSignature timeSignature)
        {
            if (_currentTimeSignature.Top == timeSignature.Top &&
                _currentTimeSignature.Bottom == timeSignature.Bottom) return;

            _symbols.Add(new PSAMControlLibrary.TimeSignature(TimeSignatureType.Numbers, (uint) timeSignature.Top,
                (uint) timeSignature.Bottom));
            _currentTimeSignature = timeSignature;
        }

        private void HandleClef(Enums.ClefType clefType)
        {
            ClefType clef = (ClefType)clefType;

            if (_currentClefType != clefType)
            {
                // Row
                int row = 2;// G
                if (clef == ClefType.FClef) row = 4;// F

                _symbols.Add(new Clef(clef, row));//(int)(clefType + 1) * 2
                _currentClefType = clefType;
            }
        }
    }
}
