using System;
using System.ComponentModel.DataAnnotations;

namespace SciTrack.Api.DTOs
{
    public class TBKHCNCreateDto
    {
        [Required]
        public string TenThietBi { get; set; } = string.Empty;

        public DateTime? NgayDuaVaoSuDung { get; set; }
        public decimal? NguyenGia { get; set; }
        public decimal? KhauHao { get; set; }
        public decimal? GiaTriConLai { get; set; }
        public string? DT_HD_KHCN_LienQuan { get; set; }

        public string? NhatKySuDung { get; set; }
        public string? TinhTrangThietBi { get; set; }

        // Gắn đúng với MaSoHopDong trong DB
        public int? MaSoHopDong { get; set; }
    }
}
