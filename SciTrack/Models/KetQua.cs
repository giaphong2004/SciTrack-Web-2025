namespace SciTrack.web.Models
{
    public class KetQua
    {
        public int Id { get; set; }
        public string? MaKetQua { get; set; }
        public string TenKetQua { get; set; } = string.Empty;
        public string? PhanLoai { get; set; }
        public decimal? DinhGia { get; set; }
        public decimal? GiaTriConLai { get; set; }
        public string? CacHopDong { get; set; }
        public DateOnly? NgayCapNhatTaiSan { get; set; }  // Đổi thành DateOnly? để khớp với API
    }
}
