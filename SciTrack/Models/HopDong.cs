namespace SciTrack.web.Models
{
    public class HopDong
    {
        public int Id { get; set; }
        public string? MaHopDong { get; set; }
        public string TenDoiTac { get; set; } = string.Empty;
        public DateOnly? NgayHieuLuc { get; set; }
        public DateOnly? NgayNghiemThu { get; set; }
        public decimal? TongGiaTriHopDong { get; set; }
        public decimal? ChiPhiKetQuaDeTai { get; set; }
        public decimal? ChiPhiTrangThietBi { get; set; }
        public decimal? ChiPhiHoatDongChuyenMon { get; set; }
        public decimal? LoiNhuan { get; set; }
    }
}
