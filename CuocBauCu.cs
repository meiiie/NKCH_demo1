using System;
using System.Collections.Generic;

namespace WebApplication3.Models;

public partial class CuocBauCu
{
    public int Id { get; set; }

    public string TenCuocBauCu { get; set; } = null!;

    public string MoTa { get; set; } = null!;

    public DateTime NgayBatDau { get; set; }

    public DateTime NgayKetThuc { get; set; }

    public virtual ICollection<CuTri> CuTris { get; set; } = new List<CuTri>();

    public virtual ICollection<PhienBauCu> PhienBauCus { get; set; } = new List<PhienBauCu>();

    public virtual ICollection<PhieuBau> PhieuBaus { get; set; } = new List<PhieuBau>();

    public virtual ICollection<TaiKhoanVaiTroUser> TaiKhoanVaiTroUsers { get; set; } = new List<TaiKhoanVaiTroUser>();

    public virtual ICollection<UngCuVien> UngCuViens { get; set; } = new List<UngCuVien>();

    public virtual ICollection<ViTriUngCu> ViTriUngCus { get; set; } = new List<ViTriUngCu>();
}
