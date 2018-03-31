using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using DPA_Musicsheets.Refactor;
using DPA_Musicsheets.Refactor.EventHandler;
using DPA_Musicsheets.Refactor.Events;
using DPA_Musicsheets.Refactor.Models.Block;
using DPA_Musicsheets.Refactor.Commands.Dialog;

namespace DPA_Musicsheets.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly List<Key> _pressedKeys;

        private string _currentState;
        //private AbstractMusicLoader _loader;
        private Piece _piece;
        private bool _hasUnsavedWork;

        private readonly SubscriptionToken _pieceSavedSubscriptionToken;
        private readonly SubscriptionToken _pieceChangedSubscriptionToken;

        //public string FileName => _loader?.FilePath ?? string.Empty;
        public string FileName => AbstractDialogCommand.Loader?.FilePath ?? string.Empty;

        public string CurrentState
        {
            get => _currentState;
            set
            {
                _currentState = value;
                RaisePropertyChanged(() => CurrentState);
            }
        }

        public MainViewModel()
        {
            _pressedKeys = new List<Key>();

            _pieceChangedSubscriptionToken = EventBus.Subscribe(new Action<PieceChangedEvent>(PieceChangedEventHandler));
            _pieceSavedSubscriptionToken = EventBus.Subscribe(new Action<PieceSavedEvent>(PieceSavedEventHandler));

            HotkeyChain.Instance.RegisterHotkey(new[] { Key.LeftCtrl, Key.O }, new OpenFile());
            HotkeyChain.Instance.RegisterHotkey(new[] { Key.LeftCtrl, Key.L }, new LoadFile());
        }

        private void PieceSavedEventHandler(PieceSavedEvent obj)
        {
            _hasUnsavedWork = false;
        }

        private void PieceChangedEventHandler(PieceChangedEvent pieceChangedEvent)
        {
            _piece = pieceChangedEvent.Payload;
            _hasUnsavedWork = true;
        }

        private void OpenFile()
        {
            OpenFile openFileCommand = new OpenFile();
            openFileCommand.execute();
            RaisePropertyChanged(nameof(FileName));
        }

        private void LoadFile()
        {
            LoadFile loadFileCommand = new LoadFile();
            loadFileCommand.execute();
            _piece = loadFileCommand.Piece;
        }

        public ICommand OpenFileCommand => new RelayCommand(OpenFile);

        public ICommand LoadCommand => new RelayCommand(LoadFile);

        public ICommand OnLostFocusCommand => new RelayCommand(() =>
        {
            Console.WriteLine(@"Lost focus");
        });

        public ICommand OnKeyDownCommand => new RelayCommand<KeyEventArgs>(e =>
        {
            _pressedKeys.Add(e.Key == Key.System ? e.SystemKey : e.Key);
        });

        public ICommand OnKeyUpCommand => new RelayCommand<KeyEventArgs>(e =>
        {
            if (HotkeyChain.Instance.Handle(_pressedKeys, _piece))
            {
                e.Handled = true;
                _pressedKeys.Clear();
                _piece = HotkeyChain.Instance.Piece;

                RaisePropertyChanged(nameof(FileName));

            }

            _pressedKeys.RemoveAll(x => x == (e.Key == Key.System ? e.SystemKey : e.Key));
        });

        public ICommand OnWindowClosingCommand => new RelayCommand(ViewModelLocator.Cleanup);

        public bool Close()
        {
            if (!_hasUnsavedWork) return true;

            var result = MessageBox.Show("You have unsaved work, do you want to save?", "Warning", MessageBoxButton.YesNoCancel);

            if (result == MessageBoxResult.Yes)
            {
                var message = FileHelper.Instance.WriteFile(_piece);
                if (message != null)
                {
                    MessageBox.Show(message);
                }

                return true;
            }

            return result == MessageBoxResult.No;
        }
    }
}
