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
    public class TrangThietBisController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TrangThietBisController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/TrangThietBis
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrangThietBi>>> GetTrangThietBis()
        {
            return await _context.TrangThietBis.ToListAsync();
        }

        // GET: api/TrangThietBis/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TrangThietBi>> GetTrangThietBi(int id)
        {
            var trangThietBi = await _context.TrangThietBis.FindAsync(id);

            if (trangThietBi == null)
            {
                return NotFound();
            }

            return trangThietBi;
        }

        // PUT: api/TrangThietBis/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrangThietBi(int id, TrangThietBi trangThietBi)
        {
            if (id != trangThietBi.Id)
            {
                return BadRequest();
            }

            _context.Entry(trangThietBi).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrangThietBiExists(id))
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

        // POST: api/TrangThietBis
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TrangThietBi>> PostTrangThietBi(TrangThietBi trangThietBi)
        {
            _context.TrangThietBis.Add(trangThietBi);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTrangThietBi", new { id = trangThietBi.Id }, trangThietBi);
        }

        // DELETE: api/TrangThietBis/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrangThietBi(int id)
        {
            var trangThietBi = await _context.TrangThietBis.FindAsync(id);
            if (trangThietBi == null)
            {
                return NotFound();
            }

            _context.TrangThietBis.Remove(trangThietBi);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TrangThietBiExists(int id)
        {
            return _context.TrangThietBis.Any(e => e.Id == id);
        }
    }
}
