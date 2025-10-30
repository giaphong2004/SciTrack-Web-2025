namespace SciTrack.Api.DTOs
{
    /// <summary>
    /// DTO dùng để trả về thông tin kết quả đề tài (OUTPUT)
    /// </summary>
    public class KetQuaDeTaiViewDto
    {
        public int Id { get; set; }
        public string TenKetQua { get; set; } = string.Empty;
        public string? PhanLoai { get; set; }
        public decimal? DinhGia { get; set; }
        public decimal? GiaTriConLai { get; set; }
        public string? CacHopDong { get; set; }
        public DateTime? NgayCapNhatTaiSan { get; set; }
        public int? MaSoThietBi { get; set; }
        public string? TenHopDong { get; set; }  // Thêm tên hợp đồng từ navigation
    }
}