using System.Windows;
using System.Windows.Input;
using database;
using prog5_ninja.Model;
using prog5_ninja.ViewModel;

namespace prog5_ninja.View
{
    public partial class EquipmentModificationDialog : Window
    {
        public EquipmentModificationDialog(equipment equipment = null)
        {
            InitializeComponent();

            DataContext = new EquipmentModificationViewModel
            {
                Equipment = new EquipmentCrudModel(equipment)
            };
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            int result;

            if (!(int.TryParse(e.Text, out result) || e.Text == "-"))
            {
                e.Handled = true;
            }
        }
    }
}
