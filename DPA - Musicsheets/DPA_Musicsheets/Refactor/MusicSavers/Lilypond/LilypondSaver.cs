using DPA_Musicsheets.Refactor.Models.Block;
using DPA_Musicsheets.Refactor.MusicConverters.Lilypond;

namespace DPA_Musicsheets.Refactor.MusicSavers.Lilypond
{
    public class LilypondSaver : AbstractMusicSaver
    {
        private readonly LilypondConverter _musicConverter;

        public LilypondSaver()
        {
            _musicConverter = new LilypondConverter();
            FilterName = "Lilypond";
            Extension = ".ly";
        }

        public override string Save(Piece piece)
        {
            try
            {
                System.IO.File.WriteAllText(FilePath, _musicConverter.Convert(piece));
            }
            catch
            {
                return "Something went wrong while saving the Lilypond file.";
            }

            return null;
        }
    }
}
