using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SciTrack.Api.Data;
using SciTrack.Api.DTOs;
using SciTrack.Api.Models;

namespace SciTrack.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaiSansController : ControllerBase
    {
        private readonly KHCN_DBContext _context;

        public TaiSansController(KHCN_DBContext context)
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

        // POST: api/TaiSans
        [HttpPost]
        public async Task<ActionResult<TaiSan>> PostTaiSan(TaiSanCreateDto taiSanDto)
        {
            var newTaiSan = new TaiSan
            {
                SoDanhMuc = taiSanDto.SoDanhMuc,
                Ten = taiSanDto.Ten,
                NguyenGia = taiSanDto.NguyenGia,
                KhauHao = taiSanDto.KhauHao,
                HaoMon = taiSanDto.HaoMon,
                GiaTriConLai = taiSanDto.GiaTriConLai,
                TrangThai = taiSanDto.TrangThai,
                DeTaiId = taiSanDto.DeTaiId,
                NgayCapNhat = taiSanDto.NgayCapNhat
            };

            _context.TaiSans.Add(newTaiSan);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTaiSan), new { id = newTaiSan.Id }, newTaiSan);
        }

        // PUT: api/TaiSans/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaiSan(int id, TaiSanCreateDto taiSanDto) 
        {
            var taiSan = await _context.TaiSans.FindAsync(id);
            if (taiSan == null)
            {
                return NotFound();
            }

            taiSan.SoDanhMuc = taiSanDto.SoDanhMuc;
            taiSan.Ten = taiSanDto.Ten;
            taiSan.NguyenGia = taiSanDto.NguyenGia;
            taiSan.KhauHao = taiSanDto.KhauHao;
            taiSan.HaoMon = taiSanDto.HaoMon;
            taiSan.GiaTriConLai = taiSanDto.GiaTriConLai;
            taiSan.TrangThai = taiSanDto.TrangThai;
            taiSan.DeTaiId = taiSanDto.DeTaiId;
            taiSan.NgayCapNhat = taiSanDto.NgayCapNhat; 

            _context.Entry(taiSan).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.TaiSans.Any(e => e.Id == id))
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
    }
}