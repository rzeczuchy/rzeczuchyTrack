using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using rzeczuchyTrack.Data;
using rzeczuchyTrack.TimeEntries;
using rzeczuchyTrack.Utilities;
using rzeczuchyTrack.UI;

namespace rzeczuchyTrack.Views
{
    class MainView : View
    {
        private readonly UIStateHandler ui;

        public MainView(DataReaderWriter data)
        {
            ui = new UIStateHandler();
            ui.AddState(new TimeEntryList(new Point(0, 0), new Point(50, 30), data));
            ui.AddState(new Window(new Point(10, 10), new Point(10, 10)));
        }

        public override void Draw()
        {
            ui.Draw();
            DrawShortcuts();
        }

        private void DrawShortcuts()
        {
            string shortcuts = "[Home] [End]";
            Utility.DrawString(shortcuts, new Point(0, Console.BufferHeight - 2), ConsoleColor.Black, ConsoleColor.White);
        }

        public override void OnClose() { }

        public override void OnOpen() { }

        public override void Update()
        {
            ui.Update();
        }

        public override void UpdateInput(ConsoleKey input)
        {
            ui.UpdateInput(input);
        }
    }
}
