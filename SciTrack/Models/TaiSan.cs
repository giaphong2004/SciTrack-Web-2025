namespace SciTrack.web.Models
{
    public class TaiSan
    {
        public int Id { get; set; }
        public string SoDanhMuc { get; set; } = string.Empty;
        public string Ten { get; set; } = string.Empty;
        public decimal? NguyenGia { get; set; }
        public decimal? KhauHao { get; set; }
        public decimal? HaoMon { get; set; }
        public decimal? GiaTriConLai { get; set; }
        public string? TrangThaiTaiSan { get; set; }
        public DateOnly? NgayCapNhat { get; set; }  // Đổi thành DateOnly?
        public int? MaDeTaiKHCN { get; set; }  // Đổi thành int? để khớp với API
    }
}
