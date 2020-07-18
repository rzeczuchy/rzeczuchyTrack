using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rzeczuchyTrack.TimeEntries
{
    class TimeEntry
    {
        public TimeEntry(int id, string label)
        {
            Id = id;
            Label = label;
            Time = new DateTime(1, 1, 1, 1, 30, 5);
            TrackedOn = DateTime.Now;
        }

        public int Id { get; }
        public string Label { get; set; }
        public DateTime Time { get; set; }
        public DateTime TrackedOn { get; }
    }
}
