using System;
using System.ComponentModel.DataAnnotations;

namespace SciTrack.Api.DTOs
{
    public class HopDongCreateDto
    {
        [Required]
        [MaxLength(255)]
        public string TenDoiTac { get; set; }

        public DateTime? NgayHieuLuc { get; set; }
        public DateTime? NgayNghiemThu { get; set; }

        public decimal? TongGiaTriHopDong { get; set; }
        public decimal? ChiPhiKetQuaDeTai { get; set; }
        public decimal? ChiPhiTrangThietBi { get; set; }
        public decimal? ChiPhiHoatDongChuyenMon { get; set; }
        public decimal? LoiNhuan { get; set; }

        [MaxLength(50)]
        public string? MaHopDong { get; set; }
    }
}
