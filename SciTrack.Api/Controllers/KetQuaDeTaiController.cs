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

        // =========================
        // GET: api/KetQuaDeTai
        // =========================
        [HttpGet]
        public async Task<ActionResult<IEnumerable<KetQuaDeTaiViewDto>>> GetKetQuaDeTais()
        {
            var ketQuas = await _context.KetQuaDeTais
                .AsNoTracking()
                .Select(k => new KetQuaDeTaiViewDto
                {
                    Id = k.Id,
                    TenKetQua = k.TenKetQua,
                    PhanLoai = k.PhanLoai,
                    DinhGia = k.DinhGia,
                    GiaTriConLai = k.GiaTriConLai,
                    CacHopDong = k.CacHopDong,
                    NgayCapNhatTaiSan = k.NgayCapNhatTaiSan
                })
                .ToListAsync();

            return Ok(ketQuas);
        }

        // =========================
        // GET: api/KetQuaDeTai/{id}
        // =========================
        [HttpGet("{id}")]
        public async Task<ActionResult<KetQuaDeTaiViewDto>> GetKetQuaDeTai(int id)
        {
            var ketQuaDto = await _context.KetQuaDeTais
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
                    NgayCapNhatTaiSan = k.NgayCapNhatTaiSan
                })
                .FirstOrDefaultAsync();

            if (ketQuaDto == null)
                return NotFound(new { message = $"Không tìm thấy kết quả với ID = {id}" });

            return Ok(ketQuaDto);
        }

        // =========================
        // POST: api/KetQuaDeTai
        // =========================
        [HttpPost]
        public async Task<ActionResult> PostKetQuaDeTai(KetQuaDeTaiCreateDto dto)
        {
            // Kiểm tra trùng mã kết quả
            var exists = await _context.KetQuaDeTais.AnyAsync(k => k.MaKetQua == dto.MaKetQua);
            if (exists)
                return BadRequest(new { message = $"Mã kết quả {dto.MaKetQua} đã tồn tại." });

            var newKetQua = new KetQuaDeTai
            {
                MaKetQua = dto.MaKetQua,
                TenKetQua = dto.TenKetQua,
                PhanLoai = dto.PhanLoai,
                DinhGia = dto.DinhGia,
                GiaTriConLai = dto.GiaTriConLai,
                CacHopDong = dto.CacHopDong,
                NgayCapNhatTaiSan = dto.NgayCapNhatTaiSan ?? DateTime.Now,
            };

            _context.KetQuaDeTais.Add(newKetQua);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetKetQuaDeTai), new { id = newKetQua.Id },
                new { message = "Tạo kết quả đề tài thành công", data = newKetQua });
        }


        // =========================
        // PUT: api/KetQuaDeTai/{id}
        // =========================
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKetQuaDeTai(int id, KetQuaDeTaiCreateDto dto)
        {
            var ketQua = await _context.KetQuaDeTais.FindAsync(id);
            if (ketQua == null)
                return NotFound(new { message = $"Không tìm thấy kết quả với ID = {id}" });

            ketQua.MaKetQua = dto.MaKetQua;
            ketQua.TenKetQua = dto.TenKetQua;
            ketQua.PhanLoai = dto.PhanLoai;
            ketQua.DinhGia = dto.DinhGia;
            ketQua.GiaTriConLai = dto.GiaTriConLai;
            ketQua.CacHopDong = dto.CacHopDong;
            ketQua.NgayCapNhatTaiSan = dto.NgayCapNhatTaiSan ?? DateTime.Now;

            await _context.SaveChangesAsync();
            return Ok(new { message = "Cập nhật kết quả đề tài thành công" });
        }


        // =========================
        // DELETE: api/KetQuaDeTai/{id}
        // =========================
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKetQuaDeTai(int id)
        {
            var ketQua = await _context.KetQuaDeTais.FindAsync(id);
            if (ketQua == null)
                return NotFound(new { message = $"Không tìm thấy kết quả với ID = {id}" });

            try
            {
                _context.KetQuaDeTais.Remove(ketQua);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Xóa kết quả đề tài thành công" });
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.Contains("REFERENCE"))
                {
                    return BadRequest(new
                    {
                        message = "Không thể xóa vì kết quả đề tài này đang được tham chiếu ở bảng Đề Tài."
                    });
                }

                return StatusCode(500, new
                {
                    message = "Đã xảy ra lỗi trong quá trình xóa kết quả đề tài.",
                    detail = ex.Message
                });
            }
        }
    }
}
