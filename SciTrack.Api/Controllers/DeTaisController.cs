using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SciTrack.Api.Data;
using SciTrack.Api.DTOs;
using SciTrack.Api.Models;

namespace SciTrack.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeTaisController : ControllerBase
    {
        private readonly KhcnDbNewContext _context;

        public DeTaisController(KhcnDbNewContext context)
        {
            _context = context;
        }

        /// <summary>
        /// GET: api/DeTais - Lấy danh sách tất cả đề tài
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeTaiViewDto>>> GetDeTais()
        {
            var deTais = await _context.Dtkhcns
                .Include(dt => dt.KetQuaDeTaiNavigation)
                .AsNoTracking()
                .Select(dt => new DeTaiViewDto
                {
                    Id = dt.Id,
                    MaDeTai = dt.MaDeTai,
                    Ten = dt.TenDtkhcn,
                    CapNhatTaiSanLanCuoi = dt.NgayCapNhatTaiSan,
                    QuyetDinhThamChieu = dt.CacQuyetDinh,
                    KinhPhiThucHien = dt.KinhPhiThucHien,
                    KinhPhiDaoTao = dt.KinhPhiGiaoKhoanChuyen,
                    KinhPhiTieuHao = dt.KinhPhiVatTuTieuHao,
                    KhauHaoThietBi = dt.HaoMonLienQuan,
                    QuyetDinhXuLyTaiSan = dt.QuyetDinhXuLy,
                    KetQuaDeTai = dt.KetQuaDeTaiNavigation != null ? dt.KetQuaDeTaiNavigation.TenKetQua : null,
                    KetQuaDeTaiId = dt.KetQuaDeTai  
                })
                .ToListAsync();

            return Ok(deTais);
        }

        /// <summary>
        /// GET: api/DeTais/5 - Lấy chi tiết một đề tài theo ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<DeTaiViewDto>> GetDeTai(int id)
        {
            var deTaiDto = await _context.Dtkhcns
                .Include(dt => dt.KetQuaDeTaiNavigation)
                .AsNoTracking()
                .Where(dt => dt.Id == id)
                .Select(dt => new DeTaiViewDto
                {
                    Id = dt.Id,
                    MaDeTai = dt.MaDeTai,
                    Ten = dt.TenDtkhcn,
                    CapNhatTaiSanLanCuoi = dt.NgayCapNhatTaiSan,
                    QuyetDinhThamChieu = dt.CacQuyetDinh,
                    KinhPhiThucHien = dt.KinhPhiThucHien,
                    KinhPhiDaoTao = dt.KinhPhiGiaoKhoanChuyen,
                    KinhPhiTieuHao = dt.KinhPhiVatTuTieuHao,
                    KhauHaoThietBi = dt.HaoMonLienQuan,
                    QuyetDinhXuLyTaiSan = dt.QuyetDinhXuLy,
                    KetQuaDeTai = dt.KetQuaDeTaiNavigation != null ? dt.KetQuaDeTaiNavigation.TenKetQua : null,
                    KetQuaDeTaiId = dt.KetQuaDeTai  
                })
                .FirstOrDefaultAsync();

            if (deTaiDto == null)
            {
                return NotFound(new { message = $"Không tìm thấy đề tài với ID = {id}" });
            }
            
            return Ok(deTaiDto);
        }

        /// <summary>
        /// POST: api/DeTais - Tạo mới một đề tài
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Dtkhcn>> PostDeTai(DeTaiCreateDto deTaiDto)
        {
            if (!string.IsNullOrEmpty(deTaiDto.MaSoDeTai))
            {
                var exists = await _context.Dtkhcns.AnyAsync(dt => dt.MaDeTai == deTaiDto.MaSoDeTai);
                if (exists)
                {
                    return BadRequest(new { message = $"Mã đề tài '{deTaiDto.MaSoDeTai}' đã tồn tại!" });
                }
            }
            if (deTaiDto.KetQuaDeTai.HasValue)
            {
                var ketQuaExists = await _context.Kqdts.AnyAsync(kq => kq.Id == deTaiDto.KetQuaDeTai.Value);
                if (!ketQuaExists)
                {
                    return BadRequest(new { message = $"Kết quả đề tài với ID = {deTaiDto.KetQuaDeTai} không tồn tại!" });
                }
            }
            var maDeTai = !string.IsNullOrEmpty(deTaiDto.MaSoDeTai) 
                ? deTaiDto.MaSoDeTai 
                : $"DT{DateTime.Now:yyyyMMddHHmmss}";
            var newDeTai = new Dtkhcn
            {
                MaDeTai = maDeTai,
                TenDtkhcn = deTaiDto.Ten,
                NgayCapNhatTaiSan = deTaiDto.NgayCapNhatTaiSan,
                CacQuyetDinh = deTaiDto.CacQuyetDinhLienQuan,
                KinhPhiThucHien = deTaiDto.KinhPhiThucHien,
                KinhPhiGiaoKhoanChuyen = deTaiDto.KinhPhiGiaoKhoaChuyen,
                KinhPhiVatTuTieuHao = deTaiDto.KinhPhiVatTuTieuHao,
                HaoMonLienQuan = deTaiDto.HaoMonKhauHaoLienQuan,
                QuyetDinhXuLy = deTaiDto.QuyetDinhXuLyTaiSan,
                KetQuaDeTai = deTaiDto.KetQuaDeTai  
            };

            _context.Dtkhcns.Add(newDeTai);
            await _context.SaveChangesAsync();
            string? tenKetQua = null;
            if (newDeTai.KetQuaDeTai.HasValue)
            {
                var ketQua = await _context.Kqdts
                    .AsNoTracking()
                    .FirstOrDefaultAsync(kq => kq.Id == newDeTai.KetQuaDeTai.Value);
                tenKetQua = ketQua?.TenKetQua;
            }

            return CreatedAtAction(
                nameof(GetDeTai), 
                new { id = newDeTai.Id }, 
                new 
                {
                    id = newDeTai.Id,
                    maDeTai = newDeTai.MaDeTai,
                    ten = newDeTai.TenDtkhcn,
                    ketQuaDeTai = tenKetQua,
                    message = "Tạo đề tài thành công"
                }
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDeTai(int id, DeTaiCreateDto deTaiDto)
        {
            var deTai = await _context.Dtkhcns.FindAsync(id);
            if (deTai == null) 
            { 
                return NotFound(new { message = $"Không tìm thấy đề tài với ID = {id}" });
            }

            if (deTaiDto.KetQuaDeTai.HasValue)
            {
                var ketQuaExists = await _context.Kqdts.AnyAsync(kq => kq.Id == deTaiDto.KetQuaDeTai.Value);
                if (!ketQuaExists)
                {
                    return BadRequest(new { message = $"Kết quả đề tài với ID = {deTaiDto.KetQuaDeTai} không tồn tại!" });
                }
            }

            deTai.TenDtkhcn = deTaiDto.Ten;
            deTai.NgayCapNhatTaiSan = deTaiDto.NgayCapNhatTaiSan;
            deTai.CacQuyetDinh = deTaiDto.CacQuyetDinhLienQuan;
            deTai.KinhPhiThucHien = deTaiDto.KinhPhiThucHien;
            deTai.KinhPhiGiaoKhoanChuyen = deTaiDto.KinhPhiGiaoKhoaChuyen;
            deTai.KinhPhiVatTuTieuHao = deTaiDto.KinhPhiVatTuTieuHao;
            deTai.HaoMonLienQuan = deTaiDto.HaoMonKhauHaoLienQuan;
            deTai.QuyetDinhXuLy = deTaiDto.QuyetDinhXuLyTaiSan;
            deTai.KetQuaDeTai = deTaiDto.KetQuaDeTai; 

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Dtkhcns.Any(e => e.Id == id)) 
                { 
                    return NotFound(new { message = $"Không tìm thấy đề tài với ID = {id}" });
                } 
                else 
                { 
                    throw; 
                }
            }
            
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeTai(int id)
        {
            var deTai = await _context.Dtkhcns.FindAsync(id);
            if (deTai == null) 
            { 
                return NotFound(new { message = $"Không tìm thấy đề tài với ID = {id}" });
            }
            var hasRelatedTaiSan = await _context.Tskhcns.AnyAsync(ts => ts.MaSoDeTaiKhcn == id);
            if (hasRelatedTaiSan)
            {
                var count = await _context.Tskhcns.CountAsync(ts => ts.MaSoDeTaiKhcn == id);
                return BadRequest(new 
                { 
                    message = $"Không thể xóa đề tài '{deTai.MaDeTai}' vì đang có {count} tài sản liên quan. Vui lòng xóa hoặc chuyển các tài sản sang đề tài khác trước.",
                    relatedCount = count,
                    deTaiMa = deTai.MaDeTai
                });
            }

            try
            {
                _context.Dtkhcns.Remove(deTai);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Xóa đề tài thành công" });
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(new 
                { 
                    message = "Không thể xóa đề tài vì có ràng buộc dữ liệu từ các bảng khác.",
                    error = ex.InnerException?.Message ?? ex.Message
                });
            }
        }
    }
}