using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SciTrack.Api.Models
{
    public class TaiSan
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Số danh mục")]
        public string? SoDanhMuc { get; set; }

        [Display(Name = "Tên")]
        public string? Ten { get; set; }

        [Display(Name = "Nguyên giá")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? NguyenGia { get; set; }

        [Display(Name = "Khấu hao")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? KhauHao { get; set; }

        [Display(Name = "Hao mòn")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? HaoMon { get; set; }

        [Display(Name = "Giá trị còn lại")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? GiaTriConLai { get; set; }

        [Display(Name = "Trạng thái tài sản")]
        public string? TrangThai { get; set; }

        [Display(Name = "Ngày cập nhật")]
        [DataType(DataType.Date)]
        public DateTime? NgayCapNhat { get; set; }

        // Foreign Key
        [Display(Name = "Mã số đề tài KHCN")]
        public int DeTaiId { get; set; }

        [ForeignKey("DeTaiId")]
        public virtual DeTai? DeTai { get; set; }
    }
}