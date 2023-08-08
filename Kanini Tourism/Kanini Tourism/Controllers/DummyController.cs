using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Kanini_Tourism.Data;
using Kanini_Tourism.Models;

namespace Kanini_Tourism.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DummyController : ControllerBase
    {
        private readonly TourDBContext _context;

        public DummyController(TourDBContext context)
        {
            _context = context;
        }

        // GET: api/Dummy
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dummy>>> GetDummy()
        {
          if (_context.Dummy == null)
          {
              return NotFound();
          }
            return await _context.Dummy.ToListAsync();
        }

        // GET: api/Dummy/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Dummy>> GetDummy(int id)
        {
          if (_context.Dummy == null)
          {
              return NotFound();
          }
            var dummy = await _context.Dummy.FindAsync(id);

            if (dummy == null)
            {
                return NotFound();
            }

            return dummy;
        }

        // PUT: api/Dummy/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDummy(int id, Dummy dummy)
        {
            if (id != dummy.UserId)
            {
                return BadRequest();
            }

            _context.Entry(dummy).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DummyExists(id))
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

        // POST: api/Dummy
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Dummy>> PostDummy(Dummy dummy)
        {
          if (_context.Dummy == null)
          {
              return Problem("Entity set 'TourDBContext.Dummy'  is null.");
          }
            _context.Dummy.Add(dummy);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDummy", new { id = dummy.UserId }, dummy);
        }

        // DELETE: api/Dummy/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDummy(int id)
        {
            if (_context.Dummy == null)
            {
                return NotFound();
            }
            var dummy = await _context.Dummy.FindAsync(id);
            if (dummy == null)
            {
                return NotFound();
            }

            _context.Dummy.Remove(dummy);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DummyExists(int id)
        {
            return (_context.Dummy?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
