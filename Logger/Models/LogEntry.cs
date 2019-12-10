using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logger.Models
{
    public class LogEntry
    {
        public long Id { get; set; }
        public string LogTime { get; set; }
        public string Entry { get; set; }

    }
}
