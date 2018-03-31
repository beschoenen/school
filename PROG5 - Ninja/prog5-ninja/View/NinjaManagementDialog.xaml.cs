using System.Windows;

namespace prog5_ninja.View
{
    public partial class NinjaManagementDialog : Window
    {
        public NinjaManagementDialog()
        {
            InitializeComponent();

            Owner = Application.Current.MainWindow;
        }
    }
}
