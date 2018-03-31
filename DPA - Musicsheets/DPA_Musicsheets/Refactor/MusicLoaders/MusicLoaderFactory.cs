using System.Collections.Generic;
using System.Linq;
using DPA_Musicsheets.Refactor.MusicLoaders.Lilypond;
using DPA_Musicsheets.Refactor.MusicLoaders.Midi;

namespace DPA_Musicsheets.Refactor.MusicLoaders
{
    public class MusicLoaderFactory
    {
        public readonly List<AbstractMusicLoader> MusicLoaders;

        public MusicLoaderFactory()
        {
            MusicLoaders = new List<AbstractMusicLoader>
            {
                new MidiLoader(),
                new LilypondLoader()
            };
        }

        public AbstractMusicLoader GetMusicLoader(string extension)
        {
            return MusicLoaders.FirstOrDefault(l => l.Extension.Equals(extension));
        }
    }
}
