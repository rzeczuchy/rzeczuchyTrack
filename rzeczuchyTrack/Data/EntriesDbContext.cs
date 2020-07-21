using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using rzeczuchyTrack.TimeEntries;

namespace rzeczuchyTrack.Data
{
    public class EntriesDbContext : DbContext
    {
        public DbSet<TimeEntry> TimeEntries { get; set; }
    }
}
