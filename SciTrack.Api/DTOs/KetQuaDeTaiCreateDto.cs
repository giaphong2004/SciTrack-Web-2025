using System.ComponentModel.DataAnnotations;

public class KetQuaDeTaiCreateDto
{
    [Required(ErrorMessage = "Mã kết quả là bắt buộc")]
    public string MaKetQua { get; set; } = string.Empty; // 🔥 mã hiển thị

    [Required(ErrorMessage = "Tên kết quả là bắt buộc")]
    public string TenKetQua { get; set; } = string.Empty;

    public string? PhanLoai { get; set; }
    public decimal? DinhGia { get; set; }
    public decimal? GiaTriConLai { get; set; }
    public string? CacHopDong { get; set; }
    public DateTime? NgayCapNhatTaiSan { get; set; }
}
