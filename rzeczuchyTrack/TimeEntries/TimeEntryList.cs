using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using rzeczuchyTrack.Data;
using rzeczuchyTrack.Utilities;

namespace rzeczuchyTrack.TimeEntries
{
    class TimeEntryList
    {
        private readonly int maxVisibleEntries;
        private int cursorPosition;
        private int topVisibleEntry;
        private readonly List<TimeEntry> entries;

        public TimeEntryList(DataReaderWriter data)
        {
            entries = data.GetTimeEntries();
            CursorPosition = 0;
            topVisibleEntry = 0;
            maxVisibleEntries = Console.BufferHeight - 2;
        }

        public int CursorPosition
        {
            get => cursorPosition;
            set
            {
                cursorPosition = Utility.Clamp(value, 0, entries.Count);
            }
        }

        public void Draw()
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
            int yPosition = i - topVisibleEntry;
            string listEntryData = entries[i].Id + " " + entries[i].Label + " createdOn: " + entries[i].CreatedOn;
            if (i == CursorPosition)
            {
                DrawString(listEntryData, 0, yPosition, ConsoleColor.Blue, ConsoleColor.White);
            }
            else
            {
                DrawString(listEntryData, 0, yPosition, ConsoleColor.Black, ConsoleColor.White);
            }
        }

        private void DrawString(string str, int x, int y, ConsoleColor background, ConsoleColor foreground)
        {
            Console.BackgroundColor = background;
            Console.ForegroundColor = foreground;
            Console.SetCursorPosition(x, y);
            Console.WriteLine(str);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
