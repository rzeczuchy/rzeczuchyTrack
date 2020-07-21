using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using rzeczuchyTrack.TimeEntries;

namespace rzeczuchyTrack.Data
{
    class DataReaderWriter
    {
        private readonly List<TimeEntry> entries;

        public DataReaderWriter()
        {
            entries = new List<TimeEntry>();

            for (int i = 0; i < 100; i++)
            {
                AddEntry("example entry", new DateTime(1, 1, 1, 0, 0, 1));
            };
        }

        public List<TimeEntry> GetTimeEntries()
        {
            return entries;
        }

        public void AddEntry(string label, DateTime time)
        {
            int id = entries.Any() ? entries.Max(e => e.Id) + 1 : 0;
            entries.Add(new TimeEntry()
            {
                Id = id,
                Label = label,
                Time = time,
            });
        }
    }
}
