using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using DPA_Musicsheets.Refactor.Models;
using Sanford.Multimedia.Midi;
using static DPA_Musicsheets.Refactor.Enums;

namespace DPA_Musicsheets.Refactor.MusicLoaders.Midi
{
    public class MetaChannelHandler
    {
        private readonly MidiStrategy _midiStrategy;
        private MidiEvent _midiEvent;

        public MetaChannelHandler(MidiStrategy midiStrategy)
        {
            _midiStrategy = midiStrategy;
        }

        public Dictionary<MetaType, Action> MetaTypesDictionary => new Dictionary<MetaType, Action>
        {
            { MetaType.TimeSignature,  HandleTimeSignature },
            { MetaType.Tempo, HandleTempo },
            { MetaType.EndOfTrack, HandleEndOfTrack },
            { MetaType.Marker, HandleMarker }
        };

        public void HandleMetaData(MidiEvent midiEvent)
        {
            _midiEvent = midiEvent;

            MetaMessage metaMessage = midiEvent.MidiMessage as MetaMessage;

            if (MetaTypesDictionary.TryGetValue(metaMessage.MetaType, out var action))
            {
                action();
            }
        }

        #region Meta data
        private void HandleTimeSignature()
        {
            // 4, 2, 18, 8
            // 6, 3, 18, 8
            var metaMessage = _midiEvent.MidiMessage as MetaMessage;
            var timeSignatureBytes = metaMessage.GetBytes();

            var beatNote = timeSignatureBytes[0];
            var beatsPerBar = (int) (1 / Math.Pow(timeSignatureBytes[1], -2));

            // Correct time signature
            var rest = beatsPerBar % 4;
            beatsPerBar -= rest;

            this._midiStrategy.Change(_midiEvent.AbsoluteTicks, new TimeSignature(beatNote, beatsPerBar));
        }

        private void HandleTempo()
        {
            var metaMessage = _midiEvent.MidiMessage as MetaMessage;
            var tempoBytes = metaMessage.GetBytes();
            var tempo = ((tempoBytes[0] & 0xff) << 16) | ((tempoBytes[1] & 0xff) << 8) | (tempoBytes[2] & 0xff);
            _midiStrategy.Change(new Tempo(4, 60000000 / tempo));
        }

        private void HandleEndOfTrack()
        {
            //if (_previousNoteAbsoluteTicks > 0)
            //{
            //    // Finish the last notelength.
            //    double percentageOfBar;
            //    int duration;
            //    int dots;
            //    GetNoteLength(_previousMidiEvent.AbsoluteTicks, _midiEvent.AbsoluteTicks, _midiEvent.DeltaTicks, out percentageOfBar, out duration, out dots);

            //    _percentageOfBarReached += percentageOfBar;
            //    if (_percentageOfBarReached >= 1)
            //    {
            //        //Piece.AddBar();
            //        percentageOfBar = percentageOfBar - 1;
            //    }
            //}
        }

        //private void HandleMarker()
        //{
        //    MetaMessage metaMessage = _midiEvent.MidiMessage as MetaMessage;
        //    byte[] markerBytes = metaMessage.GetBytes();
        //    string markerText = System.Text.Encoding.UTF8.GetString(markerBytes);
        //    Marker marker = (Marker)Enum.Parse(typeof(Marker), markerText);
        //    this._midiStrategy.HandleMarker(_midiEvent.AbsoluteTicks, marker);
        //}

        private void HandleMarker()
        {
            MetaMessage metaMessage = _midiEvent.MidiMessage as MetaMessage;
            byte[] markersBytes = metaMessage.GetBytes();
            List<Tuple<Marker, int>> markers = Deserialize(markersBytes);
            this._midiStrategy.HandleMarker(_midiEvent.AbsoluteTicks, markers);
        }

        private List<Tuple<Marker, int>> Deserialize(byte[] markersBytes)
        {
            var mStream = new MemoryStream();
            var binFormatter = new BinaryFormatter();

            // Where 'objectBytes' is your byte array.
            mStream.Write(markersBytes, 0, markersBytes.Length);
            mStream.Position = 0;

            return binFormatter.Deserialize(mStream) as List<Tuple<Marker, int>>;
        }
        #endregion
    }
}
