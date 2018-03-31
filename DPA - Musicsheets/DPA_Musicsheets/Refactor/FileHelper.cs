using System.IO;
using System.Linq;
using DPA_Musicsheets.Refactor.Models.Block;
using DPA_Musicsheets.Refactor.MusicLoaders;
using DPA_Musicsheets.Refactor.MusicSavers;
using Microsoft.Win32;

namespace DPA_Musicsheets.Refactor
{
    public class FileHelper
    {
        private static FileHelper _fileHelper;
        public static FileHelper Instance => _fileHelper ?? (_fileHelper = new FileHelper());

        public AbstractMusicLoader ReadFile()
        {
            var factory = new MusicLoaderFactory();
            var openFileDialog = new OpenFileDialog
            {
                Filter = string.Join("|", factory.MusicLoaders.Select(t => t.Filter).ToArray())
            };

            if (openFileDialog.ShowDialog() == true)
            {
                var extension = Path.GetExtension(openFileDialog.FileName);
                var musicLoader = factory.GetMusicLoader(extension);

                if (musicLoader != null)
                {
                    musicLoader.FilePath = openFileDialog.FileName;
                    return musicLoader;
                }
            }

            return null;
        }

        public string WriteFile(Piece piece, string filter = null)
        {
            var factory = new MusicSaverFactory();
            var saveFileDialog = new SaveFileDialog
            {
                Filter = filter ?? string.Join("|", factory.MusicSavers.Select(t => t.Filter).ToArray())
            };

            if ((saveFileDialog.ShowDialog() ?? false) == false) return null;

            var extension = Path.GetExtension(saveFileDialog.FileName);
            var musicSaver = factory.GetMusicSaver(extension);

            if (musicSaver != null)
            {
                musicSaver.FilePath = saveFileDialog.FileName;
                return musicSaver.Save(piece);
            }

            return null;
        }
    }
}
