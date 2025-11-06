using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SciTrack.Api.Models
{
    [Table("HDKHCN")] // ánh xạ tới bảng trong database
    public class HopDong
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [StringLength(255)]
        public string TenDoiTac { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayHieuLuc { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayNghiemThu { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? TongGiaTriHopDong { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? ChiPhiKetQuaDeTai { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? ChiPhiTrangThietBi { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? ChiPhiHoatDongChuyenMon { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? LoiNhuan { get; set; }

        [StringLength(50)]
        public string? MaHopDong { get; set; }
    }
}
