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
        private readonly KhcnDbNewContext _context;

        public TBKHCNController(KhcnDbNewContext context)
        {
            _context = context;
        }

        // GET ALL
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TBKHCNViewDto>>> GetTBKHCNs()
        {
            var result = await _context.Ttbkhcns
                .AsNoTracking()
                .Select(t => new TBKHCNViewDto
                {
                    Id = t.Id,
                    TenThietBi = t.TenThietBi,
                    NgayDuaVaoSuDung = t.NgayDuaVaoSuDung,
                    NguyenGia = t.NguyenGia,
                    KhauHao = t.KhauHao,
                    GiaTriConLai = t.GiaTriConLai,
                    DT_HD_KHCN_LienQuan = t.DtHdKhcnLienQuan,
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
            var tb = await _context.Ttbkhcns
                .AsNoTracking()
                .Where(t => t.Id == id)
                .Select(t => new TBKHCNViewDto
                {
                    Id = t.Id,
                    TenThietBi = t.TenThietBi,
                    NgayDuaVaoSuDung = t.NgayDuaVaoSuDung,
                    NguyenGia = t.NguyenGia,
                    KhauHao = t.KhauHao,
                    GiaTriConLai = t.GiaTriConLai,
                    DT_HD_KHCN_LienQuan = t.DtHdKhcnLienQuan,
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
        public async Task<ActionResult<Ttbkhcn>> PostTBKHCN([FromBody] TBKHCNCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var tb = new Ttbkhcn
            {
                TenThietBi = dto.TenThietBi,
                NgayDuaVaoSuDung = dto.NgayDuaVaoSuDung,
                NguyenGia = dto.NguyenGia,
                KhauHao = dto.KhauHao,
                GiaTriConLai = dto.GiaTriConLai,
                DtHdKhcnLienQuan = dto.DT_HD_KHCN_LienQuan,
                NhatKySuDung = dto.NhatKySuDung,
                TinhTrangThietBi = dto.TinhTrangThietBi,
                MaThietBi = dto.MaThietBi
            };

            _context.Ttbkhcns.Add(tb);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTBKHCN), new { id = tb.Id }, tb);
        }

        // PUT
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTBKHCN(int id, [FromBody] TBKHCNCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var tb = await _context.Ttbkhcns.FindAsync(id);
            if (tb == null)
                return NotFound();

            tb.TenThietBi = dto.TenThietBi;
            tb.NgayDuaVaoSuDung = dto.NgayDuaVaoSuDung;
            tb.NguyenGia = dto.NguyenGia;
            tb.KhauHao = dto.KhauHao;
            tb.GiaTriConLai = dto.GiaTriConLai;
            tb.DtHdKhcnLienQuan = dto.DT_HD_KHCN_LienQuan;
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
            var tb = await _context.Ttbkhcns.FindAsync(id);
            if (tb == null)
                return NotFound();

            try
            {
                _context.Ttbkhcns.Remove(tb);
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