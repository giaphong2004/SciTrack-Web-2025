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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeTaiViewDto>>> GetDeTais()
        {
            var deTais = await _context.DeTais
                .AsNoTracking()
                .Select(dt => new DeTaiViewDto
                {
                    Id = dt.Id,
                    Ten = dt.Ten,
                    MaDeTai = dt.MaDeTai,
                    CapNhatTaiSanLanCuoi = dt.CapNhatTaiSanLanCuoi,
                    QuyetDinhThamChieu = dt.QuyetDinhThamChieu,
                    KinhPhiThucHien = dt.KinhPhiThucHien,
                    KinhPhiDaoTao = dt.KinhPhiDaoTao,
                    KinhPhiTieuHao = dt.KinhPhiTieuHao,
                    KhauHaoThietBi = dt.KhauHaoThietBi,
                    QuyetDinhXuLyTaiSan = dt.QuyetDinhXuLyTaiSan,
                    NgayTao = dt.NgayTao,
                    NgayCapNhat = dt.NgayCapNhat,
                    KetQuaDeTai = dt.KetQuaDeTais.Select(kq => kq.Ten).FirstOrDefault()
                })
                .ToListAsync();

            return Ok(deTais);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DeTaiViewDto>> GetDeTai(int id)
        {
            var deTaiDto = await _context.DeTais
                .AsNoTracking()
                .Where(dt => dt.Id == id)
                .Select(dt => new DeTaiViewDto
                {.
                    Id = dt.Id,
                    Ten = dt.Ten,
                    MaDeTai = dt.MaDeTai,
                    CapNhatTaiSanLanCuoi = dt.CapNhatTaiSanLanCuoi,
                    QuyetDinhThamChieu = dt.QuyetDinhThamChieu,
                    KinhPhiThucHien = dt.KinhPhiThucHien,
                    KinhPhiDaoTao = dt.KinhPhiDaoTao,
                    KinhPhiTieuHao = dt.KinhPhiTieuHao,
                    KhauHaoThietBi = dt.KhauHaoThietBi,
                    QuyetDinhXuLyTaiSan = dt.QuyetDinhXuLyTaiSan,
                    NgayTao = dt.NgayTao,
                    NgayCapNhat = dt.NgayCapNhat,
                    KetQuaDeTai = dt.KetQuaDeTais.Select(kq => kq.Ten).FirstOrDefault()
                })
                .FirstOrDefaultAsync();

            if (deTaiDto == null)
            {
                return NotFound();
            }
            return Ok(deTaiDto);
        }

        [HttpPost]
        public async Task<ActionResult<DeTai>> PostDeTai(DeTaiCreateDto deTaiDto)
        {
            var newDeTai = new DeTai
            {
                Ten = deTaiDto.Ten,
                MaDeTai = deTaiDto.MaSoDeTai,
                CapNhatTaiSanLanCuoi = deTaiDto.NgayCapNhatTaiSan,
                QuyetDinhThamChieu = deTaiDto.CacQuyetDinhLienQuan,
                KinhPhiThucHien = deTaiDto.KinhPhiThucHien,
                KinhPhiDaoTao = deTaiDto.KinhPhiGiaoKhoaChuyen,
                KinhPhiTieuHao = deTaiDto.KinhPhiVatTuTieuHao,
                KhauHaoThietBi = deTaiDto.HaoMonKhauHaoLienQuan,
                NgayTao = DateTime.UtcNow,
                QuyetDinhXuLyTaiSan = deTaiDto.QuyetDinhXuLyTaiSan
            };
            _context.DeTais.Add(newDeTai);
            if (!string.IsNullOrEmpty(deTaiDto.KetQuaDeTai))
            {
                var newKetQuaDeTai = new KetQuaDeTai
                {
                    Ten = deTaiDto.KetQuaDeTai,
                    DeTai = newDeTai 
                };
                _context.KetQuaDeTais.Add(newKetQuaDeTai);
            }

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDeTai), new { id = newDeTai.Id }, newDeTai);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDeTai(int id, DeTaiCreateDto deTaiDto)
        {
            var deTai = await _context.DeTais.FindAsync(id);
            if (deTai == null) { return NotFound(); }

            deTai.Ten = deTaiDto.Ten;
            deTai.MaDeTai = deTaiDto.MaSoDeTai;
            deTai.CapNhatTaiSanLanCuoi = deTaiDto.NgayCapNhatTaiSan;
            deTai.QuyetDinhThamChieu = deTaiDto.CacQuyetDinhLienQuan;
            deTai.KinhPhiThucHien = deTaiDto.KinhPhiThucHien;
            deTai.KinhPhiDaoTao = deTaiDto.KinhPhiGiaoKhoaChuyen;
            deTai.KinhPhiTieuHao = deTaiDto.KinhPhiVatTuTieuHao;
            deTai.KhauHaoThietBi = deTaiDto.HaoMonKhauHaoLienQuan;
            deTai.NgayCapNhat = DateTime.UtcNow;
            deTai.QuyetDinhXuLyTaiSan = deTaiDto.QuyetDinhXuLyTaiSan;

            var ketQua = await _context.KetQuaDeTais.FirstOrDefaultAsync(kq => kq.DeTaiId == id);
            if (ketQua != null)
            {
                ketQua.Ten = deTaiDto.KetQuaDeTai;
                _context.Entry(ketQua).State = EntityState.Modified;
            }
            else if (!string.IsNullOrEmpty(deTaiDto.KetQuaDeTai))
            {
                _context.KetQuaDeTais.Add(new KetQuaDeTai
                {
                    Ten = deTaiDto.KetQuaDeTai,
                    DeTaiId = id
                });
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.DeTais.Any(e => e.Id == id)) { return NotFound(); } else { throw; }
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeTai(int id)
        {
            var deTai = await _context.DeTais.FindAsync(id);
            if (deTai == null) { return NotFound(); }
            _context.DeTais.Remove(deTai);
            var ketQuas = await _context.KetQuaDeTais.Where(kq => kq.DeTaiId == id).ToListAsync();
            _context.KetQuaDeTais.RemoveRange(ketQuas);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}