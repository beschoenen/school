﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using DPA_Musicsheets.Models;
using PSAMControlLibrary;
using Sanford.Multimedia.Midi;

namespace DPA_Musicsheets.Managers
{
    public class FileHandler
    {
        private static readonly List<char> notesorder = new List<char> {'c', 'd', 'e', 'f', 'g', 'a', 'b'};

        private int _beatNote = 4; // De waarde van een beatnote.
        private int _beatsPerBar; // Aantal beatnotes per maat.
        private int _bpm = 120; // Aantal beatnotes per minute.
        private string _lilypondText;

        public string LilypondText
        {
            get => _lilypondText;
            set
            {
                _lilypondText = value;
                LilypondTextChanged?.Invoke(this, new LilypondEventArgs {LilypondText = value});
            }
        }

        public List<MusicalSymbol> WPFStaffs { get; set; } = new List<MusicalSymbol>();

        public Sequence MidiSequence { get; set; }

        public event EventHandler<LilypondEventArgs> LilypondTextChanged;
        public event EventHandler<WPFStaffsEventArgs> WPFStaffsChanged;
        public event EventHandler<MidiSequenceEventArgs> MidiSequenceChanged;

        public void OpenFile(string fileName)
        {
            if (Path.GetExtension(fileName).EndsWith(".mid"))
            {
                MidiSequence = new Sequence();
                MidiSequence.Load(fileName);
                MidiSequenceChanged?.Invoke(this, new MidiSequenceEventArgs { MidiSequence = MidiSequence });
                LoadMidi(MidiSequence);
            }
            else if (Path.GetExtension(fileName).EndsWith(".ly"))
            {
                var sb = new StringBuilder();
                foreach (var line in File.ReadAllLines(fileName))
                    sb.AppendLine(line);

                LilypondText = sb.ToString();

                LoadLilypond(sb.ToString());
            }
            else
            {
                throw new NotSupportedException($"File extension {Path.GetExtension(fileName)} is not supported.");
            }
        }

        public void LoadLilypond(string content)
        {
            LilypondText = content;
            content = content.Trim().ToLower().Replace("\r\n", " ").Replace("\n", " ").Replace("  ", " ");
            var tokens = GetTokensFromLilypond(content);
            //WPFStaffs.Clear();
            //string message;
            //WPFStaffs.AddRange(GetStaffsFromTokens(tokens, out message));
            //WPFStaffsChanged?.Invoke(this, new WPFStaffsEventArgs { Symbols = WPFStaffs, Message = message });

            //MidiSequence = GetSequenceFromWPFStaffs();
            //MidiSequenceChanged?.Invoke(this, new MidiSequenceEventArgs { MidiSequence = MidiSequence });
        }

        public void LoadMidi(Sequence sequence)
        {
            var lilypondContent = new StringBuilder();
            lilypondContent.AppendLine("\\relative c' {");
            lilypondContent.AppendLine("\\clef treble");

            var division = sequence.Division;
            var previousMidiKey = 60; // Central C;
            var previousNoteAbsoluteTicks = 0;
            double percentageOfBarReached = 0;
            var startedNoteIsClosed = true;

            for (var i = 0; i < sequence.Count(); i++)
            {
                var track = sequence[i];

                foreach (var midiEvent in track.Iterator())
                {
                    var midiMessage = midiEvent.MidiMessage;
                    switch (midiMessage.MessageType)
                    {
                        case MessageType.Meta:
                            var metaMessage = midiMessage as MetaMessage;
                            switch (metaMessage.MetaType)
                            {
                                case MetaType.TimeSignature:
                                    var timeSignatureBytes = metaMessage.GetBytes();
                                    _beatNote = timeSignatureBytes[0];
                                    _beatsPerBar = (int)(1 / Math.Pow(timeSignatureBytes[1], -2));
                                    lilypondContent.AppendLine($"\\time {_beatNote}/{_beatsPerBar}");
                                    break;
                                case MetaType.Tempo:
                                    var tempoBytes = metaMessage.GetBytes();
                                    var tempo = ((tempoBytes[0] & 0xff) << 16) | ((tempoBytes[1] & 0xff) << 8) |
                                                (tempoBytes[2] & 0xff);
                                    _bpm = 60000000 / tempo;
                                    lilypondContent.AppendLine($"\\tempo 4={_bpm}");
                                    break;
                                case MetaType.EndOfTrack:
                                    if (previousNoteAbsoluteTicks > 0)
                                    {
                                        // Finish the last notelength.
                                        double percentageOfBar;
                                        lilypondContent.Append(GetNoteLength(previousNoteAbsoluteTicks,
                                            midiEvent.AbsoluteTicks, division, _beatNote, _beatsPerBar,
                                            out percentageOfBar));
                                        lilypondContent.Append(" ");

                                        percentageOfBarReached += percentageOfBar;
                                        if (percentageOfBarReached >= 1)
                                        {
                                            lilypondContent.AppendLine("|");
                                            percentageOfBar = percentageOfBar - 1;
                                        }
                                    }
                                    break;
                                default: break;
                            }
                            break;
                        case MessageType.Channel:
                            var channelMessage = midiEvent.MidiMessage as ChannelMessage;
                            if (channelMessage.Command == ChannelCommand.NoteOn)
                                if (channelMessage.Data2 > 0) // Data2 = loudness
                                {
                                    // Append the new note.
                                    lilypondContent.Append(GetNoteName(previousMidiKey, channelMessage.Data1));

                                    previousMidiKey = channelMessage.Data1;
                                    startedNoteIsClosed = false;
                                }
                                else if (!startedNoteIsClosed)
                                {
                                    // Finish the previous note with the length.
                                    double percentageOfBar;
                                    lilypondContent.Append(GetNoteLength(previousNoteAbsoluteTicks,
                                        midiEvent.AbsoluteTicks, division, _beatNote, _beatsPerBar,
                                        out percentageOfBar));
                                    previousNoteAbsoluteTicks = midiEvent.AbsoluteTicks;
                                    lilypondContent.Append(" ");

                                    percentageOfBarReached += percentageOfBar;
                                    if (percentageOfBarReached >= 1)
                                    {
                                        lilypondContent.AppendLine("|");
                                        percentageOfBarReached -= 1;
                                    }
                                    startedNoteIsClosed = true;
                                }
                                else
                                {
                                    lilypondContent.Append("r");
                                }
                            break;
                    }
                }
            }

            lilypondContent.Append("}");

            LoadLilypond(lilypondContent.ToString());
        }

        private string GetNoteLength(int absoluteTicks, int nextNoteAbsoluteTicks, int division, int beatNote,
            int beatsPerBar, out double percentageOfBar)
        {
            var duration = 0;
            var dots = 0;

            double deltaTicks = nextNoteAbsoluteTicks - absoluteTicks;

            if (deltaTicks <= 0)
            {
                percentageOfBar = 0;
                return string.Empty;
            }

            var percentageOfBeatNote = deltaTicks / division;
            percentageOfBar = 1.0 / beatsPerBar * percentageOfBeatNote;

            for (var noteLength = 32; noteLength >= 1; noteLength -= 1)
            {
                var absoluteNoteLength = 1.0 / noteLength;

                if (percentageOfBar <= absoluteNoteLength)
                {
                    if (noteLength < 2)
                        noteLength = 2;

                    int subtractDuration;

                    if (noteLength == 32)
                        subtractDuration = 32;
                    else if (noteLength >= 16)
                        subtractDuration = 16;
                    else if (noteLength >= 8)
                        subtractDuration = 8;
                    else if (noteLength >= 4)
                        subtractDuration = 4;
                    else
                        subtractDuration = 2;

                    if (noteLength >= 17)
                        duration = 32;
                    else if (noteLength >= 9)
                        duration = 16;
                    else if (noteLength >= 5)
                        duration = 8;
                    else if (noteLength >= 3)
                        duration = 4;
                    else
                        duration = 2;

                    double currentTime = 0;

                    while (currentTime < noteLength - subtractDuration)
                    {
                        var addtime = 1 / (subtractDuration / beatNote * Math.Pow(2, dots));
                        if (addtime <= 0) break;
                        currentTime += addtime;
                        if (currentTime <= noteLength - subtractDuration)
                            dots++;
                        if (dots >= 4) break;
                    }

                    break;
                }
            }

            return duration + new string('.', dots);
        }

        private static string GetNoteName(int previousMidiKey, int midiKey)
        {
            var octave = midiKey / 12 - 1;
            var name = "";
            switch (midiKey % 12)
            {
                case 0:
                    name = "c";
                    break;
                case 1:
                    name = "cis";
                    break;
                case 2:
                    name = "d";
                    break;
                case 3:
                    name = "dis";
                    break;
                case 4:
                    name = "e";
                    break;
                case 5:
                    name = "f";
                    break;
                case 6:
                    name = "fis";
                    break;
                case 7:
                    name = "g";
                    break;
                case 8:
                    name = "gis";
                    break;
                case 9:
                    name = "a";
                    break;
                case 10:
                    name = "ais";
                    break;
                case 11:
                    name = "b";
                    break;
            }

            var distance = midiKey - previousMidiKey;
            while (distance < -6)
            {
                name += ",";
                distance += 8;
            }

            while (distance > 6)
            {
                name += "'";
                distance -= 8;
            }

            return name;
        }

        #region Staffs loading

        private static IEnumerable<MusicalSymbol> GetStaffsFromTokens(LinkedList<LilypondToken> tokens,
            out string message)
        {
            var symbols = new List<MusicalSymbol>();
            message = "";

            try
            {
                Clef currentClef = null;
                var previousOctave = 4;
                var previousNote = 'c';

                var currentToken = tokens.First();
                while (currentToken != null)
                {
                    switch (currentToken.TokenKind)
                    {
                        case LilypondTokenKind.Unknown:
                            break;
                        case LilypondTokenKind.Note:
                            // Length
                            var noteLength = int.Parse(Regex.Match(currentToken.Value, @"\d+").Value);
                            // Crosses and Moles
                            var alter = 0;
                            alter += Regex.Matches(currentToken.Value, "is").Count;
                            alter -= Regex.Matches(currentToken.Value, "es|as").Count;
                            // Octaves
                            var distanceWithPreviousNote =
                                notesorder.IndexOf(currentToken.Value[0]) - notesorder.IndexOf(previousNote);
                            if (distanceWithPreviousNote > 3) // Shorter path possible the other way around
                                distanceWithPreviousNote -= 7; // The number of notes in an octave
                            else if (distanceWithPreviousNote < -3)
                                distanceWithPreviousNote += 7; // The number of notes in an octave

                            if (distanceWithPreviousNote + notesorder.IndexOf(previousNote) >= 7)
                                previousOctave++;
                            else if (distanceWithPreviousNote + notesorder.IndexOf(previousNote) < 0)
                                previousOctave--;

                            // Force up or down.
                            previousOctave += currentToken.Value.Count(c => c == '\'');
                            previousOctave -= currentToken.Value.Count(c => c == ',');

                            previousNote = currentToken.Value[0];

                            var note = new Note(currentToken.Value[0].ToString().ToUpper(), alter, previousOctave,
                                (MusicalSymbolDuration)noteLength, NoteStemDirection.Up, NoteTieType.None,
                                new List<NoteBeamType> { NoteBeamType.Single });
                            note.NumberOfDots += currentToken.Value.Count(c => c.Equals('.'));

                            symbols.Add(note);
                            break;
                        case LilypondTokenKind.Rest:
                            var restLength = int.Parse(currentToken.Value[1].ToString());
                            symbols.Add(new Rest((MusicalSymbolDuration)restLength));
                            break;
                        case LilypondTokenKind.Bar:
                            symbols.Add(new Barline());
                            break;
                        case LilypondTokenKind.Clef:
                            currentToken = currentToken.NextToken;
                            if (currentToken.Value == "treble")
                                currentClef = new Clef(ClefType.GClef, 2);
                            else if (currentToken.Value == "bass")
                                currentClef = new Clef(ClefType.FClef, 4);
                            else
                                throw new NotSupportedException($"Clef {currentToken.Value} is not supported.");

                            symbols.Add(currentClef);
                            break;
                        case LilypondTokenKind.Time:
                            currentToken = currentToken.NextToken;
                            var times = currentToken.Value.Split('/');
                            symbols.Add(new TimeSignature(TimeSignatureType.Numbers, uint.Parse(times[0]),
                                uint.Parse(times[1])));
                            break;
                        case LilypondTokenKind.Tempo:
                            // Tempo not supported
                            break;
                        default:
                            break;
                    }
                    currentToken = currentToken.NextToken;
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            return symbols;
        }

        private static LinkedList<LilypondToken> GetTokensFromLilypond(string content)
        {
            var tokens = new LinkedList<LilypondToken>();

            foreach (var s in content.Split(' '))
            {
                var token = new LilypondToken
                {
                    Value = s
                };

                switch (s)
                {
                    case "\\relative":
                        token.TokenKind = LilypondTokenKind.Staff;
                        break;
                    case "\\clef":
                        token.TokenKind = LilypondTokenKind.Clef;
                        break;
                    case "\\time":
                        token.TokenKind = LilypondTokenKind.Time;
                        break;
                    case "\\tempo":
                        token.TokenKind = LilypondTokenKind.Tempo;
                        break;
                    case "|":
                        token.TokenKind = LilypondTokenKind.Bar;
                        break;
                    default:
                        token.TokenKind = LilypondTokenKind.Unknown;
                        break;
                }

                token.Value = s;

                if (token.TokenKind == LilypondTokenKind.Unknown && new Regex(@"[a-g][,'eis]*[0-9]+[.]*").IsMatch(s))
                    token.TokenKind = LilypondTokenKind.Note;
                else if (token.TokenKind == LilypondTokenKind.Unknown && new Regex(@"r.*?[0-9][.]*").IsMatch(s))
                    token.TokenKind = LilypondTokenKind.Rest;

                if (tokens.Last != null)
                {
                    tokens.Last.Value.NextToken = token;
                    token.PreviousToken = tokens.Last.Value;
                }

                tokens.AddLast(token);
            }

            return tokens;
        }

        #endregion Staffs loading

        #region Saving to files

        internal void SaveToMidi(string fileName)
        {
            var sequence = GetSequenceFromWPFStaffs();

            sequence.Save(fileName);
        }

        private Sequence GetSequenceFromWPFStaffs()
        {
            var notesOrderWithCrosses =
                new List<string> {"c", "cis", "d", "dis", "e", "f", "fis", "g", "gis", "a", "ais", "b"};
            var absoluteTicks = 0;

            var sequence = new Sequence();

            var metaTrack = new Track();
            sequence.Add(metaTrack);

            // Calculate tempo
            var speed = 60000000 / _bpm;
            var tempo = new byte[3];
            tempo[0] = (byte) ((speed >> 16) & 0xff);
            tempo[1] = (byte) ((speed >> 8) & 0xff);
            tempo[2] = (byte) (speed & 0xff);
            metaTrack.Insert(0 /* Insert at 0 ticks*/, new MetaMessage(MetaType.Tempo, tempo));

            var notesTrack = new Track();
            sequence.Add(notesTrack);

            foreach (var musicalSymbol in WPFStaffs)
                switch (musicalSymbol.Type)
                {
                    case MusicalSymbolType.Note:
                        var note = musicalSymbol as Note;

                        // Calculate duration
                        var absoluteLength = 1.0 / (double) note.Duration;
                        absoluteLength += absoluteLength / 2.0 * note.NumberOfDots;

                        var relationToQuartNote = _beatNote / 4.0;
                        var percentageOfBeatNote = 1.0 / _beatNote / absoluteLength;
                        var deltaTicks = sequence.Division / relationToQuartNote / percentageOfBeatNote;

                        // Calculate height
                        var noteHeight = notesOrderWithCrosses.IndexOf(note.Step.ToLower()) + (note.Octave + 1) * 12;
                        noteHeight += note.Alter;
                        notesTrack.Insert(absoluteTicks,
                            new ChannelMessage(ChannelCommand.NoteOn, 1, noteHeight, 90)); // Data2 = volume

                        absoluteTicks += (int) deltaTicks;
                        notesTrack.Insert(absoluteTicks,
                            new ChannelMessage(ChannelCommand.NoteOn, 1, noteHeight, 0)); // Data2 = volume

                        break;
                    case MusicalSymbolType.TimeSignature:
                        var timeSignature = new byte[4];
                        timeSignature[0] = (byte) _beatsPerBar;
                        timeSignature[1] = (byte) (Math.Log(_beatNote) / Math.Log(2));
                        metaTrack.Insert(absoluteTicks, new MetaMessage(MetaType.TimeSignature, timeSignature));
                        break;
                    default:
                        break;
                }

            notesTrack.Insert(absoluteTicks, MetaMessage.EndOfTrackMessage);
            metaTrack.Insert(absoluteTicks, MetaMessage.EndOfTrackMessage);
            return sequence;
        }

        internal void SaveToPDF(string fileName)
        {
            var withoutExtension = Path.GetFileNameWithoutExtension(fileName);
            var tmpFileName = $"{fileName}-tmp.ly";
            SaveToLilypond(tmpFileName);

            var lilypondLocation = @"C:\Program Files (x86)\LilyPond\usr\bin\lilypond.exe";
            var sourceFolder = Path.GetDirectoryName(tmpFileName);
            var sourceFileName = Path.GetFileNameWithoutExtension(tmpFileName);
            var targetFolder = Path.GetDirectoryName(fileName);
            var targetFileName = Path.GetFileNameWithoutExtension(fileName);

            var process = new Process
            {
                StartInfo =
                {
                    WorkingDirectory = sourceFolder,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    Arguments = string.Format("--pdf \"{0}\\{1}.ly\"", sourceFolder, sourceFileName),
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
        }

        internal void SaveToLilypond(string fileName)
        {
            using (var outputFile = new StreamWriter(fileName))
            {
                outputFile.Write(LilypondText);
                outputFile.Close();
            }
        }

        #endregion Saving to files
    }
}