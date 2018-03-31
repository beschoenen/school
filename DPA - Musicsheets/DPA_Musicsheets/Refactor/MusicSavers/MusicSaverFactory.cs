using System.Collections.Generic;
using System.Linq;
using DPA_Musicsheets.Refactor.MusicSavers.Lilypond;
using DPA_Musicsheets.Refactor.MusicSavers.Midi;
using DPA_Musicsheets.Refactor.MusicSavers.Sheetmusic;

namespace DPA_Musicsheets.Refactor.MusicSavers
{
    public class MusicSaverFactory
    {
        public readonly List<AbstractMusicSaver> MusicSavers;

        public MusicSaverFactory()
        {
            MusicSavers = new List<AbstractMusicSaver>
            {
                new MidiSaver(),
                new LilypondSaver(),
                new SheetMusicSaver()
            };
        }

        public AbstractMusicSaver GetMusicSaver(string extension)
        {
            return MusicSavers.FirstOrDefault(l => l.Extension.Equals(extension));
        }
    }
}
