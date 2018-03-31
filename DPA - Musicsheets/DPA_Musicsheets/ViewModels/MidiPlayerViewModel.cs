﻿using System;
using DPA_Musicsheets.Refactor;
using DPA_Musicsheets.Refactor.EventHandler;
using DPA_Musicsheets.Refactor.Events;
 using DPA_Musicsheets.Refactor.Models.Block;
 using DPA_Musicsheets.Refactor.MusicConverters.Midi;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Sanford.Multimedia.Midi;

namespace DPA_Musicsheets.ViewModels
{
    public class MidiPlayerViewModel : ViewModelBase
    {
        private readonly SubscriptionToken _pieceLoadedSubscriptionToken;
        private readonly SubscriptionToken _pieceChangedSubscriptionToken;

        private readonly OutputDevice _outputDevice;

        // De sequencer maakt het mogelijk om een sequence af te spelen.
        // Deze heeft een timer en geeft events op de juiste momenten.
        private readonly Sequencer _sequencer;

        private bool _running;

        public MidiPlayerViewModel()
        {
            // De OutputDevice is een midi device of het midikanaal van je PC.
            // Hierop gaan we audio streamen.
            // DeviceID 0 is je audio van je PC zelf.
            _outputDevice = new OutputDevice(0);
            _sequencer = new Sequencer();

            // Wanneer een channelmessage langskomt sturen we deze direct door naar onze audio.
            // Channelmessages zijn tonen met commands als NoteOn en NoteOff
            // In midi wordt elke noot gespeeld totdat NoteOff is benoemd. Wanneer dus nooit een NoteOff komt nadat die een NoteOn heeft gehad
            // zal deze note dus oneindig lang blijven spelen.
            _sequencer.ChannelMessagePlayed += ChannelMessagePlayed;

            // Wanneer de sequence klaar is moeten we alles closen en stoppen.
            _sequencer.PlayingCompleted += (playingSender, playingEvent) => StopCommand.Execute(null);

            _pieceChangedSubscriptionToken = EventBus.Subscribe(new Action<PieceChangedEvent>(PieceChangedEventHandler));
            _pieceLoadedSubscriptionToken = EventBus.Subscribe(new Action<PieceLoadedEvent>(PieceLoadedEventHandler));
        }

        private void PieceLoadedEventHandler(PieceLoadedEvent pieceLoadedEvent) => UpdatePiece(pieceLoadedEvent.Payload);

        private void PieceChangedEventHandler(PieceChangedEvent pieceChangedEvent) => UpdatePiece(pieceChangedEvent.Payload);

        private void UpdatePiece(Piece piece)
        {
            StopCommand.Execute(null);

            _sequencer.Sequence = new MidiConverter(false).Convert(piece);
            UpdateButtons();
        }

        public RelayCommand PlayCommand => new RelayCommand(() =>
        {
            if (_running) return;

            _running = true;
            _sequencer.Continue();
            UpdateButtons();

            EventBus.Fire(new PlayStateChangedEvent(Enums.PlayState.Play));
        }, () => !_running && _sequencer.Sequence != null);

        public RelayCommand StopCommand => new RelayCommand(() =>
        {
            _running = false;
            _sequencer.Stop();
            _sequencer.Position = 0;
            UpdateButtons();

            EventBus.Fire(new PlayStateChangedEvent(Enums.PlayState.Stop));
        }, () => _running);

        public RelayCommand PauseCommand => new RelayCommand(() =>
        {
            _running = false;
            _sequencer.Stop();
            UpdateButtons();

            EventBus.Fire(new PlayStateChangedEvent(Enums.PlayState.Pause));
        }, () => _running);

        private void UpdateButtons()
        {
            PlayCommand.RaiseCanExecuteChanged();
            PauseCommand.RaiseCanExecuteChanged();
            StopCommand.RaiseCanExecuteChanged();
        }

        private void ChannelMessagePlayed(object sender, ChannelMessageEventArgs e)
        {
            try
            {
                _outputDevice.Send(e.Message);
            }
            catch (Exception ex) when (ex is ObjectDisposedException || ex is OutputDeviceException)
            {
                // Don't crash when we can't play
                // We have to do it this way because IsDisposed on
                // _outDevice may be false when it is being disposed
                // so this is the only safe way to prevent race conditions
            }
        }

        public override void Cleanup()
        {
            base.Cleanup();

            _sequencer.Stop();
            _sequencer.Dispose();
            _outputDevice.Dispose();
        }
    }
}
