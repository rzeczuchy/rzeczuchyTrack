using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using rzeczuchyTrack.Data;
using rzeczuchyTrack.TimeEntries;
using rzeczuchyTrack.Utilities;

namespace rzeczuchyTrack.Views
{
    class MainView : View
    {
        private readonly TimeEntryList entryList;

        public MainView(DataReaderWriter data)
        {
            entryList = new TimeEntryList(new Point(0, 0), new Point(50, 30), data);
        }

        public override void Draw()
        {
            entryList.Draw();
            DrawShortcuts();
        }

        private void DrawShortcuts()
        {
            string shortcuts = "[Home] [End]";
            Utility.DrawString(shortcuts, new Point(0, Console.BufferHeight - 2), ConsoleColor.Black, ConsoleColor.White);
        }

        public override void OnClose() { }

        public override void OnOpen() { }

        public override void Update(ConsoleKey input)
        {
            switch (input)
            {
                case ConsoleKey.Home:
                    entryList.ScrollToTop();
                    break;
                case ConsoleKey.End:
                    entryList.ScrollToBottom();
                    break;
                case ConsoleKey.UpArrow:
                    entryList.MoveCursorUp();
                    break;
                case ConsoleKey.DownArrow:
                    entryList.MoveCursorDown();
                    break;
                default:
                    break;
            }
        }
    }
}
