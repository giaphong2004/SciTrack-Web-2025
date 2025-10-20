using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SciTrack.Api.Models
{
    [Table("HDKHCN")]
    public class HopDong
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [Column("TenDoiTac")]
        public string TenDoiTac { get; set; } = string.Empty;

        [Column("NgayHieuLuc")]
        public DateTime? NgayHieuLuc { get; set; }

        [Column("NgayNghiemThu")]
        public DateTime? NgayNghiemThu { get; set; }

        [Column("TongGiaTriHopDong")]
        public decimal? TongGiaTriHopDong { get; set; }

        [Column("ChiPhiKetQuaDeTai")]
        public decimal? ChiPhiKetQuaDeTai { get; set; }

        [Column("ChiPhiTrangThietBi")]
        public decimal? ChiPhiTrangThietBi { get; set; }

        [Column("ChiPhiHoatDongChuyenMon")]
        public decimal? ChiPhiHoatDongChuyenMon { get; set; }

        [Column("LoiNhuan")]
        public decimal? LoiNhuan { get; set; }

        [Column("GhiChu")]
        public string? GhiChu { get; set; }

        // Mối quan hệ: Một Hợp đồng có nhiều Thiết bị
        public virtual ICollection<ThietBi> ThietBis { get; set; } = new List<ThietBi>();

        // Mối quan hệ: Một Hợp đồng có nhiều Kết quả
        public virtual ICollection<KetQuaDeTai> KetQuaDeTais { get; set; } = new List<KetQuaDeTai>();
    }
}