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

            for (int i = 0; i < 1000; i++)
            {
                entries.Add(new TimeEntry(entries.Count, "example entry"));
            }
        }

        public List<TimeEntry> GetTimeEntries()
        {
            return entries;
        }
    }
}
