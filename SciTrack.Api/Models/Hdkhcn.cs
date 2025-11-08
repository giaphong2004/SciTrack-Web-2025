using System;
using System.Collections.Generic;

namespace SciTrack.Api.Models;

public partial class Hdkhcn
{
    public int Id { get; set; }

    public string MaHopDong { get; set; } = null!;

    public string TenDoiTac { get; set; } = null!;

    public DateOnly? NgayHieuLuc { get; set; }

    public DateOnly? NgayNghiemThu { get; set; }

    public decimal? TongGiaTriHopDong { get; set; }

    public decimal? ChiPhiKetQuaDeTai { get; set; }

    public decimal? ChiPhiTrangThietBi { get; set; }

    public decimal? ChiPhiHoatDongChuyenMon { get; set; }

    public decimal? LoiNhuan { get; set; }

    public virtual ICollection<LienKetKqdtHd> LienKetKqdtHds { get; set; } = new List<LienKetKqdtHd>();
}
