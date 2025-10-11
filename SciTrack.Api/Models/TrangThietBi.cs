using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SciTrack.Api.Models
{
    public class TrangThietBi
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Mã thiết bị")]
        public string? MaThietBi { get; set; }

        [Display(Name = "Tên thiết bị")]
        public string? TenThietBi { get; set; }

        [Display(Name = "Ngày đưa vào sử dụng")]
        [DataType(DataType.Date)]
        public DateTime? NgaySuDung { get; set; }

        [Display(Name = "Nguyên giá")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? NguyenGia { get; set; }

        [Display(Name = "Khấu hao/hao mòn")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? KhauHaoHaoMon { get; set; }

        [Display(Name = "Giá trị còn lại")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? GiaTriConLai { get; set; }

        [Display(Name = "ĐT/HĐ KHCN liên quan")]
        public string? LienQuan { get; set; }

        [Display(Name = "Nhật ký sử dụng")]
        public string? NhatKySuDung { get; set; }

        [Display(Name = "Tình trạng thiết bị")]
        public string? TinhTrang { get; set; }
    }
}