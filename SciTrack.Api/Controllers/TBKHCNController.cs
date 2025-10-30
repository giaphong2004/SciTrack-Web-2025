using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SciTrack.Api.Data;
using SciTrack.Api.DTOs;
using SciTrack.Api.Models;

namespace SciTrack.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TBKHCNController : ControllerBase
    {
        private readonly KHCN_DBContext _context;

        public TBKHCNController(KHCN_DBContext context)
        {
            _context = context;
        }

        // GET: api/TBKHCN
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TBKHCNViewDto>>> GetTBKHCNs()
        {
            var result = await _context.TTBKHCNs
                .Include(t => t.HopDong)
                .Select(t => new TBKHCNViewDto
                {
                    ID = t.ID,
                    TenThietBi = t.TenThietBi,
                    NgayDuaVaoSuDung = t.NgayDuaVaoSuDung,
                    NguyenGia = t.NguyenGia,
                    KhauHao = t.KhauHao,
                    GiaTriConLai = t.GiaTriConLai,
                    DT_HD_KHCN_LienQuan = t.DT_HD_KHCN_LienQuan,
                    NhatKySuDung = t.NhatKySuDung,
                    TinhTrangThietBi = t.TinhTrangThietBi,
                    MaSoHopDong = t.MaSoHopDong,
                    TenDoiTac = t.HopDong != null ? t.HopDong.TenDoiTac : null
                })
                .ToListAsync();

            return Ok(result);
        }

        // GET: api/TBKHCN/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TBKHCNViewDto>> GetTBKHCN(int id)
        {
            var tb = await _context.TTBKHCNs
                .Include(t => t.HopDong)
                .Where(t => t.ID == id)
                .Select(t => new TBKHCNViewDto
                {
                    ID = t.ID,
                    TenThietBi = t.TenThietBi,
                    NgayDuaVaoSuDung = t.NgayDuaVaoSuDung,
                    NguyenGia = t.NguyenGia,
                    KhauHao = t.KhauHao,
                    GiaTriConLai = t.GiaTriConLai,
                    DT_HD_KHCN_LienQuan = t.DT_HD_KHCN_LienQuan,
                    NhatKySuDung = t.NhatKySuDung,
                    TinhTrangThietBi = t.TinhTrangThietBi,
                    MaSoHopDong = t.MaSoHopDong,
                    TenDoiTac = t.HopDong != null ? t.HopDong.TenDoiTac : null
                })
                .FirstOrDefaultAsync();

            if (tb == null)
                return NotFound();

            return Ok(tb);
        }

        // POST: api/TBKHCN
        [HttpPost]
        public async Task<ActionResult<TBKHCN>> PostTBKHCN(TBKHCNCreateDto dto)
        {
            var tb = new TBKHCN
            {
                TenThietBi = dto.TenThietBi,
                NgayDuaVaoSuDung = dto.NgayDuaVaoSuDung,
                NguyenGia = dto.NguyenGia,
                KhauHao = dto.KhauHao,
                GiaTriConLai = dto.GiaTriConLai,
                DT_HD_KHCN_LienQuan = dto.DT_HD_KHCN_LienQuan,
                NhatKySuDung = dto.NhatKySuDung,
                TinhTrangThietBi = dto.TinhTrangThietBi,
                MaSoHopDong = dto.MaSoHopDong
            };

            _context.TTBKHCNs.Add(tb);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTBKHCN), new { id = tb.ID }, tb);
        }

        // PUT: api/TBKHCN/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTBKHCN(int id, TBKHCNCreateDto dto)
        {
            var tb = await _context.TTBKHCNs.FindAsync(id);
            if (tb == null) return NotFound();

            tb.TenThietBi = dto.TenThietBi;
            tb.NgayDuaVaoSuDung = dto.NgayDuaVaoSuDung;
            tb.NguyenGia = dto.NguyenGia;
            tb.KhauHao = dto.KhauHao;
            tb.GiaTriConLai = dto.GiaTriConLai;
            tb.DT_HD_KHCN_LienQuan = dto.DT_HD_KHCN_LienQuan;
            tb.NhatKySuDung = dto.NhatKySuDung;
            tb.TinhTrangThietBi = dto.TinhTrangThietBi;
            tb.MaSoHopDong = dto.MaSoHopDong;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/TBKHCN/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTBKHCN(int id)
        {
            var tb = await _context.TTBKHCNs.FindAsync(id);
            if (tb == null) return NotFound();

            _context.TTBKHCNs.Remove(tb);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
