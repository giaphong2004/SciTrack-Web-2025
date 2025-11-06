using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SciTrack.Api.Data;
using SciTrack.Api.DTOs;
using SciTrack.Api.Models;

namespace SciTrack.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HopDongController : ControllerBase
    {
        private readonly KHCN_DBContext _context;

        public HopDongController(KHCN_DBContext context)
        {
            _context = context;
        }

        // GET: api/HopDong
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HopDongViewDto>>> GetAll()
        {
            var list = await _context.HopDongs
                .AsNoTracking()
                .Select(h => new HopDongViewDto
                {
                    Id = h.Id,
                    TenDoiTac = h.TenDoiTac,
                    NgayHieuLuc = h.NgayHieuLuc,
                    NgayNghiemThu = h.NgayNghiemThu,
                    TongGiaTriHopDong = h.TongGiaTriHopDong,
                    ChiPhiKetQuaDeTai = h.ChiPhiKetQuaDeTai,
                    ChiPhiTrangThietBi = h.ChiPhiTrangThietBi,
                    ChiPhiHoatDongChuyenMon = h.ChiPhiHoatDongChuyenMon,
                    LoiNhuan = h.LoiNhuan,
                    MaHopDong = h.MaHopDong
                })
                .ToListAsync();

            return Ok(list);
        }

        // GET: api/HopDong/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HopDongViewDto>> GetById(int id)
        {
            var h = await _context.HopDongs.FindAsync(id);
            if (h == null) return NotFound();

            var dto = new HopDongViewDto
            {
                Id = h.Id,
                TenDoiTac = h.TenDoiTac,
                NgayHieuLuc = h.NgayHieuLuc,
                NgayNghiemThu = h.NgayNghiemThu,
                TongGiaTriHopDong = h.TongGiaTriHopDong,
                ChiPhiKetQuaDeTai = h.ChiPhiKetQuaDeTai,
                ChiPhiTrangThietBi = h.ChiPhiTrangThietBi,
                ChiPhiHoatDongChuyenMon = h.ChiPhiHoatDongChuyenMon,
                LoiNhuan = h.LoiNhuan,
                MaHopDong = h.MaHopDong
            };

            return Ok(dto);
        }

        // POST: api/HopDong
        [HttpPost]
        public async Task<ActionResult> Create(HopDongCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var entity = new HopDong
            {
                TenDoiTac = dto.TenDoiTac,
                NgayHieuLuc = dto.NgayHieuLuc,
                NgayNghiemThu = dto.NgayNghiemThu,
                TongGiaTriHopDong = dto.TongGiaTriHopDong,
                ChiPhiKetQuaDeTai = dto.ChiPhiKetQuaDeTai,
                ChiPhiTrangThietBi = dto.ChiPhiTrangThietBi,
                ChiPhiHoatDongChuyenMon = dto.ChiPhiHoatDongChuyenMon,
                LoiNhuan = dto.LoiNhuan,
                MaHopDong = dto.MaHopDong
            };

            _context.HopDongs.Add(entity);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity);
        }

        // PUT: api/HopDong/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, HopDongCreateDto dto)
        {
            var entity = await _context.HopDongs.FindAsync(id);
            if (entity == null) return NotFound();

            entity.TenDoiTac = dto.TenDoiTac;
            entity.NgayHieuLuc = dto.NgayHieuLuc;
            entity.NgayNghiemThu = dto.NgayNghiemThu;
            entity.TongGiaTriHopDong = dto.TongGiaTriHopDong;
            entity.ChiPhiKetQuaDeTai = dto.ChiPhiKetQuaDeTai;
            entity.ChiPhiTrangThietBi = dto.ChiPhiTrangThietBi;
            entity.ChiPhiHoatDongChuyenMon = dto.ChiPhiHoatDongChuyenMon;
            entity.LoiNhuan = dto.LoiNhuan;
            entity.MaHopDong = dto.MaHopDong;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/HopDong/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _context.HopDongs.FindAsync(id);
            if (entity == null) return NotFound();

            _context.HopDongs.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
