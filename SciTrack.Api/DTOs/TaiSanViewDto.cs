namespace SciTrack.Api.DTOs
{
    /// <summary>
    /// DTO dùng để trả về thông tin tài sản (OUTPUT)
    /// </summary>
    public class TaiSanViewDto
    {
        public int Id { get; set; }
        public string SoDanhMuc { get; set; } = string.Empty;
        public string Ten { get; set; } = string.Empty;
        public decimal? NguyenGia { get; set; }
        public decimal? KhauHao { get; set; }
        public decimal? HaoMon { get; set; }
        public decimal? GiaTriConLai { get; set; }
        public string? TrangThaiTaiSan { get; set; }
        public DateOnly? NgayCapNhat { get; set; }
        public string? MaDeTaiKHCN { get; set; }  // Mã đề tài (ID)
    }
}
