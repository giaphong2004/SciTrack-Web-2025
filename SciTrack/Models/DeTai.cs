namespace SciTrack.web.Models
{
    public class DeTai
    {
        public int Id { get; set; } 
        public string MaDeTai { get; set; } = string.Empty;
        public string Ten { get; set; } = string.Empty;
        public DateOnly? CapNhatTaiSanLanCuoi { get; set; }  
        public string? QuyetDinhThamChieu { get; set; }
        public decimal? KinhPhiThucHien { get; set; }
        public decimal? KinhPhiDaoTao { get; set; }
        public decimal? KinhPhiTieuHao { get; set; }
        public decimal? KhauHaoThietBi { get; set; }
        public string? QuyetDinhXuLyTaiSan { get; set; }
        

        public string? KetQuaDeTai { get; set; }
        
       
        public int? KetQuaDeTaiId { get; set; }
    }
}