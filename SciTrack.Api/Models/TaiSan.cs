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
        public string Ten { get; set; } = string.Empty;

        [Column("OriginalValue")]
        public decimal? NguyenGia { get; set; }

        [Column("Depreciation")]
        public decimal? KhauHao { get; set; }

        [Column("Amortization")]
        public decimal? HaoMon { get; set; }

        [Column("ResidualValue")]
        public decimal? GiaTriConLai { get; set; }

        [Column("LastUpdated")]
        public DateTime? NgayCapNhat { get; set; }

        [Column("AssetStatus")]
        public string? TrangThai { get; set; }

        [Column("ProjectID")]
        public int? DeTaiId { get; set; }
        public virtual DeTai? DeTai { get; set; }

        public virtual ICollection<KetQuaDeTai> KetQuaDeTais { get; set; } = new List<KetQuaDeTai>();
    }
}