using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using finalAPI.Data;
using finalAPI.Models;
using System.IO;

namespace finalAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ThreadsController : ControllerBase
    {
        private readonly finalAPIContext _context;

        public ThreadsController(finalAPIContext context)
        {
            _context = context;
        }

        // GET: api/Threads
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Thread>>> GetThread()
        {
            return await _context.Thread.ToListAsync();
        }

        // GET: api/Threads/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Thread>> GetThread(int id)
        {
            var thread = await _context.Thread.FindAsync(id);

            if (thread == null)
            {
                return NotFound();
            }

            return thread;
        }

        // POST: api/Threads
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Thread>> PostThread(Thread thread)
        {
            _context.Thread.Add(thread);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetThread", new { id = thread.Id }, thread);
        }

    }
}
