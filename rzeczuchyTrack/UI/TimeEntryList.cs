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
        private int cursorPosition;
        private int topVisibleEntry;
        private readonly List<TimeEntry> entries;

        public TimeEntryList(Point position, Point size, DataReaderWriter data)
        {
            Position = position;
            Size = size;
            entries = data.GetTimeEntries();
            CursorPosition = 0;
            topVisibleEntry = 0;
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

        public override void Update(ConsoleKey input)
        {
            switch (input)
            {
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
            TimeEntry entry = entries[i];
            string listEntryData = "#" + entry.Id + " tracked " + entry.Time.ToString("h:mm:ss") + " on: " + entry.Label + " at: " + entry.TrackedOn;
            if (i == CursorPosition)
            {
                Utility.DrawString(listEntryData, new Point(Position.X, entryPosY + Position.Y), ConsoleColor.Blue, ConsoleColor.White);
            }
            else
            {
                Utility.DrawString(listEntryData, new Point(Position.X, entryPosY + Position.Y), ConsoleColor.Black, ConsoleColor.White);
            }
        }
    }
}
