using System;
using System.Collections.Generic;

namespace WebApplication3.Models;

public partial class UngCuVien
{
    public int Id { get; set; }

    public string HoTen { get; set; } = null!;

    public string? Anh { get; set; }

    public string MoTa { get; set; } = null!;

    public int ViTriUngCuId { get; set; }

    public int CuocBauCuId { get; set; }

    public int? PhienBauCuId { get; set; }

    public virtual CuocBauCu CuocBauCu { get; set; } = null!;

    public virtual PhienBauCu? PhienBauCu { get; set; }

    public virtual ICollection<PhieuBau> PhieuBaus { get; set; } = new List<PhieuBau>();

    public virtual ViTriUngCu ViTriUngCu { get; set; } = null!;
}
