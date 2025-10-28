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

        /// <summary>
        /// GET: api/TaiSans - Lấy danh sách tất cả tài sản
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaiSanViewDto>>> GetTaiSans()
        {
            var taiSans = await _context.TaiSans
                .Include(ts => ts.DeTai)
                .AsNoTracking()
                .Select(ts => new TaiSanViewDto
                {
                    Id = ts.Id,
                    SoDanhMuc = ts.SoDanhMuc, // Số danh mục = ID
                    Ten = ts.Ten,
                    NguyenGia = ts.NguyenGia,
                    KhauHao = ts.KhauHao,
                    HaoMon = ts.HaoMon,
                    GiaTriConLai = ts.GiaTriConLai,
                    TrangThaiTaiSan = ts.TrangThaiTaiSan,
                    NgayCapNhat = ts.NgayCapNhat,
                    MaDeTaiKHCN = ts.MaSoDeTaiKHCN.HasValue ? ts.MaSoDeTaiKHCN.Value.ToString() : null,
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
            var taiSanDto = await _context.TaiSans
                .Include(ts => ts.DeTai)
                .AsNoTracking()
                .Where(ts => ts.Id == id)
                .Select(ts => new TaiSanViewDto
                {
                    Id = ts.Id,
                    SoDanhMuc = ts.SoDanhMuc, // Số danh mục = ID
                    Ten = ts.Ten,
                    NguyenGia = ts.NguyenGia,
                    KhauHao = ts.KhauHao,
                    HaoMon = ts.HaoMon,
                    GiaTriConLai = ts.GiaTriConLai,
                    TrangThaiTaiSan = ts.TrangThaiTaiSan,
                    NgayCapNhat = ts.NgayCapNhat,
                    MaDeTaiKHCN = ts.MaSoDeTaiKHCN.HasValue ? ts.MaSoDeTaiKHCN.Value.ToString() : null,
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
                TrangThaiTaiSan = taiSanDto.TrangThaiTaiSan,
                NgayCapNhat = taiSanDto.NgayCapNhat ?? DateTime.Now,
                MaSoDeTaiKHCN = taiSanDto.MaDeTaiKHCN,
                MaSoDeTaiKHCN2 = taiSanDto.MaDeTaiKHCN
            };

            _context.TaiSans.Add(newTaiSan);
            await _context.SaveChangesAsync();

            // Lấy thông tin đề tài nếu có
            string? tenDeTai = null;
            if (newTaiSan.MaSoDeTaiKHCN.HasValue)
            {
                var deTai = await _context.DeTais
                    .AsNoTracking()
                    .FirstOrDefaultAsync(dt => dt.Id == newTaiSan.MaSoDeTaiKHCN.Value);
                tenDeTai = deTai?.TenDTKHCN;
            }

            return CreatedAtAction(
                nameof(GetTaiSan),
                new { id = newTaiSan.Id },
                new
                {
                    soDanhMuc = newTaiSan.Id.ToString(),
                    ten = newTaiSan.Ten,
                    maDeTaiKHCN = newTaiSan.MaSoDeTaiKHCN?.ToString(),
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
            var taiSan = await _context.TaiSans.FindAsync(id);
            if (taiSan == null)
            {
                return NotFound(new { message = $"Không tìm thấy tài sản với ID = {id}" });
            }

            // Cập nhật thông tin
            taiSan.Ten = taiSanDto.Ten;
            taiSan.NguyenGia = taiSanDto.NguyenGia;
            taiSan.KhauHao = taiSanDto.KhauHao;
            taiSan.HaoMon = taiSanDto.HaoMon;
            taiSan.GiaTriConLai = taiSanDto.GiaTriConLai;
            taiSan.TrangThaiTaiSan = taiSanDto.TrangThaiTaiSan;
            taiSan.NgayCapNhat = taiSanDto.NgayCapNhat ?? DateTime.Now;
            taiSan.MaSoDeTaiKHCN = taiSanDto.MaDeTaiKHCN;
            taiSan.MaSoDeTaiKHCN2 = taiSanDto.MaDeTaiKHCN;

            _context.Entry(taiSan).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.TaiSans.Any(e => e.Id == id))
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
            var taiSan = await _context.TaiSans.FindAsync(id);
            if (taiSan == null)
            {
                return NotFound(new { message = $"Không tìm thấy tài sản với ID = {id}" });
            }

            _context.TaiSans.Remove(taiSan);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Xóa tài sản thành công" });
        }
    }
}