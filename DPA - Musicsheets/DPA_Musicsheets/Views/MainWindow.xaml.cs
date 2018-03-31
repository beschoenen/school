using System;
using System.ComponentModel;
using System.Windows;
using DPA_Musicsheets.ViewModels;

namespace DPA_Musicsheets.Views
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            if (!((MainViewModel) DataContext).Close())
            {
                e.Cancel = true;
            }
        }
    }
}