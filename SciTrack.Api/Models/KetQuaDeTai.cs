﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SciTrack.Api.Models
{
    [Table("KQDT")]
    public class KetQuaDeTai
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [Column("TenKetQua")]
        public string TenKetQua { get; set; } = string.Empty;

        [Column("PhanLoai")]
        public string? PhanLoai { get; set; }

        [Column("DinhGia")]
        public decimal? DinhGia { get; set; }

        [Column("GiaTriConLai")]
        public decimal? GiaTriConLai { get; set; }

        [Column("CacHopDong")]
        public string? CacHopDong { get; set; }

        [Column("NgayCapNhatTaiSan")]
        public DateTime? NgayCapNhatTaiSan { get; set; }

        // Foreign key to HDKHCN
        [Column("MaSoThietBi")]
        public int? MaSoThietBi { get; set; }
        
        [ForeignKey("MaSoThietBi")]
        public virtual HopDong? HopDong { get; set; }

        // Mối quan hệ: Một Kết quả có nhiều Đề tài
        public virtual ICollection<DeTai> DeTais { get; set; } = new List<DeTai>();
    }
}