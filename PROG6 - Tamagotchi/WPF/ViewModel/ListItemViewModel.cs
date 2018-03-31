using System.Windows.Controls;
using GalaSoft.MvvmLight;
using WPF.Control;
using WPF.TamagotchiService;

namespace WPF.ViewModel
{
    public class ListItemViewModel : ViewModelBase
    {
        public Tamagotchi Tamagotchi { get; set; }

        public UserControl Control => new ListItemUserControl {DataContext = this};
    }
}