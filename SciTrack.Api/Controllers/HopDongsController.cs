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
    public class HopDongsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public HopDongsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/HopDongs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HopDong>>> GetHopDongs()
        {
            return await _context.HopDongs.ToListAsync();
        }

        // GET: api/HopDongs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HopDong>> GetHopDong(int id)
        {
            var hopDong = await _context.HopDongs.FindAsync(id);

            if (hopDong == null)
            {
                return NotFound();
            }

            return hopDong;
        }

        // PUT: api/HopDongs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHopDong(int id, HopDong hopDong)
        {
            if (id != hopDong.Id)
            {
                return BadRequest();
            }

            _context.Entry(hopDong).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HopDongExists(id))
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

        // POST: api/HopDongs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HopDong>> PostHopDong(HopDong hopDong)
        {
            _context.HopDongs.Add(hopDong);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHopDong", new { id = hopDong.Id }, hopDong);
        }

        // DELETE: api/HopDongs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHopDong(int id)
        {
            var hopDong = await _context.HopDongs.FindAsync(id);
            if (hopDong == null)
            {
                return NotFound();
            }

            _context.HopDongs.Remove(hopDong);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HopDongExists(int id)
        {
            return _context.HopDongs.Any(e => e.Id == id);
        }
    }
}
