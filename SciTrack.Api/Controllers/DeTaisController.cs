using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SciTrack.Api.Data;
using SciTrack.Api.Models;

namespace SciTrack.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeTaisController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DeTaisController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/DeTais
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeTai>>> GetDeTais()
        {
            return await _context.DeTais.ToListAsync();
        }

        // GET: api/DeTais/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DeTai>> GetDeTai(int id)
        {
            var deTai = await _context.DeTais.FindAsync(id);

            if (deTai == null)
            {
                return NotFound();
            }

            return deTai;
        }

        // PUT: api/DeTais/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDeTai(int id, DeTai deTai)
        {
            if (id != deTai.Id)
            {
                return BadRequest();
            }

            _context.Entry(deTai).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeTaiExists(id))
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

        // POST: api/DeTais
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DeTai>> PostDeTai(DeTai deTai)
        {
            _context.DeTais.Add(deTai);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDeTai", new { id = deTai.Id }, deTai);
        }

        // DELETE: api/DeTais/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeTai(int id)
        {
            var deTai = await _context.DeTais.FindAsync(id);
            if (deTai == null)
            {
                return NotFound();
            }

            _context.DeTais.Remove(deTai);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DeTaiExists(int id)
        {
            return _context.DeTais.Any(e => e.Id == id);
        }
    }
}
