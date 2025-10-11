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
    public class TaiSansController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TaiSansController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/TaiSans
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaiSan>>> GetTaiSans()
        {
            return await _context.TaiSans.ToListAsync();
        }

        // GET: api/TaiSans/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaiSan>> GetTaiSan(int id)
        {
            var taiSan = await _context.TaiSans.FindAsync(id);

            if (taiSan == null)
            {
                return NotFound();
            }

            return taiSan;
        }

        // PUT: api/TaiSans/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaiSan(int id, TaiSan taiSan)
        {
            if (id != taiSan.Id)
            {
                return BadRequest();
            }

            _context.Entry(taiSan).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaiSanExists(id))
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

        // POST: api/TaiSans
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TaiSan>> PostTaiSan(TaiSan taiSan)
        {
            _context.TaiSans.Add(taiSan);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTaiSan", new { id = taiSan.Id }, taiSan);
        }

        // DELETE: api/TaiSans/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaiSan(int id)
        {
            var taiSan = await _context.TaiSans.FindAsync(id);
            if (taiSan == null)
            {
                return NotFound();
            }

            _context.TaiSans.Remove(taiSan);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TaiSanExists(int id)
        {
            return _context.TaiSans.Any(e => e.Id == id);
        }
    }
}
