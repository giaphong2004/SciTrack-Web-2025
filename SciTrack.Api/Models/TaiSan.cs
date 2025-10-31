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

        [Column("SoDanhMuc")]
        public string SoDanhMuc { get; set; }


        [Required]
        [Column("Ten")]
        public string Ten { get; set; } = string.Empty;

        [Column("NguyenGia")]
        public decimal? NguyenGia { get; set; }

        [Column("KhauHao")]
        public decimal? KhauHao { get; set; }

        [Column("HaoMon")]
        public decimal? HaoMon { get; set; }

        [Column("GiaTriConLai")]
        public decimal? GiaTriConLai { get; set; }

        [Column("TrangThaiTaiSan")]
        public string? TrangThaiTaiSan { get; set; }

        [Column("NgayCapNhat")]
        public DateTime? NgayCapNhat { get; set; }

        // Foreign key to DTKHCN
        [Column("MaSoDeTaiKHCN")]
        public int? MaSoDeTaiKHCN { get; set; }
        
        [ForeignKey("MaSoDeTaiKHCN")]
        public virtual DeTai? DeTai { get; set; }

        [Column("MaSoDeTaiKHCN2")]
        public int? MaSoDeTaiKHCN2 { get; set; }
    }
}