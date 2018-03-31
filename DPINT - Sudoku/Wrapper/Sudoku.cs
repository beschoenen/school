using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Sudoku;
using static System.Linq.Enumerable;

namespace Wrapper
{
    public class Sudoku : INotifyPropertyChanged
    {
        private readonly Game _game;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsValid
        {
            get
            {
                int valid;

                _game.isValid(out valid);

                return Convert.ToBoolean(valid);
            }
        }

        public bool IsComplete => IsValid && NumberOfEmptyFields == 0;

        public int NumberOfEmptyFields
        {
            get
            {
                var noFields = 0;

                for (short i = 1; i <= 9; i++)
                {
                    for (short j = 1; j <= 9; j++)
                    {
                        short value;

                        _game.get(i, j, out value);

                        if (value <= 0)
                        {
                            noFields++;
                        }
                    }
                }

                return noFields;
            }
        }

        public Sudoku()
        {
            _game = new Game();
            _game.create();
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Get a hint
        /// </summary>
        /// <returns>A hint object</returns>
        public bool Hint()
        {
            short x, y, value;
            int success;

            _game.hint(out x, out y, out value, out success);

            if (success == 1)
            {
                _game.set(x, y, value, out success);
            }

            OnPropertyChanged(nameof(_game));

            return Convert.ToBoolean(success);
        }

        /// <summary>
        /// Get the value of a field on the gameboard.
        /// </summary>
        /// <param name="x">X Coordinate</param>
        /// <param name="y">Y Coordinate</param>
        /// <returns>The value of the field</returns>
        public int Get(int x, int y)
        {
            short value;

            _game.get(Convert.ToInt16(x), Convert.ToInt16(y), out value);

            return Convert.ToInt32(value);
        }

        /// <summary>
        /// Set a value to a field on the gameboard.
        /// </summary>
        /// <param name="x">X Coordinate</param>
        /// <param name="y">Y Coordinate</param>
        /// <param name="value">The value to set</param>
        /// <returns>If the value has been set</returns>
        public bool Set(int x, int y, int value)
        {
            int success;

            _game.set(Convert.ToInt16(x), Convert.ToInt16(y), Convert.ToInt16(value), out success);

            if (!IsValid)
            {
                _game.set(Convert.ToInt16(x), Convert.ToInt16(y), 0, out success);
                return false;
            }

            OnPropertyChanged(nameof(_game));

            return Convert.ToBoolean(success);
        }

        /// <summary>
        /// Exposes all but 2 fields
        /// </summary>
        public void Cheat()
        {
            Range(0, NumberOfEmptyFields - 2).ToList().ForEach(e => Hint());
        }
    }
}