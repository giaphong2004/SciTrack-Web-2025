using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SciTrack.Api.Models
{
    [Table("TTBKHCN")]
    public class TBKHCN
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(255)]
        public string TenThietBi { get; set; } = string.Empty;

        public DateTime? NgayDuaVaoSuDung { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? NguyenGia { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? KhauHao { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? GiaTriConLai { get; set; }

        [StringLength(255)]
        public string? DT_HD_KHCN_LienQuan { get; set; }

        public string? NhatKySuDung { get; set; }

        [StringLength(100)]
        public string? TinhTrangThietBi { get; set; }

        public int? MaSoHopDong { get; set; }

        // SỬA: navigation trỏ tới class HopDong (không phải HDKHCN)
        [ForeignKey("MaSoHopDong")]
        public virtual HopDong? HopDong { get; set; }
    }
}
