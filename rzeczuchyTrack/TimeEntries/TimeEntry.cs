using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace rzeczuchyTrack.TimeEntries
{
    public class TimeEntry
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(40)]
        public string Label { get; set; }

        public int Hours { get; set; }

        public int Minutes { get; set; }

        public int Seconds { get; set; }

        public DateTime TrackedOn { get; set;  }
    }
}
