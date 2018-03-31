using System.Collections.Generic;
using DPA_Musicsheets.Refactor.Builders;
using DPA_Musicsheets.Refactor.Models;
using DPA_Musicsheets.Refactor.Models.Base;
using DPA_Musicsheets.Refactor.Models.Block;
using Sanford.Multimedia.Midi;
using static DPA_Musicsheets.Refactor.Enums;

namespace DPA_Musicsheets.Refactor.MusicLoaders.Midi
{
    public class MidiChannelHandler
    {
        private MidiStrategy _midiStrategy;
        private int _previousMidiKey = 60; // Central C;
        private int _previousNoteAbsoluteTicks = 0;
        private double _notesOfBarReached = 0;
        private bool _creatingNote;

        public MidiChannelHandler(MidiStrategy midiStrategy, Piece piece)
        {
            _midiStrategy = midiStrategy;
            Piece = piece;
            _previousMidiKey = 60;// Central C
            _previousNoteAbsoluteTicks = 0;
            _notesOfBarReached = 0;
        }

        public Piece Piece { get; set; }
        public ConversionState ConversionState { get; set; }
        
        private TimeSignature _timeSignature;

        public TimeSignature CurrentTimeSignature
        {
            get
            {
                return _timeSignature;
            }
            set
            {
                _timeSignature = value;

                int divider = _timeSignature.Bottom / 4;
                EasyTimeSignature = new TimeSignature(value.Top / divider, value.Bottom / divider);
            }
        }

        public TimeSignature EasyTimeSignature { get; set; }

        public bool NoteIsTied { get; set; }

        public Dictionary<int, NoteBuilder> NoteBuilderDictionary => new Dictionary<int, NoteBuilder>
        {
            {0, new NoteBuilder(NoteType.C) },
            {1, new NoteBuilder(NoteType.C).Sharp() },
            {2, new NoteBuilder(NoteType.D) },
            {3, new NoteBuilder(NoteType.D).Sharp() },
            {4, new NoteBuilder(NoteType.E) },
            {5, new NoteBuilder(NoteType.F) },
            {6, new NoteBuilder(NoteType.F).Sharp() },
            {7, new NoteBuilder(NoteType.G) },
            {8, new NoteBuilder(NoteType.G).Sharp() },
            {9, new NoteBuilder(NoteType.A) },
            {10, new NoteBuilder(NoteType.A).Sharp() },
            {11, new NoteBuilder(NoteType.B) }
        };

        public void HandleChannelData(MidiEvent previousMidiEvent, MidiEvent midiEvent)
        {
            Piece = Piece;

            // Get previous channel message
            ChannelMessage previousChannelMessage = null;
            if (previousMidiEvent != null)
            {
                previousChannelMessage = previousMidiEvent.MidiMessage as ChannelMessage;
            }

            // Get current channel message
            ChannelMessage channelMessage = midiEvent.MidiMessage as ChannelMessage;

            if (channelMessage.Command == ChannelCommand.NoteOn)
            {
                if (channelMessage.Data2 > 0)
                {
                    _creatingNote = true;
                } else if (_creatingNote == true)
                {
                    if (previousChannelMessage != null && previousChannelMessage.Data2 != 0)
                    {
                        HandleNote(midiEvent, previousChannelMessage);
                    }
                    _creatingNote = false;
                } else
                {
                    HandleRest(midiEvent);
                }
            }
        }

        private void AddToPiece(Element element)
        {
            if (ConversionState == ConversionState.Normal)
            {
                Piece.AddElement(element, true);
            }
            else
            {
                var block = (BlockElement)Piece.Elements[Piece.Elements.Count - 1];
                for (int i = 1; i < _midiStrategy.OpenBlocks; i++)
                {
                    block = (BlockElement)block.Elements[block.Elements.Count - 1];
                }

                block.AddElement(element, true);
            }
        }

        private void HandleNote(MidiEvent midiEvent, ChannelMessage previousChannelMessage)
        {
            double lengthOfNote = CalculatePartOfBar(_previousNoteAbsoluteTicks, midiEvent.AbsoluteTicks);
            double availableLengthBar = EasyTimeSignature.Top - _notesOfBarReached;

            while (lengthOfNote > 0)
            {
                // Get note
                int midiKeyNote = previousChannelMessage.Data1;
                NoteBuilder noteBuilder = NoteBuilderDictionary[midiKeyNote % 12];

                int distance = midiKeyNote - _previousMidiKey;
                _previousMidiKey = midiKeyNote;
                while (distance < -6)
                {
                    noteBuilder.DecreaseOctave();
                    distance += 8;
                }

                while (distance > 6)
                {
                    noteBuilder.IncreaseOctave();
                    distance -= 8;
                }

                // Get duration
                int duration;
                int dots;
                bool isTied;
                HandleDuration(lengthOfNote, availableLengthBar, out lengthOfNote, out availableLengthBar, out duration, out dots, out isTied);

                // Create note and add
                noteBuilder.Duration((DurationType)duration);
                noteBuilder.IsDotted(dots);
                noteBuilder.IsTied(isTied);

                if (NoteIsTied)
                {
                    noteBuilder.IsTied();
                    NoteIsTied = false;
                }

                AddToPiece(noteBuilder.Build());

                // Check bar
                if (availableLengthBar == 0)
                {
                    availableLengthBar = EasyTimeSignature.Top;
                }
                
                _notesOfBarReached = EasyTimeSignature.Top - availableLengthBar;
            }

            _previousNoteAbsoluteTicks = midiEvent.AbsoluteTicks;
        }

        private void HandleRest(MidiEvent midiEvent)
        {
            double lengthOfNote = CalculatePartOfBar(_previousNoteAbsoluteTicks, midiEvent.AbsoluteTicks);
            double availableLengthBar = EasyTimeSignature.Top - _notesOfBarReached;

            while (lengthOfNote > 0)
            {
                RestBuilder restBuilder = new RestBuilder();

                // Get duration
                int duration;
                int dots;
                bool isTied;
                HandleDuration(lengthOfNote, availableLengthBar, out lengthOfNote, out availableLengthBar, out duration, out dots, out isTied);

                // Create rest and add
                restBuilder.Duration((DurationType)duration);
                restBuilder.IsDotted(dots);
                AddToPiece(restBuilder.Build());

                // Check bar
                if (availableLengthBar == 0)
                {
                    availableLengthBar = EasyTimeSignature.Top;
                }
                
                _notesOfBarReached = EasyTimeSignature.Top - availableLengthBar;
            }

            _previousNoteAbsoluteTicks = midiEvent.AbsoluteTicks;
        }

        private double CalculatePartOfBar(int absoluteTicks, int nextNoteAbsoluteTicks)
        {
            double deltaTicks = nextNoteAbsoluteTicks - absoluteTicks;

            if (deltaTicks <= 0)
            {
                return 0;
            }

            double percentageOfBeatNote = deltaTicks / Piece.Division;
            return percentageOfBeatNote;

            //return 1.0 / CurrentTimeSignature.Top * percentageOfBeatNote;
        }

        private void HandleDuration(double lengthOfNote, double availableLengthBar, out double returnedPercentageOfBar, out double returnedAvailableLengthBar, out int duration, out int dots, out bool isTied)
        {
            isTied = false;
            duration = 0;
            dots = 0;

            if (lengthOfNote <= availableLengthBar)
            {
                GetNoteLength(lengthOfNote, out duration, out dots);
                returnedAvailableLengthBar = availableLengthBar - lengthOfNote;
                returnedPercentageOfBar = 0;
            }
            else
            {
                isTied = true;
                GetNoteLength(availableLengthBar, out duration, out dots);
                returnedPercentageOfBar = lengthOfNote - availableLengthBar;
                returnedAvailableLengthBar = 0;
            }
        }

        private void GetNoteLength(double remainingDuration, out int duration, out int dots)
        {
            double percentageOfBeatNote = remainingDuration;
            duration = 0;
            dots = 0;
            bool durationFound = false;

            for (double i = 1; i <= 32; i = i * 2)
            {
                if (durationFound)
                {
                    if (remainingDuration == 0 || dots >= 4) break;

                    remainingDuration = remainingDuration - (4.0 / i);
                    dots++;
                }
                else
                {
                    if (percentageOfBeatNote % (4.0 / i) == 0)
                    {
                        if (percentageOfBeatNote / (4.0 / i) != 1)
                        {
                            i = i / 2;
                        }

                        duration = (int)i;
                        remainingDuration = remainingDuration - (4.0 / i);
                        durationFound = true;
                    }
                }
            }
        }
    }
}