using System.ComponentModel.DataAnnotations;

namespace SciTrack.Api.DTOs
{
    public class TaiSanCreateDto
    {
        [Required]
        public string Ten { get; set; }
        public string? SoDanhMuc { get; set; }
        public int? DeTaiId { get; set; }
        public int? ThietBiId { get; set; }
        public decimal? NguyenGia { get; set; }
        public string? TrangThai { get; set; }
        public decimal? KhauHao { get; set; }
        public decimal? HaoMon { get; set; }
        public decimal? GiaTriConLai { get; set; }
        public DateTime? NgayCapNhat { get; set; }
    }
}