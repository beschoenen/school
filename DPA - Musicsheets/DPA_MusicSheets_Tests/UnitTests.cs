using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DPA_Musicsheets.Refactor.MusicLoaders.Midi;
using DPA_Musicsheets.Refactor.Models;
using DPA_Musicsheets.Refactor.MusicConverters;
using System.Collections.Generic;
using DPA_Musicsheets.Refactor.Models.Block;
using DPA_Musicsheets.Refactor.MusicConverters.Sheetmusic;
using static DPA_Musicsheets.Refactor.Enums;
using DPA_Musicsheets.Refactor.MusicLoaders.Lilypond;

namespace DPA_MusicSheets_Tests
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void LoadLilypond()
        {
            LilypondLoader lilypondLoader = new LilypondLoader();
            //lilypondLoader.FilePath = "C:\\Users\\Danielle\\Documents\\SourceTree projects\\DPA_Startup_Musicsheets\\DPA_Musicsheets\\Files\\Alle-eendjes-zwemmen-in-het-water.ly";
            lilypondLoader.FilePath = "C:\\Users\\Danielle\\Documents\\SourceTree projects\\DPA_Startup_Musicsheets\\DPA_Musicsheets\\Files\\Twee-emmertjes-water-halen.ly";
            Piece piece = lilypondLoader.Load();

            string test = "";
        }

        [TestMethod]
        public void LoadMidi()
        {
            MidiLoader midiLoader = new MidiLoader();
            //midiLoader.FilePath = "C:\\Users\\Danielle\\Documents\\SourceTree projects\\DPA_Startup_Musicsheets\\DPA_Musicsheets\\Files\\Alle-eendjes-zwemmen-in-het-water.mid";
            midiLoader.FilePath = "C:\\Users\\Danielle\\Documents\\SourceTree projects\\DPA_Startup_Musicsheets\\DPA_Musicsheets\\Files\\Twee-emmertjes-water-halen.mid";
            Piece piece = midiLoader.Load();

            string info = string.Empty;

            piece.Elements.ForEach(e =>
            {
                if (e.GetType() == typeof(Note))
                {
                    info += e.ToString();

                    Note n = (Note)e;
                    // Octave
                    if (n.Octaves > 0)
                    {
                        info += new string('\'', n.Octaves);
                    }
                    else
                    {
                        info += new string(',', Math.Abs(n.Octaves));
                    }

                    info += (int)n.DurationType;
                    info += new string('.', n.Dots);
                    info += " ";
                }
                else if (e.GetType() == typeof(Rest))
                {
                    info += e.ToString();

                    Rest r = (Rest)e;
                    info += (int)r.DurationType;
                    info += new string('.', r.Dots);
                    info += " ";
                }
                else if (e.GetType() == typeof(Token))
                {
                    Token t = (Token)e;
                    if (t.TokenKind == TokenKind.Bar)
                    {
                        info += "|";
                        System.Diagnostics.Debug.WriteLine(info);
                        info = string.Empty;
                    }
                }
            });

            var test = "";

            // Sheet music converter
            var sheetMusicConverter = new SheetMusicConverter();
            var WPFStaffs = (List<PSAMControlLibrary.MusicalSymbol>)sheetMusicConverter.Convert(piece);

            test = "";
        }
    }
}