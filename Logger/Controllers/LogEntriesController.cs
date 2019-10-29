using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Logger.Models;
using Microsoft.Extensions.Logging;

namespace Logger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogEntriesController : ControllerBase
    {
        private readonly LogEntryContext _context;
        private readonly ILogger<LogEntriesController> _logger;

        public LogEntriesController(LogEntryContext context, ILogger<LogEntriesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/LogEntries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LogEntry>>> GetLogEntries()
        {
            return await _context.LogEntries.ToListAsync();
        }

        // GET: api/LogEntries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LogEntry>> GetLogEntry(long id)
        {
            var logEntry = await _context.LogEntries.FindAsync(id);

            if (logEntry == null)
            {
                return NotFound();
            }

            return logEntry;
        }

        // PUT: api/LogEntries/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLogEntry(long id, LogEntry logEntry)
        {
            if (id != logEntry.Id)
            {
                return BadRequest();
            }

            _context.Entry(logEntry).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LogEntryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/LogEntries
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<LogEntry>> PostLogEntry(LogEntry logEntry)
        {
            _context.LogEntries.Add(logEntry);
            await _context.SaveChangesAsync();
            
            _logger.LogInformation($"{logEntry.LogTime} : [{logEntry.Entry}]" );

            //return CreatedAtAction("GetLogEntry", new { id = logEntry.Id }, logEntry);
            return CreatedAtAction(nameof(GetLogEntry), new { id = logEntry.Id }, logEntry);
        }

        // DELETE: api/LogEntries/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<LogEntry>> DeleteLogEntry(long id)
        {
            var logEntry = await _context.LogEntries.FindAsync(id);
            if (logEntry == null)
            {
                return NotFound();
            }

            _context.LogEntries.Remove(logEntry);
            await _context.SaveChangesAsync();

            return logEntry;
        }

        private bool LogEntryExists(long id)
        {
            return _context.LogEntries.Any(e => e.Id == id);
        }
    }
}
