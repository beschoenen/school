using System;
using System.Linq;
using System.Timers;
using Gold_Rush.View;
using Microsoft.Win32;

namespace Gold_Rush.Model
{
    public class Game
    {
        public Timer Timer;

        private PlayingGround PlayingGround { get; set; }

        public static bool IsPlaying;

        public static int Score { get; set; }

        public Game()
        {
            IsPlaying = true;

            PlayingGround = Reviver.GeneratePlayingGround();
            Score = 0;
            Timer = new Timer(1000) {AutoReset = true};
            Timer.Elapsed += TimerTick;

            Timer.Start();

            OutputView.PrintPlayground(PlayingGround);
            InputView.PrintGameInstruction();
        }

        public void MoveSwitch(ConsoleKey input)
        {
            if (!IsPlaying) return;

            ConsoleKey[] keys = {ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.D3, ConsoleKey.D4, ConsoleKey.D5};

            if (keys.Contains(input))
            {
                PlayingGround.MoveSwitch(Array.IndexOf(keys, input) + 1);
                OutputView.PrintPlayground(PlayingGround);
                InputView.PrintGameInstruction();
            }
        }

        public void TimerTick(object sender, ElapsedEventArgs eventArgs)
        {
            PlayingGround.MoveVehicles();
            PlayingGround.SpawnNewCarts();

            OutputView.PrintPlayground(PlayingGround);

            if (!IsPlaying)
            {
                Timer.Stop();
                Timer.Dispose();

                OutputView.PrintEndGame(Score);
                return;
            }

            InputView.PrintGameInstruction();
        }
    }
}