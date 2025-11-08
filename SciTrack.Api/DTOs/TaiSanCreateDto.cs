using System.ComponentModel.DataAnnotations;
using System;

namespace SciTrack.Api.DTOs
{
    /// <summary>
    /// DTO dùng để tạo/cập nhật tài sản (INPUT)
    /// </summary>
    public class TaiSanCreateDto
    {
        [Required(ErrorMessage = "Tên tài sản là bắt buộc")]
        public string Ten { get; set; } = string.Empty;

        public string? SoDanhMuc { get; set; }

        public decimal? NguyenGia { get; set; }

        public decimal? KhauHao { get; set; }

        public decimal? HaoMon { get; set; }

        public decimal? GiaTriConLai { get; set; }

        public string? TrangThaiTaiSan { get; set; }

        public DateOnly? NgayCapNhat { get; set; }

        public int? MaDeTaiKHCN { get; set; }  // ID của đề tài
    }
}