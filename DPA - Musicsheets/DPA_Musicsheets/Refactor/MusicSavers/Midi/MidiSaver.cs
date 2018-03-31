using DPA_Musicsheets.Refactor.Models.Block;
using DPA_Musicsheets.Refactor.MusicConverters.Midi;

namespace DPA_Musicsheets.Refactor.MusicSavers.Midi
{
    public class MidiSaver : AbstractMusicSaver
    {
        private readonly MidiConverter _musicConverter;

        public MidiSaver()
        {
            _musicConverter = new MidiConverter(true);
            FilterName = "Midi";
            Extension = ".mid";
        }

        public override string Save(Piece piece)
        {
            try
            {
                _musicConverter.Convert(piece).Save(FilePath);
            }
            catch
            {
                return "Something went wrong while saving the Midi file.";
            }

            return null;
        }
    }
}
