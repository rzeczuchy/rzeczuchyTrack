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
        private readonly TimeEntryList entryList;

        public MainView(DataReaderWriter data)
        {
            ui = new UIStateHandler();
            
            entryList = new TimeEntryList(new Point(1, 1), new Point(Console.BufferWidth - 2, 21), ui, data);
            ui.AddState(entryList);
            ui.Focused = entryList;
        }

        public override void Draw()
        {
            ui.Draw();
            DrawShortcuts();
        }

        private void DrawShortcuts()
        {
            string shortcuts = "[Ins]-New [Del]-Delete [Enter]-Edit [Up]/[Down]-Scroll [Home]/[End]-Top/Bottom";
            Utility.DrawString(shortcuts, new Point(1, Console.BufferHeight - 2), ConsoleColor.Black, ConsoleColor.White);
        }

        public override void OnClose() { }

        public override void OnOpen() { }

        public override void Update()
        {
            ui.Update();
        }

        public override void UpdateInput(ConsoleKeyInfo input)
        {
            ui.UpdateInput(input);
        }
    }
}
