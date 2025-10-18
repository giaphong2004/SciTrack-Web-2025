using System.ComponentModel.DataAnnotations;

namespace SciTrack.Api.DTOs
{
    public class DeTaiCreateDto
    {
        [Required]
        public string TenDeTai { get; set; } 
        public string? MaSoDeTai { get; set; } 
        public DateTime? NgayCapNhatTaiSan { get; set; } 
        public string? CacQuyetDinhLienQuan { get; set; } 
        public decimal? KinhPhiThucHien { get; set; } 
        public decimal? KinhPhiGiaoKhoaChuyen { get; set; } 
        public decimal? KinhPhiVatTuTieuHao { get; set; } 
        public decimal? HaoMonKhauHaoLienQuan { get; set; } 

        public string? QuyetDinhXuLyTaiSan { get; set; } 
        public string? KetQuaDeTai { get; set; } 
    }
}