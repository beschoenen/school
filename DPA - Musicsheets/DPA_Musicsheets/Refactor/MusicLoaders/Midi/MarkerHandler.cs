using System;
using System.Collections.Generic;
using DPA_Musicsheets.Refactor.Models;
using DPA_Musicsheets.Refactor.Models.Base;
using DPA_Musicsheets.Refactor.Models.Block;
using static DPA_Musicsheets.Refactor.Enums;

namespace DPA_Musicsheets.Refactor.MusicLoaders.Midi
{
    public class MarkerHandler
    {
        private MidiStrategy _midiStrategy;
        private Piece _piece;
        private Repeat _repeat;
        private TimeSignature _timeSignature;
        private Tuple<Marker, int> _marker;

        public MarkerHandler(MidiStrategy midiStrategy, Piece piece)
        {
            _midiStrategy = midiStrategy;
            _piece = piece;
            _repeat = null;
            _midiStrategy.OpenBlocks = 0;
        }

        public Dictionary<Marker, Action> MarkerDictionary
        {
            get
            {
                return new Dictionary<Marker, Action>()
                {
                    { Marker.RepeatStart,  HandleRepeatStart },
                    { Marker.RepeatEnd, HandleEnd },
                    { Marker.AlternativesStart,  HandleAlternativesStart },
                    { Marker.AlternativesEnd,  HandleEnd },
                    { Marker.AlternativeStart,  HandleAlternativeStart },
                    { Marker.MusicBlockStart, HandleMusicBlockStart },
                    { Marker.MusicBlockEnd, HandleEnd },
                    { Marker.Tied, HandleTie }
                };
            }
        }

        public void HandleMarker(Tuple<Marker, int> marker, TimeSignature timeSignature)
        {
            _marker = marker;
            _timeSignature = timeSignature;

            Action action;
            if (MarkerDictionary.TryGetValue(marker.Item1, out action))
            {
                action();
            }
        }

        public void HandleMarker(Marker marker, TimeSignature timeSignature)
        {
            HandleMarker(new Tuple<Marker, int>(marker, 0), timeSignature);
        }

        private void AddElement(BlockElement element)
        {
            element.TimeSignature = _timeSignature;
            element.Tempo = _piece.Tempo;

            if (_midiStrategy.OpenBlocks == 0)
            {
                _piece.AddElement(element, true);
            }
            else
            {
                try
                {
                    BlockElement block = (BlockElement)_piece.Elements[_piece.Elements.Count - 1];
                    for (int i = 1; i < _midiStrategy.OpenBlocks; i++)
                    {
                        block = (BlockElement)block.Elements[block.Elements.Count - 1];
                    }

                    block.AddElement(element, true);
                }
                catch (Exception) { }

            }

            _midiStrategy.OpenBlocks++;
        }

        private void HandleRepeatStart()
        {
            _repeat = new Repeat();
            _repeat.Repeats = _marker.Item2;
            AddElement(_repeat);
        }

        private void HandleAlternativesStart()
        {
            Alternative alternative = new Alternative();
            AddElement(alternative);
        }

        private void HandleAlternativeStart()
        {
            HandleMusicBlockStart();
        }

        private void HandleMusicBlockStart()
        {
            MusicBlock musicBlock = new MusicBlock();
            AddElement(musicBlock);
        }

        private void HandleEnd()
        {
            _midiStrategy.OpenBlocks--;
        }

        private void HandleTie()
        {
            _midiStrategy.HandleTie();
        }
    }
}
