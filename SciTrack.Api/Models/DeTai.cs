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

        [Required]
        [Column("TenDTKHCN")]
        public string TenDTKHCN { get; set; } = string.Empty;

        [Column("NgayCapNhatTaiSan")]
        public DateTime? NgayCapNhatTaiSan { get; set; }

        [Column("CacQuyetDinh")]
        public string? CacQuyetDinh { get; set; }

        [Column("QuyetDinhXuLy")]
        public string? QuyetDinhXuLy { get; set; }

        [Column("KinhPhiThucHien")]
        public decimal? KinhPhiThucHien { get; set; }

        [Column("KinhPhiGiaoKhoanChuyen")]
        public decimal? KinhPhiGiaoKhoanChuyen { get; set; }

        [Column("KinhPhiVatTuTieuHao")]
        public decimal? KinhPhiVatTuTieuHao { get; set; }

        [Column("HaoMonLienQuan")]
        public decimal? HaoMonLienQuan { get; set; }

        // Foreign key to KQDT
        [Column("KetQuaDeTai")]
        public int? KetQuaDeTai { get; set; }
        
        [ForeignKey("KetQuaDeTai")]
        public virtual KetQuaDeTai? KetQua { get; set; }

        [Column("MaSoKetQua")]
        public int? MaSoKetQua { get; set; }

        // Mối quan hệ: Một Đề tài có nhiều Tài sản
        public virtual ICollection<TaiSan> TaiSans { get; set; } = new List<TaiSan>();
    }
}