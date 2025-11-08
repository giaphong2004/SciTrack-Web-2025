using System;
using System.Collections.Generic;

namespace SciTrack.Api.Models;

public partial class Tskhcn
{
    public int Id { get; set; }

    public string? SoDanhMuc { get; set; }

    public string Ten { get; set; } = null!;

    public decimal? NguyenGia { get; set; }

    public decimal? KhauHao { get; set; }

    public decimal? HaoMon { get; set; }

    public decimal? GiaTriConLai { get; set; }

    public string? TrangThaiTaiSan { get; set; }

    public DateOnly? NgayCapNhat { get; set; }

    public int? MaSoDeTaiKhcn { get; set; }

    public virtual Dtkhcn? MaSoDeTaiKhcnNavigation { get; set; }
}
