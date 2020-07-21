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
        private const int MaxLength = 40;
        private readonly UIStateHandler ui;
        private readonly TimeEntryList timeEntryList;
        private readonly Window window;

        public TimerWindow(Point position, Point size, UIStateHandler ui, TimeEntryList timeEntryList)
        {
            this.ui = ui;
            this.timeEntryList = timeEntryList;
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
                case ConsoleKey.Insert:
                    AddTimeEntry();
                    break;
                default:
                    if (EnteredLabel.Length < MaxLength)
                    {
                        char c = input.KeyChar;
                        if (char.IsLetter(c) || char.IsNumber(c) || char.IsSymbol(c) || char.IsPunctuation(c) || c == ' ')
                        {
                            EnteredLabel += c;
                        }
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

        public override void Draw()
        {
            window.Draw();
            Utility.DrawString("New entry:", new Point(Position.X + 1, Position.Y + 1), window.BackgroundColor, window.ForegroundColor);
            Utility.DrawString(EnteredLabel, new Point(Position.X + 12, Position.Y + 1), ConsoleColor.Black, window.ForegroundColor);
            Utility.DrawString("_", new Point(Position.X + 12 + EnteredLabel.Length, Position.Y + 1), ConsoleColor.Black, window.ForegroundColor);
            DrawTimer();
            Utility.DrawString("[Enter]-Start/Stop timer [Ins]-Save entry", new Point (Position.X + 1, Position.Y + Size.Y - 2), window.BackgroundColor, window.ForegroundColor);
        }

        private void DrawTimer()
        {
            string timerDisplay = string.Format("{0:00}:{1:00}:{2:00}",
            Timer.Elapsed.Hours, Timer.Elapsed.Minutes, Timer.Elapsed.Seconds);
            Utility.DrawString(timerDisplay, new Point(Position.X + Size.X - 10, Position.Y + 1), window.BackgroundColor, window.ForegroundColor);
        }

        private void AddTimeEntry()
        {
            if (!Timer.IsRunning && Timer.Elapsed.Seconds > 1)
            {
                timeEntryList.AddEntry(EnteredLabel, Timer.Elapsed.Hours, Timer.Elapsed.Minutes, Timer.Elapsed.Seconds);
                ui.CloseState(this);
            }
        }
    }
}
