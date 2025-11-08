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
        private readonly KhcnDbNewContext _context;

        public KetQuaDeTaiController(KhcnDbNewContext context)
        {
            _context = context;
        }

        // =========================
        // GET: api/KetQuaDeTai
        // =========================
        /// <summary>
        /// GET: api/KetQuaDeTai - Lấy danh sách tất cả kết quả đề tài
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<KetQuaDeTaiViewDto>>> GetKetQuaDeTais()
        {
            var ketQuas = await _context.Kqdts
                .Include(k => k.LienKetKqdtHds)
                .ThenInclude(lk => lk.Hdkhcn)
                .AsNoTracking()
                .Select(k => new KetQuaDeTaiViewDto
                {
                    Id = k.Id,
                    MaKetQua = k.MaKetQua,
                    TenKetQua = k.TenKetQua,
                    PhanLoai = k.PhanLoai,
                    DinhGia = k.DinhGia,
                    GiaTriConLai = k.GiaTriConLai,
                    HopDongIds = k.LienKetKqdtHds.Select(lk => lk.HdkhcnId).ToList(),
                    NgayCapNhatTaiSan = k.NgayCapNhatTaiSan
                })
                .ToListAsync();

            return Ok(ketQuas);
        }

        // =========================
        // GET: api/KetQuaDeTai/{id}
        // =========================
        /// <summary>
        /// GET: api/KetQuaDeTai/{id} - Lấy chi tiết một kết quả đề tài theo ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<KetQuaDeTaiViewDto>> GetKetQuaDeTai(int id)
        {
            var ketQuaDto = await _context.Kqdts
                .Include(k => k.LienKetKqdtHds)
                .ThenInclude(lk => lk.Hdkhcn)
                .AsNoTracking()
                .Where(k => k.Id == id)
                .Select(k => new KetQuaDeTaiViewDto
                {
                    Id = k.Id,
                    MaKetQua = k.MaKetQua,
                    TenKetQua = k.TenKetQua,
                    PhanLoai = k.PhanLoai,
                    DinhGia = k.DinhGia,
                    GiaTriConLai = k.GiaTriConLai,
                    HopDongIds = k.LienKetKqdtHds.Select(lk => lk.HdkhcnId).ToList(),
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
        /// <summary>
        /// POST: api/KetQuaDeTai - Tạo mới một kết quả đề tài
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> PostKetQuaDeTai(KetQuaDeTaiCreateDto dto)
        {
            // Kiểm tra trùng mã kết quả
            if (!string.IsNullOrEmpty(dto.MaKetQua))
            {
                var exists = await _context.Kqdts.AnyAsync(k => k.MaKetQua == dto.MaKetQua);
                if (exists)
                    return BadRequest(new { message = $"Mã kết quả {dto.MaKetQua} đã tồn tại." });
            }

            // Tạo kết quả đề tài mới
            var newKetQua = new Kqdt
            {
                MaKetQua = dto.MaKetQua,
                TenKetQua = dto.TenKetQua,
                PhanLoai = dto.PhanLoai,
                DinhGia = dto.DinhGia,
                GiaTriConLai = dto.GiaTriConLai,
                NgayCapNhatTaiSan = dto.NgayCapNhatTaiSan
            };

            _context.Kqdts.Add(newKetQua);
            await _context.SaveChangesAsync();

            // Thêm liên kết với hợp đồng (nếu có)
            if (dto.HopDongIds != null && dto.HopDongIds.Any())
            {
                foreach (var hopDongId in dto.HopDongIds)
                {
                    var lienKet = new LienKetKqdtHd
                    {
                        KqdtId = newKetQua.Id,
                        HdkhcnId = hopDongId
                    };
                    _context.LienKetKqdtHds.Add(lienKet);
                }
                await _context.SaveChangesAsync();
            }

            return CreatedAtAction(nameof(GetKetQuaDeTai), new { id = newKetQua.Id },
                new { message = "Tạo kết quả đề tài thành công", data = newKetQua });
        }


        // =========================
        // PUT: api/KetQuaDeTai/{id}
        // =========================
        /// <summary>
        /// PUT: api/KetQuaDeTai/{id} - Cập nhật kết quả đề tài
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKetQuaDeTai(int id, KetQuaDeTaiCreateDto dto)
        {
            var ketQua = await _context.Kqdts
                .Include(k => k.LienKetKqdtHds)
                .FirstOrDefaultAsync(k => k.Id == id);
                
            if (ketQua == null)
                return NotFound(new { message = $"Không tìm thấy kết quả với ID = {id}" });

            // Cập nhật thông tin cơ bản
            ketQua.MaKetQua = dto.MaKetQua;
            ketQua.TenKetQua = dto.TenKetQua;
            ketQua.PhanLoai = dto.PhanLoai;
            ketQua.DinhGia = dto.DinhGia;
            ketQua.GiaTriConLai = dto.GiaTriConLai;
            ketQua.NgayCapNhatTaiSan = dto.NgayCapNhatTaiSan;

            // Xóa tất cả liên kết cũ
            _context.LienKetKqdtHds.RemoveRange(ketQua.LienKetKqdtHds);

            // Thêm liên kết mới
            if (dto.HopDongIds != null && dto.HopDongIds.Any())
            {
                foreach (var hopDongId in dto.HopDongIds)
                {
                    var lienKet = new LienKetKqdtHd
                    {
                        KqdtId = ketQua.Id,
                        HdkhcnId = hopDongId
                    };
                    _context.LienKetKqdtHds.Add(lienKet);
                }
            }

            await _context.SaveChangesAsync();
            return Ok(new { message = "Cập nhật kết quả đề tài thành công" });
        }


        // =========================
        // DELETE: api/KetQuaDeTai/{id}
        // =========================
        /// <summary>
        /// DELETE: api/KetQuaDeTai/{id} - Xóa kết quả đề tài
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKetQuaDeTai(int id)
        {
            var ketQua = await _context.Kqdts
                .Include(k => k.LienKetKqdtHds)
                .FirstOrDefaultAsync(k => k.Id == id);
                
            if (ketQua == null)
                return NotFound(new { message = $"Không tìm thấy kết quả với ID = {id}" });

            // Kiểm tra xem có đề tài nào đang tham chiếu đến kết quả này không
            var hasRelatedDeTai = await _context.Dtkhcns.AnyAsync(dt => dt.KetQuaDeTai == id);
            if (hasRelatedDeTai)
            {
                var count = await _context.Dtkhcns.CountAsync(dt => dt.KetQuaDeTai == id);
                return BadRequest(new 
                { 
                    message = $"Không thể xóa kết quả đề tài '{ketQua.TenKetQua}' vì đang có {count} đề tài liên quan. Vui lòng cập nhật các đề tài trước.",
                    relatedCount = count,
                    ketQuaTen = ketQua.TenKetQua
                });
            }

            try
            {
                // Xóa tất cả liên kết với hợp đồng trước
                _context.LienKetKqdtHds.RemoveRange(ketQua.LienKetKqdtHds);
                
                // Xóa kết quả đề tài
                _context.Kqdts.Remove(ketQua);
                await _context.SaveChangesAsync();
                
                return Ok(new { message = "Xóa kết quả đề tài thành công" });
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(new
                {
                    message = "Không thể xóa kết quả đề tài vì có ràng buộc dữ liệu từ các bảng khác.",
                    error = ex.InnerException?.Message ?? ex.Message
                });
            }
        }
    }
}
