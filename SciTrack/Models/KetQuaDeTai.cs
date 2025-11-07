using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SciTrack.web.Models
{
    public class KetQuaDeTai
    {
        public int Id { get; set; }  // Khóa chính tự tăng (PK)
        public string MaKetQua { get; set; }
        public string TenKetQua { get; set; } = string.Empty;
        public string? PhanLoai { get; set; }
        public decimal? DinhGia { get; set; }
        public decimal? GiaTriConLai { get; set; }
        public string? CacHopDong { get; set; }
        public DateTime? NgayCapNhatTaiSan { get; set; }
    }
}
