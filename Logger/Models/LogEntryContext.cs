using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Logger.Models
{
    public class LogEntryContext : DbContext
    {

        public DbSet<LogEntry> LogEntries { get; set; }

        public LogEntryContext(DbContextOptions<LogEntryContext> options)
            : base(options)
        {
        }
    }
}
