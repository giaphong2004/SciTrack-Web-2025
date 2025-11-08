using System;
using System.Collections.Generic;

namespace SciTrack.Api.Models;

public partial class Ttbkhcn
{
    public int Id { get; set; }

    public string? MaThietBi { get; set; }

    public string TenThietBi { get; set; } = null!;

    public DateOnly? NgayDuaVaoSuDung { get; set; }

    public decimal? NguyenGia { get; set; }

    public decimal? KhauHao { get; set; }

    public decimal? GiaTriConLai { get; set; }

    public string? DtHdKhcnLienQuan { get; set; }

    public string? NhatKySuDung { get; set; }

    public string? TinhTrangThietBi { get; set; }
}
