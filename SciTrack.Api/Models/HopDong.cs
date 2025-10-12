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

        [Column("ContractCode")]
        public string? MaHopDong { get; set; }

        [Column("ContractName")]
        public string TenHopDong { get; set; }

        [Column("StartDate")]
        public DateTime? NgayBatDau { get; set; }

        [Column("EndDate")]
        public DateTime? NgayKetThuc { get; set; }

        [Column("TotalValue")]
        public decimal? TongGiaTri { get; set; }

        // Mối quan hệ: Một Hợp đồng có nhiều Thiết bị
        public virtual ICollection<ThietBi> ThietBis { get; set; } = new List<ThietBi>();

        // Mối quan hệ Nhiều-Nhiều với Đề tài và Kết quả
        public virtual ICollection<DeTai> DeTais { get; set; } = new List<DeTai>();
        public virtual ICollection<KetQuaDeTai> KetQuaDeTais { get; set; } = new List<KetQuaDeTai>();
    }
}