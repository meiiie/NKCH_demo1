using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models;

public partial class TaiKhoan
{
    [Key]
    public int Id { get; set; }

    public string TenDangNhap { get; set; } = null!;

    public string MatKhau { get; set; } = null!;

    public string Email { get; set; } = null!;

    public bool TrangThai { get; set; }

    public DateOnly? LanDangNhapCuoi { get; set; }

    public DateOnly? NgayThamGia { get; set; }

    //public virtual ICollection<LichSuHoatDong> LichSuHoatDongs { get; set; } = new List<LichSuHoatDong>();

    //public virtual ICollection<PhienDangNhap> PhienDangNhaps { get; set; } = new List<PhienDangNhap>();

    //public virtual ICollection<TaiKhoanVaiTroAdmin> TaiKhoanVaiTroAdmins { get; set; } = new List<TaiKhoanVaiTroAdmin>();

    //public virtual ICollection<TaiKhoanVaiTroUser> TaiKhoanVaiTroUsers { get; set; } = new List<TaiKhoanVaiTroUser>();

    //public virtual ICollection<ThongBao> ThongBaos { get; set; } = new List<ThongBao>();
}
