using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("HDKHCN")]
public class HopDong
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("TenDoiTac")]
    [Required]
    public string TenDoiTac { get; set; } = string.Empty;

    [Column("NgayHieuLuc")]
    public DateTime? NgayHieuLuc { get; set; }

    [Column("NgayNghiemThu")]
    public DateTime? NgayNghiemThu { get; set; }

    [Column("TongGiaTriHopDong")]
    public decimal? TongGiaTriHopDong { get; set; }

    [Column("ChiPhiKetQuaDeTai")]
    public decimal? ChiPhiKetQuaDeTai { get; set; }

    [Column("ChiPhiTrangThietBi")]
    public decimal? ChiPhiTrangThietBi { get; set; }

    [Column("ChiPhiHoatDongChuyenMon")]
    public decimal? ChiPhiHoatDongChuyenMon { get; set; }

    [Column("LoiNhuan")]
    public decimal? LoiNhuan { get; set; }

    [Column("GhiChu")]
    public string? GhiChu { get; set; }

    // XÓA HOÀN TOÀN:
    // public virtual ICollection<KetQuaDeTai> KetQuaDeTais { get; set; } = new List<KetQuaDeTai>();
    // public virtual ICollection<TBKHCN> TTBKHCNs { get; set; } = new List<TBKHCN>();
}