using System;
using System.Collections.Generic;

namespace SciTrack.Api.Models;

public partial class Kqdt
{
    public int Id { get; set; }

    public string? MaKetQua { get; set; }

    public string TenKetQua { get; set; } = null!;

    public string? PhanLoai { get; set; }

    public decimal? DinhGia { get; set; }

    public decimal? GiaTriConLai { get; set; }

    public DateOnly? NgayCapNhatTaiSan { get; set; }

    public virtual ICollection<Dtkhcn> Dtkhcns { get; set; } = new List<Dtkhcn>();

    public virtual ICollection<LienKetKqdtHd> LienKetKqdtHds { get; set; } = new List<LienKetKqdtHd>();
}
