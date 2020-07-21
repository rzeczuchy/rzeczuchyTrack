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

        public TimeEntryList(Point position, Point size, UIStateHandler ui)
        {
            this.ui = ui;
            Position = position;
            Size = size;
            entries = ReadEntriesFromDb();
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

        public void AddEntry(string label, int hours, int minutes, int seconds)
        {
            int id = entries.Any() ? entries.Max(e => e.Id) + 1 : 0;
            var entry = new TimeEntry()
            {
                Id = id,
                Label = label,
                Hours = hours,
                Minutes = minutes,
                Seconds = seconds,
                TrackedOn = DateTime.Now,
            };
            entries.Add(entry);

            using (var ctx = new EntriesDbContext())
            {
                ctx.TimeEntries.Add(entry);
                ctx.SaveChanges();
            }
        }

        public List<TimeEntry> GetTimeEntries()
        {
            return entries;
        }

        public void DeleteEntry(int id)
        {
            entries.RemoveAt(id);
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
            DateTime time = new DateTime(1, 1, 1, entry.Hours, entry.Minutes, entry.Seconds);
            string listEntryData = "#" + entry.Id + " tracked " + entry.Hours + ":" + time.ToString("mm:ss") +
                " on: " + entry.Label + " at: " + entry.TrackedOn;
            if (i == CursorPosition)
            {
                Utility.DrawString(listEntryData, new Point(Position.X + 1, entryPosY + Position.Y + 1),
                    ConsoleColor.Blue, ConsoleColor.White);
            }
            else
            {
                Utility.DrawString(listEntryData, new Point(Position.X + 1, entryPosY + Position.Y + 1),
                    window.BackgroundColor, window.ForegroundColor);
            }
        }

        private TimeEntry GetHovered()
        {
            return entries[entries.Count - 1 - CursorPosition];
        }

        private List<TimeEntry> ReadEntriesFromDb()
        {
            using (var ctx = new EntriesDbContext())
            {
                return ctx.TimeEntries.ToList();
            }
        }
    }
}
