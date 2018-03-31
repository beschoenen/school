using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WPF.ViewModel;

namespace WPF.View
{
    public partial class MainWindow
    {
        private readonly List<KeyValuePair<string, TextBox>> _field;

        public MainWindow()
        {
            _field = new List<KeyValuePair<string, TextBox>>();

            InitializeComponent();

            SetupSudokuField();

            ((MainViewModel) DataContext).PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == "Game")
                {
                    UpdateField(((MainViewModel) sender).Game);
                }
            };

            UpdateField(((MainViewModel) DataContext).Game);
        }

        private void UpdateField(Wrapper.Sudoku game)
        {
            _field.ForEach(f =>
            {
                var split = f.Key.Split(':');

                var x = Convert.ToInt32(split[0]);
                var y = Convert.ToInt32(split[1]);

                int? value = game.Get(x, y);

                if (value == 0) value = null;

                f.Value.Text = value.ToString();
            });
        }

        private void SetupSudokuField()
        {
            var hOffset = 0;

            for (var i = 0; i < 9; i++)
            {
                SetupSudokuRow(i, hOffset);

                if (new[] {2, 5, 8}.Contains(i)) hOffset += 3;
            }
        }

        private void SetupSudokuRow(int row, int offset)
        {
            const int size = 25;

            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    var textBox = new TextBox
                    {
                        Width = size,
                        Height = size,
                        IsReadOnly = true,
                        Cursor = Cursors.Hand,
                        TextAlignment = TextAlignment.Center,
                        Margin = new Thickness(i * (size * 3) + j * size + (3 * i), row * size + offset, 0, 0),
                    };
                    
                    var x = (i * 3) + j + 1;
                    var y = row + 1;

                    textBox.PreviewMouseLeftButtonUp += (sender, e) =>
                    {
                        var vm = (MainViewModel) DataContext;
                        vm.X = x;
                        vm.Y = y;
                    };

                    _field.Add(new KeyValuePair<string, TextBox>($"{x}:{y}", textBox));

                    Canvas.Children.Add(textBox);
                }
            }
        }
    }
}