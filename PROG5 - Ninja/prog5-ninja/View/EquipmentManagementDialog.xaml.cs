using System.Windows;

namespace prog5_ninja.View
{
    public partial class EquipmentManagementDialog : Window
    {
        public EquipmentManagementDialog()
        {
            InitializeComponent();

            Owner = Application.Current.MainWindow;
        }
    }
}
