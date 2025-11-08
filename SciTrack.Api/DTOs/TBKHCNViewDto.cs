using System;

namespace SciTrack.Api.DTOs
{
    /// <summary>
    /// Dữ liệu trả về cho bảng Thiết Bị Khoa Học Công Nghệ (TTBKHCN)
    /// </summary>
    public class TBKHCNViewDto
    {
        public int Id { get; set; }
        public string TenThietBi { get; set; } = string.Empty;
        public DateOnly? NgayDuaVaoSuDung { get; set; }
        public decimal? NguyenGia { get; set; }
        public decimal? KhauHao { get; set; }
        public decimal? GiaTriConLai { get; set; }
        public string? DT_HD_KHCN_LienQuan { get; set; }
        public string? NhatKySuDung { get; set; }
        public string? TinhTrangThietBi { get; set; }
        public string? MaThietBi { get; set; }  // MỚI
        public string? TenDoiTac { get; set; } // Nếu cần hiển thị tên hợp đồng
    }
}
