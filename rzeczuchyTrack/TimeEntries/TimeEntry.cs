using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rzeczuchyTrack.TimeEntries
{
    public class TimeEntry
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public DateTime Time { get; set; }
        public DateTime TrackedOn { get; }
    }
}
