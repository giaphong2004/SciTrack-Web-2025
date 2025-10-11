using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SciTrack.Api.Models
{
    public class KetQuaDeTai
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Mã số kết quả")]
        public string? MaSoKetQua { get; set; }

        [Display(Name = "Tên của kết quả")]
        public string? TenKetQua { get; set; }

        [Display(Name = "Phân loại")]
        public string? PhanLoai { get; set; }

        [Display(Name = "Định giá")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? DinhGia { get; set; }

        [Display(Name = "Ngày cập nhật")]
        [DataType(DataType.Date)]
        public DateTime? NgayCapNhat { get; set; }

        [Display(Name = "Giá trị còn lại")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? GiaTriConLai { get; set; }

        [Display(Name = "Các hợp đồng sử dụng kết quả")]
        public string? CacHopDongSuDung { get; set; }
    }
}