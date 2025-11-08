using System;
using System.Collections.Generic;

namespace SciTrack.Api.Models;

public partial class Dtkhcn
{
    public int Id { get; set; }

    public string MaDeTai { get; set; } = null!;

    public string TenDtkhcn { get; set; } = null!;

    public DateOnly? NgayCapNhatTaiSan { get; set; }

    public string? CacQuyetDinh { get; set; }

    public string? QuyetDinhXuLy { get; set; }

    public decimal? KinhPhiThucHien { get; set; }

    public decimal? KinhPhiGiaoKhoanChuyen { get; set; }

    public decimal? KinhPhiVatTuTieuHao { get; set; }

    public decimal? HaoMonLienQuan { get; set; }

    public int? KetQuaDeTai { get; set; }

    public virtual Kqdt? KetQuaDeTaiNavigation { get; set; }

    public virtual ICollection<Tskhcn> Tskhcns { get; set; } = new List<Tskhcn>();
}
