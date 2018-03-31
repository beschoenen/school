﻿using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using DPA_Musicsheets.Refactor;
using DPA_Musicsheets.Refactor.EventHandler;
using DPA_Musicsheets.Refactor.Events;
using DPA_Musicsheets.Refactor.Models;
using DPA_Musicsheets.Refactor.Models.Block;
using DPA_Musicsheets.Refactor.MusicConverters.Lilypond;
using DPA_Musicsheets.Refactor.MusicLoaders.Lilypond;
using DPA_Musicsheets.Refactor.Commands.Editor;
using DPA_Musicsheets.Refactor.Commands.Dialog;
using DPA_Musicsheets.Refactor.MusicSavers.Lilypond;
using DPA_Musicsheets.Refactor.EditorMementos;
using DPA_Musicsheets.Refactor.EditorStates;

namespace DPA_Musicsheets.ViewModels
{
    public class LilypondViewModel : ViewModelBase
    {
        private static readonly int MILLISECONDS_BEFORE_CHANGE_HANDLED = 1500;

        public BaseEditorState State { get; private set; }

        private readonly SubscriptionToken _playStateChangedSubscriptionToken;
        private readonly SubscriptionToken _pieceLoadedSubscriptionToken;
        private readonly SubscriptionToken _pieceChangedSubscriptionToken;

        private DateTime _lastChange;
        private readonly LilypondHistory _history;

        private TextBox _textBox;
        private Piece _piece;
        private string _text;

        private bool _fromLoad;

        public string LilypondText
        {
            get => _text;
            set
            {
                _text = value;
                RaisePropertyChanged(nameof(LilypondText));
            }
        }

        public LilypondViewModel()
        {
            _history = new LilypondHistory();
            _text = "Your lilypond text will appear here.";

            State = new EditorEnabledState(this);

            _pieceLoadedSubscriptionToken = EventBus.Subscribe(new Action<PieceLoadedEvent>(PieceLoadedEventHandler));
            _playStateChangedSubscriptionToken = EventBus.Subscribe(new Action<PlayStateChangedEvent>(PlayStateChangedEventHandler));
            _pieceChangedSubscriptionToken = EventBus.Subscribe(new Action<PieceChangedEvent>(PieceChangedEventHandler));

            HotkeyChain.Instance.RegisterHotkey(new[] { Key.LeftCtrl, Key.S }, new SaveFile(new LilypondSaver().Filter));
            HotkeyChain.Instance.RegisterHotkey(new[] { Key.LeftAlt, Key.C, Key.B, Key.N }, new InsertTreble(true, "bass"));
            HotkeyChain.Instance.RegisterHotkey(new[] { Key.LeftAlt, Key.C, Key.B }, new InsertTreble(false, "bass"));
            HotkeyChain.Instance.RegisterHotkey(new[] { Key.LeftAlt, Key.C, Key.N }, new InsertTreble(true, "treble"));
            HotkeyChain.Instance.RegisterHotkey(new[] { Key.LeftAlt, Key.C }, new InsertTreble(false, "treble"));
            HotkeyChain.Instance.RegisterHotkey(new[] { Key.LeftAlt, Key.S, Key.N }, new InsertTempo(true));
            HotkeyChain.Instance.RegisterHotkey(new[] { Key.LeftAlt, Key.S }, new InsertTempo(false));
            HotkeyChain.Instance.RegisterHotkey(new[] { Key.LeftAlt, Key.B, Key.A }, new FixBars(true));
            HotkeyChain.Instance.RegisterHotkey(new[] { Key.LeftAlt, Key.B }, new FixBars(false));
            HotkeyChain.Instance.RegisterHotkey(new[] { Key.LeftAlt, Key.T, Key.D6 }, new InsertTime(6, 8));
            HotkeyChain.Instance.RegisterHotkey(new[] { Key.LeftAlt, Key.T, Key.D3 }, new InsertTime(3, 4));
            HotkeyChain.Instance.RegisterHotkey(new[] { Key.LeftAlt, Key.T, Key.D4 }, new InsertTime(4, 4));
            HotkeyChain.Instance.RegisterHotkey(new[] { Key.LeftAlt, Key.T }, new InsertTime(4, 4));
            HotkeyChain.Instance.RegisterHotkey(new[] { Key.LeftAlt, Key.R }, new InsertRepeat());
            HotkeyChain.Instance.RegisterHotkey(new[] { Key.LeftAlt, Key.A }, new InsertAlternative());
        }

        public ICommand SelectionChangedCommand => new RelayCommand<RoutedEventArgs>(args =>
        {
            _textBox = (TextBox) args.OriginalSource;

            AbstractEditorCommand.SelectionStart = _textBox.SelectionStart;
            AbstractEditorCommand.Offset = _textBox.SelectionLength + _textBox.SelectionStart;
            AbstractEditorCommand.SelectionLength = _textBox.SelectionLength;
        });

        public ICommand TextChangedCommand => new RelayCommand<TextChangedEventArgs>(args =>
        {
            if (_fromLoad)
            {
                _fromLoad = false;
                return;
            }

            _lastChange = DateTime.Now;

            Task.Delay(MILLISECONDS_BEFORE_CHANGE_HANDLED).ContinueWith(task =>
            {
                if (!((DateTime.Now - _lastChange).TotalMilliseconds >= MILLISECONDS_BEFORE_CHANGE_HANDLED)) return;

                try
                {
                    var piece = new LilypondLoader().LoadLilypond(LilypondText);

                    _history.Add(LilypondText);
                    EventBus.Fire(new PieceChangedEvent(piece));
                }
                catch(Exception) {}

            }, TaskScheduler.FromCurrentSynchronizationContext()); // Request from main thread.
        });

        private void PlayStateChangedEventHandler(PlayStateChangedEvent playingChangedEvent)
        {
            StateChanged(playingChangedEvent.Payload);
        }

        private void PieceLoadedEventHandler(PieceLoadedEvent pieceLoaded)
        {
            StateChanged(Enums.PlayState.Stop);

            _fromLoad = true;
            _history.Clear();

            _piece = pieceLoaded.Payload;
            LilypondText = new LilypondConverter().Convert(_piece);
            _history.Add(LilypondText);
        }

        private void PieceChangedEventHandler(PieceChangedEvent pieceChanged)
        {
            _piece = pieceChanged.Payload;
            LilypondText = new LilypondConverter().Convert(_piece);
            _history.Add(LilypondText);
        }

        public RelayCommand UndoCommand => new RelayCommand(() =>
        {
            State.SetLilypondText(_history.Undo());
            UndoCommand.RaiseCanExecuteChanged();
        }, () => _history.CanUndo && !State.ReadOnly);

        public RelayCommand RedoCommand => new RelayCommand(() =>
        {
            State.SetLilypondText(_history.Redo());
            RedoCommand.RaiseCanExecuteChanged();
        }, () => _history.CanRedo && !State.ReadOnly);

        public ICommand SaveAsCommand => new RelayCommand(new SaveFile(new LilypondSaver().Filter).execute, () => _piece != null);

        private void StateChanged(Enums.PlayState playState)
        {
            if (playState == Enums.PlayState.Play)
            {
                State = new EditorDisabledState(this);
            }
            else
            {
                State = new EditorEnabledState(this);
            }

            RaisePropertyChanged(nameof(State));
        }
    }
}