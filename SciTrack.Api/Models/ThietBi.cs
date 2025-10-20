using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SciTrack.Api.Models
{
    [Table("TTBKHCN")]
    public class ThietBi
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [Column("TenThietBi")]
        public string TenThietBi { get; set; } = string.Empty;

        [Column("NgayDuaVaoSuDung")]
        public DateTime? NgayDuaVaoSuDung { get; set; }

        [Column("NguyenGia")]
        public decimal? NguyenGia { get; set; }

        [Column("KhauHao")]
        public decimal? KhauHao { get; set; }

        [Column("GiaTriConLai")]
        public decimal? GiaTriConLai { get; set; }

        [Column("DT_HD_KHCN_LienQuan")]
        public string? DT_HD_KHCN_LienQuan { get; set; }

        [Column("NhatKySuDung")]
        public string? NhatKySuDung { get; set; }

        [Column("TinhTrangThietBi")]
        public string? TinhTrangThietBi { get; set; }

        // Foreign key to HDKHCN
        [Column("MaSoHopDong")]
        public int? MaSoHopDong { get; set; }
        
        [ForeignKey("MaSoHopDong")]
        public virtual HopDong? HopDong { get; set; }
    }
}