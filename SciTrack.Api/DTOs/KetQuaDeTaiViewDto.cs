using System;

namespace SciTrack.Api.DTOs
{
    /// <summary>
    /// DTO dùng để trả về thông tin kết quả đề tài (OUTPUT)
    /// </summary>
    public class KetQuaDeTaiViewDto
    {
        public int Id { get; set; }               // Khóa chính DB
        public string? MaKetQua { get; set; }     
        public string TenKetQua { get; set; } = string.Empty;
        public string? PhanLoai { get; set; }
        public decimal? DinhGia { get; set; }
        public decimal? GiaTriConLai { get; set; }
        public string? CacHopDong { get; set; }
        public DateTime? NgayCapNhatTaiSan { get; set; }
    }

}
