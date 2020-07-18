using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using rzeczuchyTrack.Data;
using rzeczuchyTrack.TimeEntries;

namespace rzeczuchyTrack.Views
{
    class MainView : View
    {
        private readonly TimeEntryList entryList;

        public MainView(DataReaderWriter data)
        {
            entryList = new TimeEntryList(data);
        }

        public override void Draw()
        {
            entryList.Draw();
        }

        public override void OnClose() { }

        public override void OnOpen() { }

        public override void Update(ConsoleKey input)
        {

        }
    }
}
