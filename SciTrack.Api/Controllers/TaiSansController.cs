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
        private readonly KhcnDbNewContext _context;

        public TaiSansController(KhcnDbNewContext context)
        {
            _context = context;
        }

        /// <summary>
        /// GET: api/TaiSans - Lấy danh sách tất cả tài sản
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaiSanViewDto>>> GetTaiSans()
        {
            var taiSans = await _context.Tskhcns
                .Include(ts => ts.MaSoDeTaiKhcnNavigation)
                .AsNoTracking()
                .Select(ts => new TaiSanViewDto
                {
                    Id = ts.Id,
                    SoDanhMuc = ts.SoDanhMuc,
                    Ten = ts.Ten,
                    NguyenGia = ts.NguyenGia,
                    KhauHao = ts.KhauHao,
                    HaoMon = ts.HaoMon,
                    GiaTriConLai = ts.GiaTriConLai,
                    TrangThaiTaiSan = ts.TrangThaiTaiSan,
                    NgayCapNhat = ts.NgayCapNhat,
                    MaDeTaiKHCN = ts.MaSoDeTaiKhcn  
                })
                .ToListAsync();

            return Ok(taiSans);
        }

        /// <summary>
        /// GET: api/TaiSans/5 - Lấy chi tiết một tài sản theo ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<TaiSanViewDto>> GetTaiSan(int id)
        {
            var taiSanDto = await _context.Tskhcns
                .Include(ts => ts.MaSoDeTaiKhcnNavigation)
                .AsNoTracking()
                .Where(ts => ts.Id == id)
                .Select(ts => new TaiSanViewDto
                {
                    Id = ts.Id,
                    SoDanhMuc = ts.SoDanhMuc,
                    Ten = ts.Ten,
                    NguyenGia = ts.NguyenGia,
                    KhauHao = ts.KhauHao,
                    HaoMon = ts.HaoMon,
                    GiaTriConLai = ts.GiaTriConLai,
                    TrangThaiTaiSan = ts.TrangThaiTaiSan,
                    NgayCapNhat = ts.NgayCapNhat,
                    MaDeTaiKHCN = ts.MaSoDeTaiKhcn  
                })
                .FirstOrDefaultAsync();

            if (taiSanDto == null)
            {
                return NotFound(new { message = $"Không tìm thấy tài sản với ID = {id}" });
            }

            return Ok(taiSanDto);
        }

        /// <summary>
        /// POST: api/TaiSans - Tạo mới một tài sản
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Tskhcn>> PostTaiSan(TaiSanCreateDto taiSanDto)
        {
            var newTaiSan = new Tskhcn
            {
                SoDanhMuc = taiSanDto.SoDanhMuc,
                Ten = taiSanDto.Ten,
                NguyenGia = taiSanDto.NguyenGia,
                KhauHao = taiSanDto.KhauHao,
                HaoMon = taiSanDto.HaoMon,
                GiaTriConLai = taiSanDto.GiaTriConLai,
                TrangThaiTaiSan = taiSanDto.TrangThaiTaiSan,
                NgayCapNhat = taiSanDto.NgayCapNhat,
                MaSoDeTaiKhcn = taiSanDto.MaDeTaiKHCN
            };

            _context.Tskhcns.Add(newTaiSan);
            await _context.SaveChangesAsync();
            string? tenDeTai = null;
            if (newTaiSan.MaSoDeTaiKhcn.HasValue)
            {
                var deTai = await _context.Dtkhcns
                    .AsNoTracking()
                    .FirstOrDefaultAsync(dt => dt.Id == newTaiSan.MaSoDeTaiKhcn.Value);
                tenDeTai = deTai?.TenDtkhcn;
            }

            return CreatedAtAction(
                nameof(GetTaiSan),
                new { id = newTaiSan.Id },
                new
                {
                    soDanhMuc = newTaiSan.SoDanhMuc,
                    ten = newTaiSan.Ten,
                    maDeTaiKHCN = newTaiSan.MaSoDeTaiKhcn?.ToString(),
                    tenDeTaiKHCN = tenDeTai,
                    message = "Tạo tài sản thành công"
                }
            );
        }

        /// <summary>
        /// PUT: api/TaiSans/5 - Cập nhật tài sản
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaiSan(int id, TaiSanCreateDto taiSanDto)
        {
            var taiSan = await _context.Tskhcns.FindAsync(id);
            if (taiSan == null)
            {
                return NotFound(new { message = $"Không tìm thấy tài sản với ID = {id}" });
            }

            taiSan.SoDanhMuc = taiSanDto.SoDanhMuc;
            taiSan.Ten = taiSanDto.Ten;
            taiSan.NguyenGia = taiSanDto.NguyenGia;
            taiSan.KhauHao = taiSanDto.KhauHao;
            taiSan.HaoMon = taiSanDto.HaoMon;
            taiSan.GiaTriConLai = taiSanDto.GiaTriConLai;
            taiSan.TrangThaiTaiSan = taiSanDto.TrangThaiTaiSan;
            taiSan.NgayCapNhat = taiSanDto.NgayCapNhat;
            taiSan.MaSoDeTaiKhcn = taiSanDto.MaDeTaiKHCN;

            _context.Entry(taiSan).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Tskhcns.Any(e => e.Id == id))
                {
                    return NotFound(new { message = $"Không tìm thấy tài sản với ID = {id}" });
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// DELETE: api/TaiSans/5 - Xóa tài sản
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaiSan(int id)
        {
            var taiSan = await _context.Tskhcns.FindAsync(id);
            if (taiSan == null)
            {
                return NotFound(new { message = $"Không tìm thấy tài sản với ID = {id}" });
            }

            _context.Tskhcns.Remove(taiSan);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Xóa tài sản thành công" });
        }
    }
}