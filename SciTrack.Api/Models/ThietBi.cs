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

        [Column("EquipmentCode")]
        public string? MaThietBi { get; set; }

        [Column("EquipmentName")]
        public string TenThietBi { get; set; }

        [Column("OriginalValue")]
        public decimal? NguyenGia { get; set; }

        [Column("Status")]
        public string? TrangThai { get; set; }

        // Khóa ngoại đến Hợp đồng
        [Column("Contract_ID")]
        public int? HopDongId { get; set; }
        public virtual HopDong? HopDong { get; set; }
    }
}