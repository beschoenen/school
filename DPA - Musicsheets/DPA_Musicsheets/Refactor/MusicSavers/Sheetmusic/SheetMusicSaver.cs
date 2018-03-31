using System.Diagnostics;
using System.IO;
using DPA_Musicsheets.Refactor.Models.Block;
using DPA_Musicsheets.Refactor.MusicConverters.Lilypond;

namespace DPA_Musicsheets.Refactor.MusicSavers.Sheetmusic
{
    public class SheetMusicSaver : AbstractMusicSaver
    {
        private readonly LilypondConverter _musicConverter;

        public SheetMusicSaver()
        {
            _musicConverter = new LilypondConverter();
            FilterName = "Sheet music";
            Extension = ".pdf";
        }

        public override string Save(Piece piece)
        {
            const string lilypondLocation = @"C:\Program Files (x86)\LilyPond\usr\bin\lilypond.exe";
            if (!File.Exists(lilypondLocation))
            {
                return "Please install Lilypond to save PDF files.";
            }

            var tmpFileName = $"{FilePath}-tmp.ly";
            File.WriteAllText(tmpFileName, _musicConverter.Convert(piece));

            var sourceFolder = Path.GetDirectoryName(tmpFileName);
            var sourceFileName = Path.GetFileNameWithoutExtension(tmpFileName);
            var targetFolder = Path.GetDirectoryName(FilePath);
            var targetFileName = Path.GetFileNameWithoutExtension(FilePath);

            var process = new Process
            {
                StartInfo =
                {
                    WorkingDirectory = sourceFolder,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    Arguments = $"--pdf \"{sourceFolder}\\{sourceFileName}.ly\"",
                    FileName = lilypondLocation
                }
            };

            process.Start();

            while (!process.HasExited)
            { 
                /* Wait for exit */
            }

            if (sourceFolder != targetFolder || sourceFileName != targetFileName)
            {
                File.Move(sourceFolder + "\\" + sourceFileName + ".pdf", targetFolder + "\\" + targetFileName + ".pdf");
                File.Delete(tmpFileName);
            }

            return null;
        }
    }
}
