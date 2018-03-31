using DPA_Musicsheets.Refactor.Commands;
using DPA_Musicsheets.Refactor.Commands.Editor;
using DPA_Musicsheets.Refactor.Models.Block;
using DPA_Musicsheets.Refactor.MusicConverters.Lilypond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace DPA_Musicsheets.Refactor
{
    public class HotkeyChain
    {
        private static HotkeyChain _hotkeyChain;
        public static HotkeyChain Instance => _hotkeyChain ?? (_hotkeyChain = new HotkeyChain());

        private List<Hotkey> _hotkeys = new List<Hotkey>();

        private struct Hotkey
        {
            public List<Key> Keys;
            public Command Command;
        }

        public Piece Piece { get; set; }
        public string LilypondText { get; set; }

        public bool RegisterHotkey(IEnumerable<Key> keys, Command command)
        {
            if (_hotkeys.Any(hotkey => hotkey.Keys.All(keys.Contains) && hotkey.Keys.Count == keys.Count())) return false;

            _hotkeys.Add(new Hotkey { Keys = keys.ToList(), Command = command });
            _hotkeys = _hotkeys.OrderByDescending(e => e.Keys.Count).ToList();

            return true;
        }

        public bool Handle(IEnumerable<Key> keys, Piece piece)
        {
            foreach (var hotkey in _hotkeys)
            {
                var isHotkey = hotkey.Keys.All(keys.ToList().Contains);

                if (!isHotkey) continue;

                LilypondConverter lilypondConverter = new LilypondConverter();
                LilypondText = lilypondConverter.Convert(piece);

                hotkey.Command.Piece = piece;
                hotkey.Command.LilypondText = LilypondText;
                hotkey.Command.execute();
                Piece = hotkey.Command.Piece;
                LilypondText = hotkey.Command.LilypondText;

                return true;
            }

            return false;
        }
    }
}
