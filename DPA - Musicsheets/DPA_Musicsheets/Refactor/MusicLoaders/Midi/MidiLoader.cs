using DPA_Musicsheets.Refactor.Models.Block;
using Sanford.Multimedia.Midi;

namespace DPA_Musicsheets.Refactor.MusicLoaders.Midi
{
    public class MidiLoader : AbstractMusicLoader
    {
        public MidiLoader()
        {
            FilterName = "Midi";
            Extension = ".mid";
        }

        public override Piece Load()
        {
            if (!FilePath.EndsWith(".mid"))
            {
                return new Piece(24);
            }

            var midiSequence = new Sequence();
            midiSequence.Load(FilePath);

            return LoadMidi(midiSequence);
        }

        private Piece LoadMidi(Sequence sequence)
        {
            // Create new piece
            var piece = new Piece(sequence.Division)
            {
                //TimeSignature = new TimeSignature(4, 4),
                //Tempo = new Tempo(4, 120),
                Clef = Enums.ClefType.G,// Clef treble
                Division = sequence.Division
            };

            // Create midi strategie
            var midiStrategy = new MidiStrategy(piece);

            // Previous midi event
            MidiEvent previousMidiEvent = null;

            foreach (var track in sequence)
            {
                // Handle midi events
                foreach (var midiEvent in track.Iterator())
                {
                    midiStrategy.HandleMidiEvent(previousMidiEvent, midiEvent);
                    previousMidiEvent = midiEvent;
                }
            }

            return piece;
        }
    }
}