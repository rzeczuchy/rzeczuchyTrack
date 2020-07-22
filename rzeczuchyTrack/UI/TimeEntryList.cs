using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
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
        private int topVisibleY;

        public TimeEntryList(Point position, Point size, UIStateHandler ui)
        {
            this.ui = ui;
            Position = position;
            Size = size;
            entries = ReadEntriesFromDb();
            CursorPosition = 0;
            topVisibleY = 0;
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

        public int MaxVisibleY
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

            AddEntryToDb(entry);
        }

        public void DeleteEntry(TimeEntry entry)
        {
            if (entry != null)
            {
                entries.Remove(entry);
                RemoveEntryFromDb(entry);

                if (CursorPosition >= entries.Count && entries.Any())
                {
                    MoveCursorUp();
                }
            }
        }

        public override void UpdateInput(ConsoleKeyInfo input)
        {
            switch (input.Key)
            {
                case ConsoleKey.Insert:
                    ui.OpenTimer(this);
                    break;
                case ConsoleKey.Delete:
                    DeleteEntry(GetHovered());
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

        public void MoveCursorUp()
        {
            CursorPosition--;

            if (CursorPosition < topVisibleY)
            {
            topVisibleY--;
            }
        }

        public void MoveCursorDown()
        {
            CursorPosition++;

            if (CursorPosition >= topVisibleY + MaxVisibleY)
            {
                topVisibleY++;
            }
        }

        public void ScrollToBottom()
        {
            if (IsScrollable())
            {
                topVisibleY = entries.Count - MaxVisibleY;
            }
            CursorPosition = entries.Count - 1;
        }

        public void ScrollToTop()
        {
            CursorPosition = 0;
            topVisibleY = 0;
        }

        public override void Draw()
        {
            window.Draw();
            int displayed = topVisibleY + MaxVisibleY;

            var stringsToDraw = ToDraw(displayed);

            displayed = stringsToDraw.Count < displayed ? stringsToDraw.Count : displayed;

            for (int i = topVisibleY; i < displayed; i++)
            {
                int entryPosY = i - topVisibleY;

                if (i == CursorPosition)
                {
                    Utility.DrawString(stringsToDraw[i], new Point(Position.X + 1, entryPosY + Position.Y + 1),
                        ConsoleColor.Blue, ConsoleColor.White);
                }
                else
                {
                    Utility.DrawString(stringsToDraw[i], new Point(Position.X + 1, entryPosY + Position.Y + 1),
                        window.BackgroundColor, window.ForegroundColor);
                }
                
            }
        }

        public List<string> ToDraw(int limit)
        {
            var strings = new List<string>();
            int row = 0;

            List<DateTime> distinctDays = entries.Select(e => e.TrackedOn.Date).Distinct().ToList();
            distinctDays.Reverse();
            int day = 0;

            while (row < limit && day < distinctDays.Count)
            {
                DateTime currentDay = distinctDays[day];
                strings.Add(GetDayString(currentDay));
                row++;

                List<TimeEntry> dayEntries = entries.Where(e => e.TrackedOn.Date == currentDay).ToList();

                for (int e = 0; e < dayEntries.Count; e++)
                {
                    if (row >= limit)
                    {
                        break;
                    }
                    strings.Add(GetEntryString(dayEntries[e]));
                    row++;
                }
                day++;
            }
            return strings;
        }

        private string GetDayString(DateTime day)
        {
            if (day == DateTime.Today)
            {
                return "Today";
            }
            if (day == DateTime.Today.AddDays(-1))
            {
                return "Yesterday";
            }
            return day.ToShortDateString();
        }

        private string GetEntryString(TimeEntry entry)
        {
            DateTime time = new DateTime(1, 1, 1, entry.Hours, entry.Minutes, entry.Seconds);
            return "#" + entry.Id + " tracked " + entry.Hours + ":" + time.ToString("mm:ss") +
                " on: " + entry.Label + " at: " + entry.TrackedOn;
        }
        
        private bool IsScrollable()
        {
            return entries.Count() > MaxVisibleY;
        }

        private TimeEntry GetHovered()
        {
            if (entries.Any())
            {
                return entries[entries.Count - 1 - CursorPosition];
            }
            return null;
        }

        private int GetHoveredPosition()
        {
            return entries.Count - 1 - CursorPosition;
        }

        #region DataHandling
        private static void AddEntryToDb(TimeEntry entry)
        {
            using (var ctx = new EntriesDbContext())
            {
                ctx.TimeEntries.Add(entry);
                ctx.SaveChanges();
            }
        }

        private void RemoveEntryFromDb(TimeEntry item)
        {
            using (var ctx = new EntriesDbContext())
            {
                ctx.Entry(item).State = EntityState.Deleted;
                ctx.SaveChanges();
            }
        }

        private List<TimeEntry> ReadEntriesFromDb()
        {
            using (var ctx = new EntriesDbContext())
            {
                return ctx.TimeEntries.ToList();
            }
        }
        #endregion
    }
}
