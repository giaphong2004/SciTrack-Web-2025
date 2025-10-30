using System.ComponentModel.DataAnnotations;

namespace SciTrack.Api.DTOs
{
    /// <summary>
    /// DTO dùng để tạo/cập nhật kết quả đề tài (INPUT)
    /// </summary>
    public class KetQuaDeTaiCreateDto
    {
        [Required(ErrorMessage = "Tên kết quả là bắt buộc")]
        public string TenKetQua { get; set; } = string.Empty;

        public string? PhanLoai { get; set; }

        public decimal? DinhGia { get; set; }

        public decimal? GiaTriConLai { get; set; }

        public string? CacHopDong { get; set; }

        public DateTime? NgayCapNhatTaiSan { get; set; }

        public int? MaSoThietBi { get; set; }  // ID của hợp đồng
    }
}