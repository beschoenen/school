using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using SOh_ParkInspect.Enum;
using SOh_ParkInspect.Helper;
using SOh_ParkInspect.Model;
using SOh_ParkInspect.View;

namespace SOh_ParkInspect.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly RouterService _router;

        public UserControl CurrentView { get; private set; }
        public string CurrentUserName { get; set; }

        public RelayCommand<ControlType> ChangeViewCommand { get; set; }
        public ICommand UploadDataCommand { get; set; }
        public ICommand BackCommand { get; set; }
        public ICommand ExitCommand { get; set; }

        public MainViewModel(RouterService router)
        {
            _router = router;
            _router.PropertyChanged += RouterViewChanged;

            new LoginWindow().ShowDialog();

            if (!Settings.IsLoggedIn) Environment.Exit(0);

            CurrentUserName = Settings.CurrentUser.Email;

            _router.GoTo(ControlType.AccountControl);

            UploadDataCommand = new RelayCommand(UploadData, () => DbSync.CanUpload);
            ChangeViewCommand = new RelayCommand<ControlType>(ChangeView);
            ExitCommand = new RelayCommand(() => Environment.Exit(0));
            BackCommand = new RelayCommand(GoBack);
        }

        private void UploadData()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            DbSync.Upload();

            Mouse.OverrideCursor = null;

            MessageBox.Show("Uw data is geüpload!");
        }

        private void GoBack()
        {
            if (_router.IsAtRoot()) return;
            _router.GoBack();
        }

        private void ChangeView(ControlType view)
        {
            _router.GoTo(view, null, true);
        }

        private void RouterViewChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != "CurrentView") return;

            CurrentView = _router.CurrentView;
            RaisePropertyChanged(nameof(CurrentView));
        }
    }
}
