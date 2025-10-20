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
        private readonly KHCN_DBContext _context;

        public DeTaisController(KHCN_DBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// GET: api/DeTais - Lấy danh sách tất cả đề tài
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeTaiViewDto>>> GetDeTais()
        {
            var deTais = await _context.DeTais
                .Include(dt => dt.KetQua)
                .AsNoTracking()
                .Select(dt => new DeTaiViewDto
                {
                    MaDeTai = dt.Id.ToString(), // Mã đề tài = ID
                    Ten = dt.TenDTKHCN,
                    CapNhatTaiSanLanCuoi = dt.NgayCapNhatTaiSan,
                    QuyetDinhThamChieu = dt.CacQuyetDinh,
                    KinhPhiThucHien = dt.KinhPhiThucHien,
                    KinhPhiDaoTao = dt.KinhPhiGiaoKhoanChuyen,
                    KinhPhiTieuHao = dt.KinhPhiVatTuTieuHao,
                    KhauHaoThietBi = dt.HaoMonLienQuan,
                    QuyetDinhXuLyTaiSan = dt.QuyetDinhXuLy,
                    KetQuaDeTai = dt.KetQua != null ? dt.KetQua.TenKetQua : null
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
            var deTaiDto = await _context.DeTais
                .Include(dt => dt.KetQua)
                .AsNoTracking()
                .Where(dt => dt.Id == id)
                .Select(dt => new DeTaiViewDto
                {
                    MaDeTai = dt.Id.ToString(), // Mã đề tài = ID
                    Ten = dt.TenDTKHCN,
                    CapNhatTaiSanLanCuoi = dt.NgayCapNhatTaiSan,
                    QuyetDinhThamChieu = dt.CacQuyetDinh,
                    KinhPhiThucHien = dt.KinhPhiThucHien,
                    KinhPhiDaoTao = dt.KinhPhiGiaoKhoanChuyen,
                    KinhPhiTieuHao = dt.KinhPhiVatTuTieuHao,
                    KhauHaoThietBi = dt.HaoMonLienQuan,
                    QuyetDinhXuLyTaiSan = dt.QuyetDinhXuLy,
                    KetQuaDeTai = dt.KetQua != null ? dt.KetQua.TenKetQua : null
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
        public async Task<ActionResult<DeTai>> PostDeTai(DeTaiCreateDto deTaiDto)
        {
            // Bước 1: Tìm hoặc tạo mới KetQuaDeTai nếu có
            KetQuaDeTai? ketQua = null;
            if (!string.IsNullOrEmpty(deTaiDto.KetQuaDeTai))
            {
                // Tìm kết quả đề tài đã tồn tại
                ketQua = await _context.KetQuaDeTais
                    .FirstOrDefaultAsync(kq => kq.TenKetQua == deTaiDto.KetQuaDeTai);

                // Nếu chưa có thì tạo mới
                if (ketQua == null)
                {
                    ketQua = new KetQuaDeTai
                    {
                        TenKetQua = deTaiDto.KetQuaDeTai,
                        NgayCapNhatTaiSan = deTaiDto.NgayCapNhatTaiSan
                    };
                    _context.KetQuaDeTais.Add(ketQua);
                    await _context.SaveChangesAsync(); // Lưu để lấy ID
                }
            }

            // Bước 2: Tạo đề tài mới
            var newDeTai = new DeTai
            {
                TenDTKHCN = deTaiDto.Ten,
                NgayCapNhatTaiSan = deTaiDto.NgayCapNhatTaiSan,
                CacQuyetDinh = deTaiDto.CacQuyetDinhLienQuan,
                KinhPhiThucHien = deTaiDto.KinhPhiThucHien,
                KinhPhiGiaoKhoanChuyen = deTaiDto.KinhPhiGiaoKhoaChuyen,
                KinhPhiVatTuTieuHao = deTaiDto.KinhPhiVatTuTieuHao,
                HaoMonLienQuan = deTaiDto.HaoMonKhauHaoLienQuan,
                QuyetDinhXuLy = deTaiDto.QuyetDinhXuLyTaiSan,
                KetQuaDeTai = ketQua?.Id,     // Foreign key đến bảng KQDT
                MaSoKetQua = ketQua?.Id        // Tham chiếu ID kết quả
            };

            _context.DeTais.Add(newDeTai);
            await _context.SaveChangesAsync();

            // Bước 3: Trả về response với thông tin đề tài vừa tạo
            return CreatedAtAction(
                nameof(GetDeTai), 
                new { id = newDeTai.Id }, 
                new 
                {
                    maDeTai = newDeTai.Id.ToString(),
                    ten = newDeTai.TenDTKHCN,
                    ketQuaDeTai = ketQua?.TenKetQua,
                    message = "Tạo đề tài thành công"
                }
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDeTai(int id, DeTaiCreateDto deTaiDto)
        {
            var deTai = await _context.DeTais.FindAsync(id);
            if (deTai == null) 
            { 
                return NotFound(new { message = $"Không tìm thấy đề tài với ID = {id}" });
            }

            // Tìm hoặc tạo mới KetQuaDeTai
            KetQuaDeTai? ketQua = null;
            if (!string.IsNullOrEmpty(deTaiDto.KetQuaDeTai))
            {
                ketQua = await _context.KetQuaDeTais
                    .FirstOrDefaultAsync(kq => kq.TenKetQua == deTaiDto.KetQuaDeTai);

                if (ketQua == null)
                {
                    ketQua = new KetQuaDeTai
                    {
                        TenKetQua = deTaiDto.KetQuaDeTai,
                        NgayCapNhatTaiSan = deTaiDto.NgayCapNhatTaiSan
                    };
                    _context.KetQuaDeTais.Add(ketQua);
                    await _context.SaveChangesAsync();
                }
            }

            // Cập nhật thông tin đề tài
            deTai.TenDTKHCN = deTaiDto.Ten;
            deTai.NgayCapNhatTaiSan = deTaiDto.NgayCapNhatTaiSan;
            deTai.CacQuyetDinh = deTaiDto.CacQuyetDinhLienQuan;
            deTai.KinhPhiThucHien = deTaiDto.KinhPhiThucHien;
            deTai.KinhPhiGiaoKhoanChuyen = deTaiDto.KinhPhiGiaoKhoaChuyen;
            deTai.KinhPhiVatTuTieuHao = deTaiDto.KinhPhiVatTuTieuHao;
            deTai.HaoMonLienQuan = deTaiDto.HaoMonKhauHaoLienQuan;
            deTai.QuyetDinhXuLy = deTaiDto.QuyetDinhXuLyTaiSan;
            deTai.KetQuaDeTai = ketQua?.Id;
            deTai.MaSoKetQua = ketQua?.Id;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.DeTais.Any(e => e.Id == id)) 
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
            var deTai = await _context.DeTais.FindAsync(id);
            if (deTai == null) 
            { 
                return NotFound(new { message = $"Không tìm thấy đề tài với ID = {id}" });
            }

            _context.DeTais.Remove(deTai);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Xóa đề tài thành công" });
        }
    }
}