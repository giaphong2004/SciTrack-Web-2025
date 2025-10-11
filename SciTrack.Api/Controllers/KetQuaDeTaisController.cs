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
    public class KetQuaDeTaisController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public KetQuaDeTaisController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/KetQuaDeTais
        [HttpGet]
        public async Task<ActionResult<IEnumerable<KetQuaDeTai>>> GetKetQuaDeTais()
        {
            return await _context.KetQuaDeTais.ToListAsync();
        }

        // GET: api/KetQuaDeTais/5
        [HttpGet("{id}")]
        public async Task<ActionResult<KetQuaDeTai>> GetKetQuaDeTai(int id)
        {
            var ketQuaDeTai = await _context.KetQuaDeTais.FindAsync(id);

            if (ketQuaDeTai == null)
            {
                return NotFound();
            }

            return ketQuaDeTai;
        }

        // PUT: api/KetQuaDeTais/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKetQuaDeTai(int id, KetQuaDeTai ketQuaDeTai)
        {
            if (id != ketQuaDeTai.Id)
            {
                return BadRequest();
            }

            _context.Entry(ketQuaDeTai).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KetQuaDeTaiExists(id))
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

        // POST: api/KetQuaDeTais
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<KetQuaDeTai>> PostKetQuaDeTai(KetQuaDeTai ketQuaDeTai)
        {
            _context.KetQuaDeTais.Add(ketQuaDeTai);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetKetQuaDeTai", new { id = ketQuaDeTai.Id }, ketQuaDeTai);
        }

        // DELETE: api/KetQuaDeTais/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKetQuaDeTai(int id)
        {
            var ketQuaDeTai = await _context.KetQuaDeTais.FindAsync(id);
            if (ketQuaDeTai == null)
            {
                return NotFound();
            }

            _context.KetQuaDeTais.Remove(ketQuaDeTai);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool KetQuaDeTaiExists(int id)
        {
            return _context.KetQuaDeTais.Any(e => e.Id == id);
        }
    }
}
