using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace WPF.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public Wrapper.Sudoku Game { get; private set; }

        private int _x;
        public int X
        {
            get { return _x; }
            set
            {
                _x = value;
                RaisePropertyChanged();
            }
        }

        private int _y;
        public int Y
        {
            get { return _y; }
            set
            {
                _y = value;
                RaisePropertyChanged();
            }
        }

        private int _value;
        public int Value
        {
            get { return _value; }
            set
            {
                _value = value;
                RaisePropertyChanged();
            }
        }

        public RelayCommand SetCommand { get; set; }
        public RelayCommand CheatCommand { get; set; }
        public RelayCommand NewGameCommand { get; set; }

        public MainViewModel()
        {
            NewGame();

            SetCommand = new RelayCommand(Set);
            CheatCommand = new RelayCommand(Game.Cheat);
            NewGameCommand = new RelayCommand(NewGame);
        }

        public void NewGame()
        {
            Game = new Wrapper.Sudoku();

            Game.PropertyChanged += (sender, e) =>
            {
                RaisePropertyChanged(nameof(Game));
            };

            RaisePropertyChanged(nameof(Game));
        }

        private void Set()
        {
            if (!Game.Set(X, Y, Value))
            {
                MessageBox.Show("Your value is invalid!");
                return;
            }

            Value = 0;
            X = 0;
            Y = 0;

            if (Game.IsComplete)
            {
                MessageBox.Show("You won!");
            }
        }
    }
}