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
                    MaDeTai = dt.MaDeTai,
                    Ten = dt.TenDtkhcn,
                    CapNhatTaiSanLanCuoi = dt.NgayCapNhatTaiSan,
                    QuyetDinhThamChieu = dt.CacQuyetDinh,
                    KinhPhiThucHien = dt.KinhPhiThucHien,
                    KinhPhiDaoTao = dt.KinhPhiGiaoKhoanChuyen,
                    KinhPhiTieuHao = dt.KinhPhiVatTuTieuHao,
                    KhauHaoThietBi = dt.HaoMonLienQuan,
                    QuyetDinhXuLyTaiSan = dt.QuyetDinhXuLy,
                    KetQuaDeTai = dt.KetQuaDeTaiNavigation != null ? dt.KetQuaDeTaiNavigation.TenKetQua : null
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
                    MaDeTai = dt.MaDeTai,
                    Ten = dt.TenDtkhcn,
                    CapNhatTaiSanLanCuoi = dt.NgayCapNhatTaiSan,
                    QuyetDinhThamChieu = dt.CacQuyetDinh,
                    KinhPhiThucHien = dt.KinhPhiThucHien,
                    KinhPhiDaoTao = dt.KinhPhiGiaoKhoanChuyen,
                    KinhPhiTieuHao = dt.KinhPhiVatTuTieuHao,
                    KhauHaoThietBi = dt.HaoMonLienQuan,
                    QuyetDinhXuLyTaiSan = dt.QuyetDinhXuLy,
                    KetQuaDeTai = dt.KetQuaDeTaiNavigation != null ? dt.KetQuaDeTaiNavigation.TenKetQua : null
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
            // Bước 1: Tìm hoặc tạo mới KetQuaDeTai nếu có
            Kqdt? ketQua = null;
            if (!string.IsNullOrEmpty(deTaiDto.KetQuaDeTai))
            {
                // Tìm kết quả đề tài đã tồn tại
                ketQua = await _context.Kqdts
                    .FirstOrDefaultAsync(kq => kq.TenKetQua == deTaiDto.KetQuaDeTai);

                // Nếu chưa có thì tạo mới
                if (ketQua == null)
                {
                    ketQua = new Kqdt
                    {
                        TenKetQua = deTaiDto.KetQuaDeTai,
                        NgayCapNhatTaiSan = deTaiDto.NgayCapNhatTaiSan
                    };
                    _context.Kqdts.Add(ketQua);
                    await _context.SaveChangesAsync(); // Lưu để lấy ID
                }
            }

            // Bước 2: Generate mã đề tài (nếu cần)
            var maDeTai = $"DT{DateTime.Now:yyyyMMddHHmmss}";

            // Bước 3: Tạo đề tài mới
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
                KetQuaDeTai = ketQua?.Id     // Foreign key đến bảng KQDT
            };

            _context.Dtkhcns.Add(newDeTai);
            await _context.SaveChangesAsync();

            // Bước 4: Trả về response với thông tin đề tài vừa tạo
            return CreatedAtAction(
                nameof(GetDeTai), 
                new { id = newDeTai.Id }, 
                new 
                {
                    maDeTai = newDeTai.MaDeTai,
                    ten = newDeTai.TenDtkhcn,
                    ketQuaDeTai = ketQua?.TenKetQua,
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

            // Tìm hoặc tạo mới KetQuaDeTai
            Kqdt? ketQua = null;
            if (!string.IsNullOrEmpty(deTaiDto.KetQuaDeTai))
            {
                ketQua = await _context.Kqdts
                    .FirstOrDefaultAsync(kq => kq.TenKetQua == deTaiDto.KetQuaDeTai);

                if (ketQua == null)
                {
                    ketQua = new Kqdt
                    {
                        TenKetQua = deTaiDto.KetQuaDeTai,
                        NgayCapNhatTaiSan = deTaiDto.NgayCapNhatTaiSan
                    };
                    _context.Kqdts.Add(ketQua);
                    await _context.SaveChangesAsync();
                }
            }

            // Cập nhật thông tin đề tài
            deTai.TenDtkhcn = deTaiDto.Ten;
            deTai.NgayCapNhatTaiSan = deTaiDto.NgayCapNhatTaiSan;
            deTai.CacQuyetDinh = deTaiDto.CacQuyetDinhLienQuan;
            deTai.KinhPhiThucHien = deTaiDto.KinhPhiThucHien;
            deTai.KinhPhiGiaoKhoanChuyen = deTaiDto.KinhPhiGiaoKhoaChuyen;
            deTai.KinhPhiVatTuTieuHao = deTaiDto.KinhPhiVatTuTieuHao;
            deTai.HaoMonLienQuan = deTaiDto.HaoMonKhauHaoLienQuan;
            deTai.QuyetDinhXuLy = deTaiDto.QuyetDinhXuLyTaiSan;
            deTai.KetQuaDeTai = ketQua?.Id;

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

            _context.Dtkhcns.Remove(deTai);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Xóa đề tài thành công" });
        }
    }
}