using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SciTrack.Api.Models
{
    [Table("DTKHCN")]
    public class DeTai
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("ProjectCode")]
        public string? MaDeTai { get; set; }

        [Column("ProjectName")]
        public string TenDeTai { get; set; }

        [Column("StartDate")]
        public DateTime? NgayBatDau { get; set; }

        [Column("EndDate")]
        public DateTime? NgayKetThuc { get; set; }

        [Column("TotalBudget")]
        public decimal? TongKinhPhi { get; set; }

        // Mối quan hệ: Một Đề tài có nhiều Tài sản
        public virtual ICollection<TaiSan> TaiSans { get; set; } = new List<TaiSan>();

        // Mối quan hệ Nhiều-Nhiều với Hợp đồng
        public virtual ICollection<HopDong> HopDongs { get; set; } = new List<HopDong>();
    }
}