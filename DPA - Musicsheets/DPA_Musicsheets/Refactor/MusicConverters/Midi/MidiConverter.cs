using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using DPA_Musicsheets.Refactor.Models;
using DPA_Musicsheets.Refactor.Models.Base;
using DPA_Musicsheets.Refactor.Models.Block;
using Sanford.Multimedia.Midi;
using static DPA_Musicsheets.Refactor.Enums;

namespace DPA_Musicsheets.Refactor.MusicConverters.Midi
{
    public class MidiConverter : AbstractMusicConverter<Sequence>
    {
        #region Variables
        private Piece _piece;
        private Element _element;
        private Sequence _sequence;
        private int _absoluteTicks;
        private Track _metaTrack;
        private Track _notesTrack;
        private int _startHeight = 60;
        private bool _isCreatingMidiFile;

        readonly List<string> notesOrderWithCrosses = new List<string> { "c", "cis", "d", "dis", "e", "f", "fis", "g", "gis", "a", "ais", "b" };

        // Repeat and alternatives
        private List<Tuple<int, ChannelMessage>> _repeatList;
        private bool _isHandlingRepeat;
        private bool _isHandlingAlternatives;
        private bool _isHandlingAlternativeOption;
        private int _repeats;
        private int _alternatives;
        private int _currentAlternative;

        // Ties
        private bool _previousIsTied;
        private List<int> _indexesOfTies;
        private double _previousNoteDuration;

        // Flat and sharp
        private AlterType _previousNoteAlteration;

        // Markers
        private Dictionary<int, List<Tuple<Marker, int>>> _markerDictionary;
        #endregion

        public MidiConverter(bool isCreatingMidiFile)
        {
            _isCreatingMidiFile = isCreatingMidiFile;
            _indexesOfTies = new List<int>();
            _markerDictionary = new Dictionary<int, List<Tuple<Marker, int>>>();
        }

        #region Actions
        public Dictionary<Type, Action> ElementActionDictionary => new Dictionary<Type, Action>
        {
            { typeof(Note), HandleNote },
            { typeof(Rest), HandleRest },
            { typeof(MusicBlock), HandleMusicBlock },
            { typeof(Alternative), HandleAlternatives },
            { typeof(Repeat), HandleRepeat }
        };

        public void RunActions(List<Element> elements)
        {
            elements.ForEach(e =>
            {
                _element = e;
                Action action;
                if (ElementActionDictionary.TryGetValue(e.GetType(), out action))
                {
                    action();
                }
            });
        }
        #endregion

        public override Sequence Convert(Piece piece)
        {
            _piece = piece;
            _sequence = new Sequence();
            _absoluteTicks = 0;

            // Meta track
            _metaTrack = new Track();
            _sequence.Add(_metaTrack);
            _metaTrack.Insert(0, new MetaMessage(MetaType.Tempo, CalculateTempo(piece)));
            byte[] timeSignature = ConvertTimeSignatureToByteArray(_piece.TimeSignature);
            _metaTrack.Insert(0, new MetaMessage(MetaType.TimeSignature, timeSignature));

            // Note track
            _notesTrack = new Track();
            _sequence.Add(_notesTrack);

            // Run action for elements
            RunActions(piece.Elements);

            // Serialize markers
            foreach (KeyValuePair<int, List<Tuple<Marker, int>>> entry in _markerDictionary)
            {
                byte[] markersBytes = SerializeMarkers(entry.Value);
                _metaTrack.Insert(entry.Key, new MetaMessage(MetaType.Marker, markersBytes));
            }

            // End of track
            _notesTrack.Insert(_absoluteTicks, MetaMessage.EndOfTrackMessage);
            _metaTrack.Insert(_absoluteTicks, MetaMessage.EndOfTrackMessage);

            return _sequence;
        }

        private double CalculateDuration(DurationType durationType, int dots)
        {
            int beatNote = _piece.TimeSignature.Top;

            // Calculate duration
            double absoluteLength = 1.0 / (double)durationType;
            absoluteLength += (absoluteLength / 2.0) * dots;

            double relationToQuartNote = beatNote / 4.0;
            double percentageOfBeatNote = (1.0 / beatNote) / absoluteLength;
            double deltaTicks = (_sequence.Division / relationToQuartNote) / percentageOfBeatNote;

            return deltaTicks;
        }

        private void AddToTrack(double deltaTicks, int noteHeight = 0)
        {
            if (_previousNoteAlteration == AlterType.Flat)
            {
                noteHeight -= 1;
            }
            else if (_previousNoteAlteration == AlterType.Sharp)
            {
                noteHeight += 1;
            }

            if (_isHandlingRepeat)
            {
                if (noteHeight == 0)
                {
                    // Rest
                    _repeatList.Add(new Tuple<int, ChannelMessage>(0, new ChannelMessage(ChannelCommand.NoteOn, 1, noteHeight, 0)));
                }
                else
                {
                    // Note
                    if (_previousIsTied)
                    {
                        _indexesOfTies.Add(_repeatList.Count);
                    }
                    _repeatList.Add(new Tuple<int, ChannelMessage>(0, new ChannelMessage(ChannelCommand.NoteOn, 1, noteHeight, 90)));
                }

                _repeatList.Add(new Tuple<int, ChannelMessage>((int)deltaTicks, new ChannelMessage(ChannelCommand.NoteOn, 1, noteHeight, 0)));
            }
            else
            {
                if (noteHeight == 0)
                {
                    // Rest
                    _notesTrack.Insert(_absoluteTicks, new ChannelMessage(ChannelCommand.NoteOn, 1, noteHeight, 0)); // Data2 = volume
                }
                else
                {
                    // Note
                    if (_previousIsTied)
                    {
                        HandleMarker(Marker.Tied);
                    }

                    _notesTrack.Insert(_absoluteTicks, new ChannelMessage(ChannelCommand.NoteOn, 1, noteHeight, 90)); // Data2 = volume
                }

                _absoluteTicks += (int)deltaTicks;
                _absoluteTicks += (int)_previousNoteDuration;
                _previousNoteDuration = 0;

                if (_absoluteTicks > 0)
                {
                    _notesTrack.Insert(_absoluteTicks, new ChannelMessage(ChannelCommand.NoteOn, 1, noteHeight, 0)); // Data2 = volume
                }
            }
        }

        private void HandleNote()
        {
            Note note = (Note)_element;
            double deltaTicks = CalculateDuration(note.DurationType, note.Dots);

            // Calculate height
            int noteHeight = _startHeight;
            if (note.Octaves != 0)
            {
                noteHeight += (note.Octaves * 12);
            }
            int index = notesOrderWithCrosses.IndexOf(note.NoteType.ToString().ToLower());
            int previousIndex = (_startHeight % 12);
            if (index != previousIndex)
            {
                int difference = Math.Abs(previousIndex - index);
                int testIndex = 12 - difference;//5

                if (index < previousIndex && testIndex < 6)
                {
                    noteHeight += testIndex;
                }
                else if (index < previousIndex && testIndex > 6)
                {
                    noteHeight -= (previousIndex - index);
                }
                else if (index > previousIndex && testIndex < 6)
                {
                    noteHeight -= testIndex;
                }
                else
                {
                    noteHeight -= (previousIndex - index);
                }
            }

            _startHeight = noteHeight;

            // Remember altertype
            _previousNoteAlteration = note.AlterType;

            // Check tie
            if (note.IsTied)
            {
                _previousIsTied = true;
                _previousNoteDuration += deltaTicks;
                if (_isCreatingMidiFile)
                {
                    _previousNoteDuration = 0;
                    AddToTrack(deltaTicks, noteHeight);
                }
            }
            else
            {
                _previousIsTied = false;
                AddToTrack(deltaTicks, noteHeight);
            }
        }

        private void HandleRest()
        {
            Rest rest = (Rest)_element;
            double deltaTicks = CalculateDuration(rest.DurationType, rest.Dots);
            AddToTrack(deltaTicks);
        }

        private void HandleMusicBlock()
        {
            MusicBlock musicBlock = (MusicBlock)_element;
            HandleTime(musicBlock.TimeSignature);

            if (!_isHandlingAlternatives || _isHandlingAlternativeOption)
            {
                this.CreateNormalMusicBlock(musicBlock);
            }
            else
            {
                this.CreateAlternativesMusicBlock(musicBlock);
            }

            HandleMarker(Marker.MusicBlockEnd);
        }

        private void CreateNormalMusicBlock(MusicBlock musicBlock)
        {
            // Music block
            HandleMarker(Marker.MusicBlockStart);
            RunActions(musicBlock.Elements);
            HandleMarker(Marker.MusicBlockEnd);
        }

        private void CreateAlternativesMusicBlock(MusicBlock musicBlock)
        {
            HandleMarker(Marker.AlternativeStart);

            int differenceOctaves = 0;
            musicBlock.Elements.ForEach(e =>
            {
                //if (e is MusicElement && musicBlock.Elements.ElementAt(0) != e)
                if (e is MusicElement)
                {
                    MusicElement musicElement = (MusicElement)e;
                    differenceOctaves += musicElement.Octaves;
                }
            });

            if (_isCreatingMidiFile)
            {
                // Repeat already in sequence, add alternative
                _isHandlingAlternativeOption = true;
                RunActions(musicBlock.Elements);
                _isHandlingAlternativeOption = false;
            }
            else
            {
                // Midi player
                int runs = 1;
                if (_currentAlternative == 1)
                {
                    runs = 1 + _repeats - _alternatives;
                }

                for (int i = 1; i <= runs; i++)
                {
                    // Add repeat and alternative to sequence
                    AddRepeatForMidiPlayer();
                    _isHandlingAlternativeOption = true;
                    RunActions(musicBlock.Elements);
                    _isHandlingAlternativeOption = false;

                    if (runs > 1 && i < runs)
                    {
                        _startHeight += 12 * (differenceOctaves * -1);
                    }
                }

                _currentAlternative++;
            }
        }

        private void AddRepeatForMidiPlayer()
        {
            for (int i = 0; i < _repeatList.Count; i++)
            {
                if (_indexesOfTies.Contains(i))
                {
                    HandleMarker(Marker.Tied);
                }

                var t = _repeatList[i];
                _notesTrack.Insert(_absoluteTicks + t.Item1, t.Item2);
                _absoluteTicks += t.Item1;
            }
        }

        private void HandleRepeat()
        {
            _repeatList = new List<Tuple<int, ChannelMessage>>();
            _isHandlingRepeat = true;

            // Run action for elements
            Repeat repeat = (Repeat)_element;
            HandleTime(repeat.TimeSignature);
            HandleMarker(Marker.RepeatStart, repeat.Repeats);
            _repeats = repeat.Repeats;
            RunActions(repeat.Elements);

            if (_isCreatingMidiFile)
            {
                // Add repeat
                _repeatList.ForEach(t =>
                {
                    _notesTrack.Insert(_absoluteTicks + t.Item1, t.Item2);
                    _absoluteTicks += t.Item1;
                });
            }

            HandleMarker(Marker.RepeatEnd);
            _isHandlingRepeat = false;
        }

        private void HandleAlternatives()
        {
            if (_repeatList.Count == 0) return;

            _isHandlingAlternatives = true;
            _isHandlingAlternativeOption = false;

            // Run action for elements
            Alternative alternative = (Alternative)_element;
            HandleTime(alternative.TimeSignature);
            HandleMarker(Marker.AlternativesStart);
            _alternatives = alternative.Elements.Count;
            _currentAlternative = 1;
            RunActions(alternative.Elements);

            // End alternatives
            HandleMarker(Marker.AlternativesEnd);
            _isHandlingAlternatives = false;
        }

        #region Meta data
        private byte[] CalculateTempo(Piece piece)
        {
            // Calculate tempo
            int speed = (60000000 / piece.Tempo.Count);
            byte[] tempo = new byte[3];
            tempo[0] = (byte)((speed >> 16) & 0xff);
            tempo[1] = (byte)((speed >> 8) & 0xff);
            tempo[2] = (byte)(speed & 0xff);
            return tempo;
        }

        private void HandleTime(TimeSignature signature)
        {
            byte[] timeSignature = ConvertTimeSignatureToByteArray(signature);
            _metaTrack.Insert(_absoluteTicks, new MetaMessage(MetaType.TimeSignature, timeSignature));
        }

        private byte[] ConvertTimeSignatureToByteArray(TimeSignature signature)
        {
            byte[] timeSignature = new byte[4];
            timeSignature[0] = (byte)signature.Top;
            timeSignature[1] = (byte)(Math.Log(signature.Bottom) / Math.Log(2));
            return timeSignature;
        }

        private void HandleMarker(Marker marker, int value = 0)
        {
            if (!_markerDictionary.ContainsKey(_absoluteTicks))
            {
                _markerDictionary.Add(_absoluteTicks, new List<Tuple<Marker, int>>());
            }

            _markerDictionary[_absoluteTicks].Add(new Tuple<Marker, int>(marker, value));
        }

        private byte[] SerializeMarkers(List<Tuple<Marker, int>> markers)
        {
            var binFormatter = new BinaryFormatter();
            var mStream = new MemoryStream();
            binFormatter.Serialize(mStream, markers);
            return mStream.ToArray();
        }
        #endregion
    }
}