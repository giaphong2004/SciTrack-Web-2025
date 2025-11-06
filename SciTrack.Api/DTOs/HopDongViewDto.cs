using System;

namespace SciTrack.Api.DTOs
{
    public class HopDongViewDto
    {
        public int Id { get; set; }
        public string TenDoiTac { get; set; }
        public DateTime? NgayHieuLuc { get; set; }
        public DateTime? NgayNghiemThu { get; set; }
        public decimal? TongGiaTriHopDong { get; set; }
        public decimal? ChiPhiKetQuaDeTai { get; set; }
        public decimal? ChiPhiTrangThietBi { get; set; }
        public decimal? ChiPhiHoatDongChuyenMon { get; set; }
        public decimal? LoiNhuan { get; set; }
        public string? MaHopDong { get; set; }
    }
}
