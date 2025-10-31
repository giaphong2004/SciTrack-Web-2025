using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("TTBKHCN")]
public class TBKHCN
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("TenThietBi")]
    [Required]
    public string TenThietBi { get; set; } = string.Empty;

    [Column("NgayDuaVaoSuDung")]
    public DateTime? NgayDuaVaoSuDung { get; set; }

    [Column("NguyenGia")]
    public decimal? NguyenGia { get; set; }

    [Column("KhauHao")]
    public decimal? KhauHao { get; set; }

    [Column("GiaTriConLai")]
    public decimal? GiaTriConLai { get; set; }

    [Column("DT_HD_KHCN_LienQuan")]
    public string? DT_HD_KHCN_LienQuan { get; set; }

    [Column("NhatKySuDung")]
    public string? NhatKySuDung { get; set; }

    [Column("TinhTrangThietBi")]
    public string? TinhTrangThietBi { get; set; }

    [Column("MaThietBi")]
    [StringLength(50)]
    public string? MaThietBi { get; set; }  // MỚI

    // XÓA HOÀN TOÀN:
    // public int? MaSoHopDong { get; set; }
    // public virtual HopDong? HopDong { get; set; }
}