using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SciTrack.Api.Data;
using SciTrack.Api.DTOs;
using SciTrack.Api.Models;

namespace SciTrack.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeTaisController : ControllerBase
    {
        private readonly KHCN_DBContext _context;

        public DeTaisController(KHCN_DBContext context)
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

        // POST: api/DeTais
        [HttpPost]
        public async Task<ActionResult<DeTai>> PostDeTai(DeTaiCreateDto deTaiDto)
        {
            var newDeTai = new DeTai
            {
                Ten = deTaiDto.Ten,
                MaDeTai = deTaiDto.MaDeTai,
                DecisionRefs = deTaiDto.DecisionRefs,
                BudgetExecution = deTaiDto.BudgetExecution,
                BudgetForTraining = deTaiDto.BudgetForTraining,
                ConsumablesBudget = deTaiDto.ConsumablesBudget,
                EquipmentDepreciation = deTaiDto.EquipmentDepreciation,
                CreatedAt = DateTime.UtcNow
            };

            _context.DeTais.Add(newDeTai);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetDeTai), new { id = newDeTai.Id }, newDeTai);
        }

        // PUT: api/DeTais/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDeTai(int id, DeTai deTai)
        {
            if (id != deTai.Id)
            {
                return BadRequest();
            }
            deTai.UpdatedAt = DateTime.UtcNow;
            _context.Entry(deTai).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.DeTais.Any(e => e.Id == id))
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
    }
}