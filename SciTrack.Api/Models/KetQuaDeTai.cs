using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SciTrack.Api.Models
{
    [Table("KQDT")]
    public class KetQuaDeTai
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("ResultName")]
        public string TenKetQua { get; set; }

        [Column("ResultType")]
        public string? LoaiKetQua { get; set; }

        [Column("SubmissionDate")]
        public DateTime? NgayNop { get; set; }

        // Mối quan hệ Nhiều-Nhiều với Tài sản và Hợp đồng
        public virtual ICollection<TaiSan> TaiSans { get; set; } = new List<TaiSan>();
        public virtual ICollection<HopDong> HopDongs { get; set; } = new List<HopDong>();
    }
}