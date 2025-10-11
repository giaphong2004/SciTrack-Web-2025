using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SciTrack.Api.Models
{
    public class HopDong
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Mã hợp đồng")]
        public string? MaHopDong { get; set; }

        [Display(Name = "Tên đối tác")]
        public string? TenDoiTac { get; set; }

        [Display(Name = "Ngày hiệu lực")]
        [DataType(DataType.Date)]
        public DateTime? NgayHieuLuc { get; set; }

        [Display(Name = "Ngày nghiệm thu")]
        [DataType(DataType.Date)]
        public DateTime? NgayNghiemThu { get; set; }

        [Display(Name = "Tổng giá trị hợp đồng")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? TongGiaTri { get; set; }

        [Display(Name = "Chi phí từ tài sản là kết quả đề tài")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? ChiPhiTuKetQuaDeTai { get; set; }

        [Display(Name = "Chi phí từ trang thiết bị")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? ChiPhiTuTrangThietBi { get; set; }

        [Display(Name = "Chi phí hoạt động chuyên môn")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? ChiPhiChuyenMon { get; set; }

        [Display(Name = "Lợi nhuận")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? LoiNhuan { get; set; }
    }
}