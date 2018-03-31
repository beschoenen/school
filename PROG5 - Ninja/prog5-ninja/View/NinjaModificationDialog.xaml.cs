using System.Windows;
using database;
using prog5_ninja.Model;
using prog5_ninja.ViewModel;

namespace prog5_ninja.View
{
    public partial class NinjaModificationDialog : Window
    {
        public NinjaModificationDialog(ninja ninja = null)
        {
            InitializeComponent();

            DataContext = new NinjaModificationViewModel
            {
                Ninja = new NinjaCrudModel(ninja)
            };
        }
    }
}
