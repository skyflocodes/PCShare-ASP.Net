using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCShare.Data;
using PCShare.Models;

namespace PCShare.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class PCsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PCsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/PCs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PC>>> GetPC()
        {
            return await _context.PC.ToListAsync();
        }

        // GET: api/PCs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PC>> GetPC(int id)
        {
            var pC = await _context.PC.FindAsync(id);

            if (pC == null)
            {
                return NotFound();
            }

            return pC;
        }

        // PUT: api/PCs/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPC(int id, PC pC)
        {
            if (id != pC.Id)
            {
                return BadRequest();
            }

            _context.Entry(pC).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PCExists(id))
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

        // POST: api/PCs
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PC>> PostPC(PC pC)
        {
            _context.PC.Add(pC);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPC", new { id = pC.Id }, pC);
        }

        // DELETE: api/PCs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PC>> DeletePC(int id)
        {
            var pC = await _context.PC.FindAsync(id);
            if (pC == null)
            {
                return NotFound();
            }

            _context.PC.Remove(pC);
            await _context.SaveChangesAsync();

            return pC;
        }

        private bool PCExists(int id)
        {
            return _context.PC.Any(e => e.Id == id);
        }
    }
}
