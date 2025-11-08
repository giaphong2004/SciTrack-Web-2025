namespace SciTrack.Api.DTOs
{
    // DTO này dùng để "trưng bày" các trường
    public class DeTaiViewDto
    {
        public string MaDeTai { get; set; } = string.Empty;
        public string Ten { get; set; } = string.Empty;
        public DateOnly? CapNhatTaiSanLanCuoi { get; set; }
        public string? QuyetDinhThamChieu { get; set; }
        public decimal? KinhPhiThucHien { get; set; }
        public decimal? KinhPhiDaoTao { get; set; } 
        public decimal? KinhPhiTieuHao { get; set; }
        public decimal? KhauHaoThietBi { get; set; }
        public string? QuyetDinhXuLyTaiSan { get; set; }

        // --- Trường từ "bảng khác" ---
        public string? KetQuaDeTai { get; set; }
    }
}