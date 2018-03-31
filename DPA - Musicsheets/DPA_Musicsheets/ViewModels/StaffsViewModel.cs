﻿using System;
using System.Collections.ObjectModel;
using DPA_Musicsheets.Managers;
using DPA_Musicsheets.Messages;
using DPA_Musicsheets.Refactor.EventHandler;
using DPA_Musicsheets.Refactor.Events;
using GalaSoft.MvvmLight;
using PSAMControlLibrary;
using DPA_Musicsheets.Refactor.MusicConverters;
using System.Collections.Generic;
 using System.Windows;
 using DPA_Musicsheets.Refactor;
 using DPA_Musicsheets.Refactor.MusicConverters.Sheetmusic;
 using Key = System.Windows.Input.Key;
using DPA_Musicsheets.Refactor.Models.Block;
 using DPA_Musicsheets.Refactor.MusicSavers.Sheetmusic;
using DPA_Musicsheets.Refactor.Commands;
using DPA_Musicsheets.Refactor.Commands.Dialog;

namespace DPA_Musicsheets.ViewModels
{
    public class StaffsViewModel : ViewModelBase
    {
        private Piece _piece;

        private readonly SubscriptionToken _pieceLoadedSubscriptionToken;
        private readonly SubscriptionToken _pieceChangedSubscriptionToken;

        public ObservableCollection<MusicalSymbol> Staffs { get; set; }

        public StaffsViewModel()
        {
            Staffs = new ObservableCollection<MusicalSymbol>();

            _pieceChangedSubscriptionToken = EventBus.Subscribe(new Action<PieceChangedEvent>(PieceChangedEventHandler));
            _pieceLoadedSubscriptionToken = EventBus.Subscribe(new Action<PieceLoadedEvent>(PieceLoadedEventHandler));

            HotkeyChain.Instance.RegisterHotkey(new[] {Key.LeftCtrl, Key.S, Key.P}, new SaveFile(new SheetMusicSaver().Filter));
        }

        private void PieceLoadedEventHandler(PieceLoadedEvent pieceLoadedEvent) => UpdatePiece(pieceLoadedEvent.Payload);

        private void PieceChangedEventHandler(PieceChangedEvent pieceChangedEvent) => UpdatePiece(pieceChangedEvent.Payload);

        private void UpdatePiece(Piece piece)
        {
            _piece = piece;
            var symbols = new SheetMusicConverter().Convert(_piece);

            Staffs.Clear();
            symbols.ForEach(Staffs.Add);
        }

        private void SaveFile()
        {
            if (_piece == null) return;

            var message = FileHelper.Instance.WriteFile(_piece, new SheetMusicSaver().Filter);
            if (message != null)
            {
                MessageBox.Show(message);
            }
        }
    }
}