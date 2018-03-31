using System.Windows;

namespace prog5_ninja.View
{
    public partial class NinjaWindow : Window
    {
        public NinjaWindow()
        {
            InitializeComponent();

            Closing += (sender, e) =>
            {
                if (Application.Current.MainWindow?.IsVisible ?? false)
                {
                    e.Cancel = true;
                }
            };
        }
    }
}
