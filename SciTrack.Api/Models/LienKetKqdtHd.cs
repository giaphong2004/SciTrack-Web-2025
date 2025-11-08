using System;
using System.Collections.Generic;

namespace SciTrack.Api.Models;

public partial class LienKetKqdtHd
{
    public int Id { get; set; }

    public int KqdtId { get; set; }

    public int HdkhcnId { get; set; }

    public virtual Hdkhcn Hdkhcn { get; set; } = null!;

    public virtual Kqdt Kqdt { get; set; } = null!;
}
