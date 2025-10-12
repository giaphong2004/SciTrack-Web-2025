using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SciTrack.Api.Models
{
    [Table("TSKHCN")]
    public class TaiSan
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("AssetCode")]
        public string? SoDanhMuc { get; set; }

        [Column("Name")]
        public string Ten { get; set; }

        [Column("OriginalValue")]
        public decimal? NguyenGia { get; set; }

        // === DÒNG QUAN TRỌNG ĐÃ ĐƯỢC SỬA LẠI ===
        [Column("AssetStatus")] // Sửa từ "Status" thành "AssetStatus"
        public string? TrangThai { get; set; }
        // =====================================

        [Column("ProjectID")]
        public int? DeTaiId { get; set; }
        public virtual DeTai? DeTai { get; set; }

        [Column("EquipmentID")]
        public int? ThietBiId { get; set; }
        public virtual ThietBi? ThietBi { get; set; }

        public virtual ICollection<KetQuaDeTai> KetQuaDeTais { get; set; } = new List<KetQuaDeTai>();
    }
}