using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SciTrack.Api.Models
{
    public class DeTai
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Mã số đề tài")]
        public string? MaSoDeTai { get; set; }

        [Display(Name = "Tên ĐT KHCN")]
        public string? TenDeTai { get; set; }

        [Display(Name = "Ngày cập nhật tài sản")]
        [DataType(DataType.Date)]
        public DateTime? NgayCapNhatTaiSan { get; set; }

        [Display(Name = "Các quyết định liên quan")]
        public string? QuyetDinhLienQuan { get; set; }

        [Display(Name = "Quyết định xử lý tài sản")]
        public string? QuyetDinhXuLyTaiSan { get; set; }

        [Display(Name = "Kinh phí thực hiện")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? KinhPhiThucHien { get; set; }

        [Display(Name = "Kinh phí giao khoán chuyên môn")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? KinhPhiGiaoKhoan { get; set; }

        [Display(Name = "Kinh phí vật tư tiêu hao")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? KinhPhiVatTu { get; set; }

        [Display(Name = "Hao mòn / Khấu hao liên quan")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? HaoMonKhauHaoLienQuan { get; set; }

        // Foreign Key
        [Display(Name = "Kết quả đề tài")]
        public int? KetQuaDeTaiId { get; set; }

        [ForeignKey("KetQuaDeTaiId")]
        public virtual KetQuaDeTai? KetQuaDeTai { get; set; }
    }
}