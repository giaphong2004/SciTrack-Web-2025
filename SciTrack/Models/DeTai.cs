namespace SciTrack.web.Models
{
    public class DeTai
    {
        public int Id { get; set; }  // Thêm Id để map với backend
        public string MaDeTai { get; set; } = string.Empty;
        public string Ten { get; set; } = string.Empty;
        public DateOnly? CapNhatTaiSanLanCuoi { get; set; }  // Đổi thành DateOnly? để khớp với API
        public string? QuyetDinhThamChieu { get; set; }
        public decimal? KinhPhiThucHien { get; set; }
        public decimal? KinhPhiDaoTao { get; set; }
        public decimal? KinhPhiTieuHao { get; set; }
        public decimal? KhauHaoThietBi { get; set; }
        public string? QuyetDinhXuLyTaiSan { get; set; }
        
        // Tên kết quả đề tài (để hiển thị)
        public string? KetQuaDeTai { get; set; }
        
        // ID kết quả đề tài (để submit)
        public int? KetQuaDeTaiId { get; set; }
    }
}