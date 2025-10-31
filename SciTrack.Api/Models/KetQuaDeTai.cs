using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SciTrack.Api.Models
{
    [Table("KQDT")]
    public class KetQuaDeTai
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }  // Khóa chính tự tăng (PK)

        [Column("MaKetQua")]
        [StringLength(50)]
        public string? MaKetQua { get; set; }  

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

        // Quan hệ 1-N với Đề tài
        public virtual ICollection<DeTai> DeTais { get; set; } = new List<DeTai>();
    }
}
