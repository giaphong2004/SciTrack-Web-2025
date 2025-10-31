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

        // GET ALL
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TBKHCNViewDto>>> GetTBKHCNs()
        {
            var result = await _context.TTBKHCNs
                .AsNoTracking()
                .Select(t => new TBKHCNViewDto
                {
                    Id = t.Id,  // ← ĐÃ SỬA: ID → Id
                    TenThietBi = t.TenThietBi,
                    NgayDuaVaoSuDung = t.NgayDuaVaoSuDung,
                    NguyenGia = t.NguyenGia,
                    KhauHao = t.KhauHao,
                    GiaTriConLai = t.GiaTriConLai,
                    DT_HD_KHCN_LienQuan = t.DT_HD_KHCN_LienQuan,
                    NhatKySuDung = t.NhatKySuDung,
                    TinhTrangThietBi = t.TinhTrangThietBi,
                    MaThietBi = t.MaThietBi
                })
                .ToListAsync();

            return Ok(result);
        }

        // GET BY ID
        [HttpGet("{id}")]
        public async Task<ActionResult<TBKHCNViewDto>> GetTBKHCN(int id)
        {
            var tb = await _context.TTBKHCNs
                .AsNoTracking()
                .Where(t => t.Id == id)  // ← ĐÃ SỬA: ID → Id
                .Select(t => new TBKHCNViewDto
                {
                    Id = t.Id,  // ← ĐÃ SỬA
                    TenThietBi = t.TenThietBi,
                    NgayDuaVaoSuDung = t.NgayDuaVaoSuDung,
                    NguyenGia = t.NguyenGia,
                    KhauHao = t.KhauHao,
                    GiaTriConLai = t.GiaTriConLai,
                    DT_HD_KHCN_LienQuan = t.DT_HD_KHCN_LienQuan,
                    NhatKySuDung = t.NhatKySuDung,
                    TinhTrangThietBi = t.TinhTrangThietBi,
                    MaThietBi = t.MaThietBi
                })
                .FirstOrDefaultAsync();

            if (tb == null)
                return NotFound();

            return Ok(tb);
        }

        // POST
        [HttpPost]
        public async Task<ActionResult<TBKHCN>> PostTBKHCN([FromBody] TBKHCNCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

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
                MaThietBi = dto.MaThietBi
            };

            _context.TTBKHCNs.Add(tb);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTBKHCN), new { id = tb.Id }, tb); // ← Id
        }

        // PUT
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTBKHCN(int id, [FromBody] TBKHCNCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var tb = await _context.TTBKHCNs.FindAsync(id);
            if (tb == null)
                return NotFound();

            tb.TenThietBi = dto.TenThietBi;
            tb.NgayDuaVaoSuDung = dto.NgayDuaVaoSuDung;
            tb.NguyenGia = dto.NguyenGia;
            tb.KhauHao = dto.KhauHao;
            tb.GiaTriConLai = dto.GiaTriConLai;
            tb.DT_HD_KHCN_LienQuan = dto.DT_HD_KHCN_LienQuan;
            tb.NhatKySuDung = dto.NhatKySuDung;
            tb.TinhTrangThietBi = dto.TinhTrangThietBi;
            tb.MaThietBi = dto.MaThietBi;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTBKHCN(int id)
        {
            var tb = await _context.TTBKHCNs.FindAsync(id);
            if (tb == null)
                return NotFound();

            try
            {
                _context.TTBKHCNs.Remove(tb);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return BadRequest("Thiết bị này đang được tham chiếu ở bảng khác, không thể xóa.");
            }

            return NoContent();
        }
    }
}