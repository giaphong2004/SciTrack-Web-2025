namespace SciTrack.Api.DTOs
{
    // DTO này dùng để "trưng bày" 13 trường
    public class DeTaiViewDto
    {
        public int Id { get; set; } // Cột quan trọng nhất
        public string Ten { get; set; } = string.Empty;
        public string? MaDeTai { get; set; }
        public DateTime? CapNhatTaiSanLanCuoi { get; set; }
        public string? QuyetDinhThamChieu { get; set; }
        public decimal? KinhPhiThucHien { get; set; }
        public decimal? KinhPhiDaoTao { get; set; } 
        public decimal? KinhPhiTieuHao { get; set; }
        public decimal? KhauHaoThietBi { get; set; }
        public string? QuyetDinhXuLyTaiSan { get; set; }

        // --- Trường từ "bảng khác" ---
        public string? KetQuaDeTai { get; set; }

        // --- 2 trường Server-tự-tạo ---
        public DateTime? NgayTao { get; set; }
        public DateTime? NgayCapNhat { get; set; }
    }
}