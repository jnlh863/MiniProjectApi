using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestiondeEventos.Context;
using GestiondeEventos.Models;
using Microsoft.AspNetCore.Cors;

namespace GestiondeEventos.Controllers
{
    [EnableCors("ReglasCORS")]
    [Route("api/[controller]")]
    [ApiController]
    public class NotifiesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public NotifiesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Notifies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Notify>>> GetNotifications()
        {
            return await _context.Notifications.ToListAsync();
        }

        // GET: api/Notifies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Notify>> GetNotify(int id)
        {
            var notify = await _context.Notifications.FindAsync(id);

            if (notify == null)
            {
                return NotFound();
            }

            return notify;
        }

        // PUT: api/Notifies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNotify(int id, Notify notify)
        {
            if (id != notify.id)
            {
                return BadRequest();
            }

            _context.Entry(notify).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotifyExists(id))
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

        // POST: api/Notifies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Notify>> PostNotify(Notify notify)
        {
            _context.Notifications.Add(notify);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNotify", new { id = notify.id }, notify);
        }

        // DELETE: api/Notifies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotify(int id)
        {
            var notify = await _context.Notifications.FindAsync(id);
            if (notify == null)
            {
                return NotFound();
            }

            _context.Notifications.Remove(notify);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NotifyExists(int id)
        {
            return _context.Notifications.Any(e => e.id == id);
        }
    }
}
