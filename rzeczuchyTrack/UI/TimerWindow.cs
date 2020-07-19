using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using rzeczuchyTrack.Utilities;
using rzeczuchyTrack.TimeEntries;

namespace rzeczuchyTrack.UI
{
    class TimerWindow : UIState
    {
        private readonly Window window;
        private const int MaxLength = 40;

        public TimerWindow(Point position, Point size)
        {
            Position = position;
            Size = size;
            window = new Window(position, size);
            window.BackgroundColor = ConsoleColor.DarkGreen;
            Timer = new Stopwatch();
            EnteredLabel = string.Empty;
        }

        public Point Position { get; set; }
        public Point Size { get; set; }
        public Stopwatch Timer { get; set; }
        public string EnteredLabel { get; private set; }

        public override void OnOpen()
        {
            Console.SetCursorPosition(Position.X + 10, Position.Y + 1);
        }

        public override void Update()
        {
            if (Timer.IsRunning)
            {
                DrawTimer();
            }
        }

        public override void UpdateInput(ConsoleKeyInfo input)
        {
            switch (input.Key)
            {
                case ConsoleKey.Enter:
                    ToggleTimer();
                    break;
                case ConsoleKey.Backspace:
                    Backspace();
                    break;
                default:
                    if (EnteredLabel.Length < MaxLength)
                    {
                        EnteredLabel += input.KeyChar;
                    }
                    break;
            }
        }

        public void ToggleTimer()
        {
            if (!Timer.IsRunning)
            {
                Timer.Start();
                window.BackgroundColor = ConsoleColor.DarkRed;
            }
            else
            {
                Timer.Stop();
                window.BackgroundColor = ConsoleColor.DarkYellow;
            }
        }

        public void Backspace()
        {
            if (EnteredLabel.Length > 0)
            {
                EnteredLabel = EnteredLabel.Remove(EnteredLabel.Length - 1);
            }
        }

        public void Space()
        {
            if (EnteredLabel.Length < MaxLength)
            {
                EnteredLabel += " ";
            }
        }

        public override void Draw()
        {
            window.Draw();
            Utility.DrawString("New entry:", new Point(Position.X + 1, Position.Y + 1), window.BackgroundColor, window.ForegroundColor);
            Utility.DrawString(EnteredLabel, new Point(Position.X + 12, Position.Y + 1), ConsoleColor.Black, window.ForegroundColor);
            Utility.DrawString("_", new Point(Position.X + 12 + EnteredLabel.Length, Position.Y + 1), ConsoleColor.Black, window.ForegroundColor);
            DrawTimer();
            Utility.DrawString("[Enter]-Start/Stop timer", new Point (Position.X + 1, Position.Y + Size.Y - 2), window.BackgroundColor, window.ForegroundColor);
        }

        private void DrawTimer()
        {
            string timerDisplay = string.Format("{0:00}:{1:00}:{2:00}",
            Timer.Elapsed.Hours, Timer.Elapsed.Minutes, Timer.Elapsed.Seconds);
            Utility.DrawString(timerDisplay, new Point(Position.X + Size.X - 10, Position.Y + 1), window.BackgroundColor, window.ForegroundColor);
        }
    }
}
