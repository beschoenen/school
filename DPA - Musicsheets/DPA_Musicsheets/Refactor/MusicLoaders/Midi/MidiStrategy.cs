using System;
using System.Collections.Generic;
using System.Linq;
using DPA_Musicsheets.Refactor.Models;
using DPA_Musicsheets.Refactor.Models.Block;
using Sanford.Multimedia.Midi;
using static DPA_Musicsheets.Refactor.Enums;

namespace DPA_Musicsheets.Refactor.MusicLoaders.Midi
{
    public class MidiStrategy
    {
        public readonly Piece Piece;

        private MidiEvent _previousMidiEvent;
        private MidiEvent _midiEvent;

        private readonly MidiChannelHandler _midiChannelHandler;
        private readonly MetaChannelHandler _metaChannelHandler;
        private readonly MarkerHandler _markerHandler;

        private int _previousAbsoluteTicks;
        private int _absoluteTicks;

        private int _currentIndexTimeSignatures;
        private int _currentIndexMarkers;

        public MidiStrategy(Piece piece)
        {
            Piece = piece;
            _midiChannelHandler = new MidiChannelHandler(this, Piece);
            _metaChannelHandler = new MetaChannelHandler(this);
            _markerHandler = new MarkerHandler(this, Piece);

            _previousAbsoluteTicks = 0;
            _absoluteTicks = 0;

            _currentIndexTimeSignatures = 0;
            _currentIndexMarkers = 0;

            TimeSignatures = new Dictionary<int, TimeSignature>();
            Markers = new Dictionary<int, List<Tuple<Marker, int>>>();
        }

        public Dictionary<int, TimeSignature> TimeSignatures { get; set; }

        public Dictionary<int, List<Tuple<Marker, int>>> Markers { get; set; }

        public int OpenBlocks;

        public Dictionary<MessageType, Action> MessageTypesDictionary => new Dictionary<MessageType, Action>()
        {
            { MessageType.Meta,  HandleMetaData },
            { MessageType.Channel, HandleChannelData }
        };

        public void HandleMidiEvent(MidiEvent previousMidiEvent, MidiEvent midiEvent)
        {
            _previousMidiEvent = previousMidiEvent;
            _midiEvent = midiEvent;

            IMidiMessage midiMessage = midiEvent.MidiMessage;

            Action action;
            if (MessageTypesDictionary.TryGetValue(midiMessage.MessageType, out action))
            {
                action();
            }
        }

        private void HandleTimeSignaturesAndMarkers()
        {
            bool timeSignatureChanged = false;
            bool markerHandled = false;

            // Timesignature
            if (_currentIndexTimeSignatures < TimeSignatures.Count)
            {
                var nextTimeSignature = TimeSignatures.ElementAt(_currentIndexTimeSignatures);
                if (nextTimeSignature.Key <= _absoluteTicks)
                {
                    _midiChannelHandler.CurrentTimeSignature = nextTimeSignature.Value;
                    _currentIndexTimeSignatures++;
                    timeSignatureChanged = true;
                }
            }

            // Marker
            if (_currentIndexMarkers < Markers.Count)
            {
                var nextMarkers = Markers.ElementAt(_currentIndexMarkers);
                if (nextMarkers.Key <= _absoluteTicks)
                {
                    _currentIndexMarkers++;
                    var markers = nextMarkers.Value;

                    markers.ForEach(m =>
                    {
                        _markerHandler.HandleMarker(m, _midiChannelHandler.CurrentTimeSignature);
                    });
                    if (OpenBlocks == 0)
                    {
                        _midiChannelHandler.ConversionState = ConversionState.Normal;
                    }
                    else
                    {
                        _midiChannelHandler.ConversionState = ConversionState.Block;
                    }

                    markerHandled = true;
                }
            }

            // Fix
            if (timeSignatureChanged && !markerHandled && _currentIndexTimeSignatures > 1)
            {
                _markerHandler.HandleMarker(Marker.MusicBlockStart, _midiChannelHandler.CurrentTimeSignature);
                _midiChannelHandler.ConversionState = ConversionState.Block;
            }
        }

        public void Change(int absoluteTicks, TimeSignature timeSignature)
        {
            if (!TimeSignatures.ContainsKey(absoluteTicks))
            {
                TimeSignatures.Add(absoluteTicks, timeSignature);
            }

            if (Piece.TimeSignature == null)
            {
                Piece.TimeSignature = timeSignature;
            }
        }

        public void Change(Tempo tempo)
        {
            Piece.Tempo = tempo;
        }

        public void HandleMarker(int absoluteTicks, List<Tuple<Marker, int>> markers)
        {
            if (!Markers.ContainsKey(absoluteTicks))
            {
                Markers.Add(absoluteTicks, markers);
            }
        }

        public void HandleTie()
        {
            this._midiChannelHandler.NoteIsTied = true;
        }

        public void HandleChannelData()
        {
            _midiChannelHandler.HandleChannelData(_previousMidiEvent, _midiEvent);

            _previousAbsoluteTicks = _absoluteTicks;
            _absoluteTicks = _midiEvent.AbsoluteTicks;

            if (_midiChannelHandler.CurrentTimeSignature == null || _previousAbsoluteTicks < _absoluteTicks)
            {
                HandleTimeSignaturesAndMarkers();
            }
        }

        public void HandleMetaData()
        {
            _metaChannelHandler.HandleMetaData(_midiEvent);
        }
    }
}