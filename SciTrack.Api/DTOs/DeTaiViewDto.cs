namespace SciTrack.Api.DTOs
{

    public class DeTaiViewDto
    {
        public int Id { get; set; }  
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
        public int? KetQuaDeTaiId { get; set; }
    }
}