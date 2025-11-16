using System.ComponentModel.DataAnnotations;

namespace SciTrack.Api.DTOs
{
    public class DeTaiCreateDto
    {
        [Required]
        public string Ten { get; set; }

        public string? MaSoDeTai { get; set; }

        public DateOnly? NgayCapNhatTaiSan { get; set; }

        public string? CacQuyetDinhLienQuan { get; set; }

        public decimal? KinhPhiThucHien { get; set; }

        public decimal? KinhPhiGiaoKhoaChuyen { get; set; }

        public decimal? KinhPhiVatTuTieuHao { get; set; }

        public decimal? HaoMonKhauHaoLienQuan { get; set; }

        public string? QuyetDinhXuLyTaiSan { get; set; }

        public int? KetQuaDeTai { get; set; }
    }
}