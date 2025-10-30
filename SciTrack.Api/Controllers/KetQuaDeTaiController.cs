using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SciTrack.Api.Data;
using SciTrack.Api.DTOs;
using SciTrack.Api.Models;

namespace SciTrack.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KetQuaDeTaiController : ControllerBase
    {
        private readonly KHCN_DBContext _context;

        public KetQuaDeTaiController(KHCN_DBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// GET: api/KetQuaDeTai - Lấy danh sách tất cả kết quả đề tài
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<KetQuaDeTaiViewDto>>> GetKetQuaDeTais()
        {
            var ketQuas = await _context.KetQuaDeTais
                .Include(k => k.HopDong)
                .AsNoTracking()
                .Select(k => new KetQuaDeTaiViewDto
                {
                    Id = k.Id,
                    TenKetQua = k.TenKetQua,
                    PhanLoai = k.PhanLoai,
                    DinhGia = k.DinhGia,
                    GiaTriConLai = k.GiaTriConLai,
                    CacHopDong = k.CacHopDong,
                    NgayCapNhatTaiSan = k.NgayCapNhatTaiSan,
                    MaSoThietBi = k.MaSoThietBi,
                    TenHopDong = k.HopDong != null ? k.HopDong.TenDoiTac : "Không có hợp đồng"

                })
                .ToListAsync();

            return Ok(ketQuas);
        }

        /// <summary>
        /// GET: api/KetQuaDeTai/5 - Lấy chi tiết một kết quả đề tài theo ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<KetQuaDeTaiViewDto>> GetKetQuaDeTai(int id)
        {
            var ketQuaDto = await _context.KetQuaDeTais
                .Include(k => k.HopDong)
                .AsNoTracking()
                .Where(k => k.Id == id)
                .Select(k => new KetQuaDeTaiViewDto
                {
                    Id = k.Id,
                    TenKetQua = k.TenKetQua,
                    PhanLoai = k.PhanLoai,
                    DinhGia = k.DinhGia,
                    GiaTriConLai = k.GiaTriConLai,
                    CacHopDong = k.CacHopDong,
                    NgayCapNhatTaiSan = k.NgayCapNhatTaiSan,
                    MaSoThietBi = k.MaSoThietBi,
                    TenHopDong = k.HopDong != null ? k.HopDong.TenDoiTac : "Không có hợp đồng"

                })
                .FirstOrDefaultAsync();

            if (ketQuaDto == null)
            {
                return NotFound(new { message = $"Không tìm thấy kết quả với ID = {id}" });
            }

            return Ok(ketQuaDto);
        }

        /// <summary>
        /// POST: api/KetQuaDeTai - Tạo mới một kết quả đề tài
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<KetQuaDeTaiViewDto>> PostKetQuaDeTai(KetQuaDeTaiCreateDto dto)
        {
            var newKetQua = new KetQuaDeTai
            {
                TenKetQua = dto.TenKetQua,
                PhanLoai = dto.PhanLoai,
                DinhGia = dto.DinhGia,
                GiaTriConLai = dto.GiaTriConLai,
                CacHopDong = dto.CacHopDong,
                NgayCapNhatTaiSan = dto.NgayCapNhatTaiSan ?? DateTime.Now,
                MaSoThietBi = dto.MaSoThietBi
            };

            _context.KetQuaDeTais.Add(newKetQua);
            await _context.SaveChangesAsync();

            // Lấy thông tin hợp đồng nếu có
            string? tenHopDong = null;
            if (newKetQua.MaSoThietBi.HasValue)
            {
                var hopDong = await _context.HopDongs
                    .AsNoTracking()
                    .FirstOrDefaultAsync(hd => hd.Id == newKetQua.MaSoThietBi.Value);
                tenHopDong = hopDong?.TenDoiTac;
            }

            return CreatedAtAction(
                nameof(GetKetQuaDeTai),
                new { id = newKetQua.Id },
                new
                {
                    id = newKetQua.Id,
                    tenKetQua = newKetQua.TenKetQua,
                    maSoThietBi = newKetQua.MaSoThietBi?.ToString(),
                    tenHopDong = tenHopDong,
                    message = "Tạo kết quả đề tài thành công"
                }
            );
        }

        /// <summary>
        /// PUT: api/KetQuaDeTai/5 - Cập nhật kết quả đề tài
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKetQuaDeTai(int id, KetQuaDeTaiCreateDto dto)
        {
            var ketQua = await _context.KetQuaDeTais.FindAsync(id);
            if (ketQua == null)
            {
                return NotFound(new { message = $"Không tìm thấy kết quả với ID = {id}" });
            }

            ketQua.TenKetQua = dto.TenKetQua;
            ketQua.PhanLoai = dto.PhanLoai;
            ketQua.DinhGia = dto.DinhGia;
            ketQua.GiaTriConLai = dto.GiaTriConLai;
            ketQua.CacHopDong = dto.CacHopDong;
            ketQua.NgayCapNhatTaiSan = dto.NgayCapNhatTaiSan ?? DateTime.Now;
            ketQua.MaSoThietBi = dto.MaSoThietBi;

            _context.Entry(ketQua).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// DELETE: api/KetQuaDeTai/5 - Xóa kết quả đề tài
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKetQuaDeTai(int id)
        {
            var ketQua = await _context.KetQuaDeTais.FindAsync(id);
            if (ketQua == null)
            {
                return NotFound(new { message = $"Không tìm thấy kết quả với ID = {id}" });
            }

            _context.KetQuaDeTais.Remove(ketQua);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Xóa kết quả đề tài thành công" });
        }
    }
}