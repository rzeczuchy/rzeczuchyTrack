using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using rzeczuchyTrack.Data;
using rzeczuchyTrack.Utilities;
using rzeczuchyTrack.TimeEntries;

namespace rzeczuchyTrack.UI
{
    class TimeEntryList : UIState
    {
        private readonly UIStateHandler ui;
        private readonly List<TimeEntry> entries;
        private readonly Window window;
        private int cursorPosition;
        private int topVisibleEntry;

        public TimeEntryList(Point position, Point size, UIStateHandler ui, DataReaderWriter data)
        {
            this.ui = ui;
            Position = position;
            Size = size;
            entries = data.GetTimeEntries();
            CursorPosition = 0;
            topVisibleEntry = 0;
            window = new Window(position, size);
        }

        public Point Position { get; set; }
        public Point Size { get; set; }

        public int CursorPosition
        {
            get => cursorPosition;
            set
            {
                cursorPosition = Utility.Clamp(value, 0, entries.Count - 1);
            }
        }

        public int maxVisibleEntries
        {
            get
            {
                return (Position.Y + Size.Y > Console.BufferHeight - 1) ? Console.BufferHeight - Position.Y - 2 : Size.Y - 2;
            }
        }

        public void AddEntry(string label, DateTime time)
        {
            entries.Add(new TimeEntry() {
                Id = entries.Max(i => i.Id) + 1,
                Label = label,
                Time = time,
            });
        }

        public void DeleteEntry(TimeEntry entry)
        {
            entries.Remove(entry);
        }

        public override void UpdateInput(ConsoleKeyInfo input)
        {
            switch (input.Key)
            {
                case ConsoleKey.Insert:
                    ui.OpenTimer(this);
                    break;
                case ConsoleKey.Delete:
                    //delete time entry
                    break;
                case ConsoleKey.Home:
                    ScrollToTop();
                    break;
                case ConsoleKey.End:
                    ScrollToBottom();
                    break;
                case ConsoleKey.UpArrow:
                    MoveCursorUp();
                    break;
                case ConsoleKey.DownArrow:
                    MoveCursorDown();
                    break;
                default:
                    break;
            }
        }

        public override void Draw()
        {
            window.Draw();
            int displayed = (topVisibleEntry + maxVisibleEntries < entries.Count()) ? topVisibleEntry + maxVisibleEntries : entries.Count();
            for (int i = topVisibleEntry; i < displayed; i++)
            {
                DrawEntry(i);
            }
        }

        public void MoveCursorUp()
        {
            CursorPosition--;

            if (CursorPosition < topVisibleEntry)
            {
            topVisibleEntry--;
            }
        }

        public void MoveCursorDown()
        {
            CursorPosition++;

            if (CursorPosition >= topVisibleEntry + maxVisibleEntries)
            {
                topVisibleEntry++;
            }
        }

        public void ScrollToBottom()
        {
            if (IsScrollable())
            {
                topVisibleEntry = entries.Count - maxVisibleEntries;
            }
            CursorPosition = entries.Count - 1;
        }

        public void ScrollToTop()
        {
            CursorPosition = 0;
            topVisibleEntry = 0;
        }

        private bool IsScrollable()
        {
            return entries.Count() > maxVisibleEntries;
        }

        private void DrawEntry(int i)
        {
            int entryPosY = i - topVisibleEntry;
            TimeEntry entry = entries[entries.Count - 1 - i];
            string listEntryData = "#" + entry.Id + " tracked " + entry.Time.ToString("h:mm:ss") + " on: " + entry.Label + " at: " + entry.TrackedOn;
            if (i == CursorPosition)
            {
                Utility.DrawString(listEntryData, new Point(Position.X + 1, entryPosY + Position.Y + 1), ConsoleColor.Blue, ConsoleColor.White);
            }
            else
            {
                Utility.DrawString(listEntryData, new Point(Position.X + 1, entryPosY + Position.Y + 1), window.BackgroundColor, window.ForegroundColor);
            }
        }

        private TimeEntry GetHovered()
        {
            return entries[entries.Count - 1 - CursorPosition];
        }
    }
}
