using System;
using System.Windows;

namespace prog5_ninja.View
{
    public partial class ShopWindow : Window
    {
        private readonly NinjaWindow _ninjaWindow;

        public ShopWindow()
        {
            InitializeComponent();

            _ninjaWindow = new NinjaWindow();

            ContentRendered += ShopWindow_ContentRendered;
            LocationChanged += ShopWindow_LocationChanged;
            Closed += ShopWindow_Closed;
            Closing += ShopWindow_Closing;
        }

        private void ShopWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!Database.Instance.Context.ChangeTracker.HasChanges()) return;

            var result = MessageBox.Show("There are pending changes, do you want to save them?", "Pending Changes", MessageBoxButton.YesNoCancel, MessageBoxImage.Exclamation);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    Database.Instance.Context.SaveChanges();
                    break;
                case MessageBoxResult.Cancel:
                case MessageBoxResult.None:
                    e.Cancel = true;
                    break;
            }
        }

        private void ShopWindow_Closed(object sender, EventArgs e)
        {
             _ninjaWindow.Close();
        }

        private void ShopWindow_LocationChanged(object sender, EventArgs e)
        {
            SyncPosition();
        }

        private void ShopWindow_ContentRendered(object sender, EventArgs e)
        {
            SyncPosition();
            _ninjaWindow.Show();
        }

        private void SyncPosition()
        {
            _ninjaWindow.Left = Left + Width + 5;
            _ninjaWindow.Top = Top;
        }
        
    }
}
