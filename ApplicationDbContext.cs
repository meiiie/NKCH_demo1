﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApplication3.Models;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CauHinhHeThong> CauHinhHeThongs { get; set; }

    public virtual DbSet<ChucNang> ChucNangs { get; set; }

    public virtual DbSet<CuTri> CuTris { get; set; }

    public virtual DbSet<CuocBauCu> CuocBauCus { get; set; }

    public virtual DbSet<LichSuHoatDong> LichSuHoatDongs { get; set; }

    public virtual DbSet<PhienBauCu> PhienBauCus { get; set; }

    //public virtual DbSet<PhienDangNhap> PhienDangNhaps { get; set; }

    public virtual DbSet<PhieuBau> PhieuBaus { get; set; }

    public virtual DbSet<TaiKhoan> TaiKhoan { get; set; }

    public virtual DbSet<TaiKhoanVaiTroAdmin> TaiKhoanVaiTroAdmins { get; set; }

    public virtual DbSet<TaiKhoanVaiTroUser> TaiKhoanVaiTroUsers { get; set; }

    public virtual DbSet<ThongBao> ThongBaos { get; set; }

    public virtual DbSet<UngCuVien> UngCuViens { get; set; }

    public virtual DbSet<VKetQuaPhienBauCu> VKetQuaPhienBauCus { get; set; }

    public virtual DbSet<VPhieuBau> VPhieuBaus { get; set; }

    public virtual DbSet<VTrangThaiPbc> VTrangThaiPbcs { get; set; }

    public virtual DbSet<VaiTro> VaiTros { get; set; }

    public virtual DbSet<VaiTroChucNang> VaiTroChucNangs { get; set; }

    public virtual DbSet<ViTriUngCu> ViTriUngCus { get; set; }

    public virtual DbSet<VtrangThaiCbc> VtrangThaiCbcs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=ADMIN;Database=Nckh_trcTet;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CauHinhHeThong>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CauHinhH__3214EC2794D0BC2A");

            entity.ToTable("CauHinhHeThong");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.TenCauHinh).HasMaxLength(255);
        });

        modelBuilder.Entity<ChucNang>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ChucNang__3214EC27E7B6E84B");

            entity.ToTable("ChucNang");

            entity.HasIndex(e => e.TenChucNang, "UQ__ChucNang__CFD37AFA69DDD913").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.TenChucNang).HasMaxLength(255);
        });

        modelBuilder.Entity<CuTri>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CuTri__3214EC27CBD841C7");

            entity.ToTable("CuTri");

            entity.HasIndex(e => e.Email, "UQ__CuTri__A9D10534D6D5F218").IsUnique();

            entity.HasIndex(e => e.Sdt, "UQ__CuTri__CA1930A5375CB211").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CuocBauCuId).HasColumnName("CuocBauCuID");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.PhienBauCuId).HasColumnName("PhienBauCuID");
            entity.Property(e => e.Sdt)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("SDT");
            entity.Property(e => e.SoLanGuiOtp).HasColumnName("SoLanGuiOTP");

            entity.HasOne(d => d.CuocBauCu).WithMany(p => p.CuTris)
                .HasForeignKey(d => d.CuocBauCuId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CuTri__CuocBauCu__48CFD27E");

            entity.HasOne(d => d.PhienBauCu).WithMany(p => p.CuTris)
                .HasForeignKey(d => d.PhienBauCuId)
                .HasConstraintName("FK__CuTri__PhienBauC__49C3F6B7");
        });

        modelBuilder.Entity<CuocBauCu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CuocBauC__3214EC27927CACAC");

            entity.ToTable("CuocBauCu");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.NgayBatDau).HasColumnType("datetime");
            entity.Property(e => e.NgayKetThuc).HasColumnType("datetime");
        });

        modelBuilder.Entity<LichSuHoatDong>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LichSuHo__3214EC27BB880488");

            entity.ToTable("LichSuHoatDong");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.HoatDong).HasMaxLength(255);
            entity.Property(e => e.TaiKhoanId).HasColumnName("TaiKhoanID");
            entity.Property(e => e.ThoiGian).HasColumnType("datetime");

            //entity.HasOne(d => d.TaiKhoan).WithMany(p => p.LichSuHoatDongs)
            //    .HasForeignKey(d => d.TaiKhoanId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK__LichSuHoa__TaiKh__6B24EA82");
        });

        modelBuilder.Entity<PhienBauCu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PhienBau__3214EC279845F2B0");

            entity.ToTable("PhienBauCu");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CuocBauCuId).HasColumnName("CuocBauCuID");
            entity.Property(e => e.NgayBatDau).HasColumnType("datetime");
            entity.Property(e => e.NgayKetThuc).HasColumnType("datetime");

            entity.HasOne(d => d.CuocBauCu).WithMany(p => p.PhienBauCus)
                .HasForeignKey(d => d.CuocBauCuId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PhienBauC__CuocB__398D8EEE");
        });

        //modelBuilder.Entity<PhienDangNhap>(entity =>
        //{
        //    entity.HasKey(e => e.Id).HasName("PK__PhienDan__3214EC27A7AA192E");

        //    entity.ToTable("PhienDangNhap");

        //    entity.Property(e => e.Id)
        //        .HasMaxLength(40)
        //        .HasColumnName("ID");
        //    entity.Property(e => e.NgayHetHan).HasColumnType("datetime");
        //    entity.Property(e => e.TaiKhoanId).HasColumnName("TaiKhoanID");

        //    entity.HasOne(d => d.TaiKhoan).WithMany(p => p.PhienDangNhaps)
        //        .HasForeignKey(d => d.TaiKhoanId)
        //        .OnDelete(DeleteBehavior.ClientSetNull)
        //        .HasConstraintName("FK__PhienDang__TaiKh__6E01572D");
        //});

        modelBuilder.Entity<PhieuBau>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PhieuBau__3214EC27BE61F897");

            entity.ToTable("PhieuBau");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CuTriId).HasColumnName("CuTriID");
            entity.Property(e => e.CuocBauCuId).HasColumnName("CuocBauCuID");
            entity.Property(e => e.PhienBauCuId).HasColumnName("PhienBauCuID");
            entity.Property(e => e.UngCuVienId).HasColumnName("UngCuVienID");
            entity.Property(e => e.ViTriUngCuId).HasColumnName("ViTriUngCuID");

            entity.HasOne(d => d.CuTri).WithMany(p => p.PhieuBaus)
                .HasForeignKey(d => d.CuTriId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PhieuBau__CuTriI__4D94879B");

            entity.HasOne(d => d.CuocBauCu).WithMany(p => p.PhieuBaus)
                .HasForeignKey(d => d.CuocBauCuId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PhieuBau__CuocBa__5070F446");

            entity.HasOne(d => d.PhienBauCu).WithMany(p => p.PhieuBaus)
                .HasForeignKey(d => d.PhienBauCuId)
                .HasConstraintName("FK__PhieuBau__PhienB__4F7CD00D");

            entity.HasOne(d => d.UngCuVien).WithMany(p => p.PhieuBaus)
                .HasForeignKey(d => d.UngCuVienId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PhieuBau__UngCuV__4CA06362");

            entity.HasOne(d => d.ViTriUngCu).WithMany(p => p.PhieuBaus)
                .HasForeignKey(d => d.ViTriUngCuId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PhieuBau__ViTriU__4E88ABD4");
        });

        modelBuilder.Entity<TaiKhoan>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TaiKhoan__3214EC27D89FB40D");

            entity.ToTable("TaiKhoan");

            entity.HasIndex(e => e.TenDangNhap, "UQ__TaiKhoan__55F68FC0C79307E2").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.MatKhau).HasMaxLength(128);
            entity.Property(e => e.TenDangNhap).HasMaxLength(150);
        });

        modelBuilder.Entity<TaiKhoanVaiTroAdmin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TaiKhoan__3214EC278D24B91C");

            entity.ToTable("TaiKhoanVaiTroAdmin");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.TaiKhoanId).HasColumnName("TaiKhoanID");
            entity.Property(e => e.VaiTroId).HasColumnName("VaiTroID");

            //entity.HasOne(d => d.TaiKhoan).WithMany(p => p.TaiKhoanVaiTroAdmins)
            //    .HasForeignKey(d => d.TaiKhoanId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK__TaiKhoanV__TaiKh__6754599E");

            entity.HasOne(d => d.VaiTro).WithMany(p => p.TaiKhoanVaiTroAdmins)
                .HasForeignKey(d => d.VaiTroId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TaiKhoanV__VaiTr__68487DD7");
        });

        modelBuilder.Entity<TaiKhoanVaiTroUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TaiKhoan__3214EC2711CBAC23");

            entity.ToTable("TaiKhoanVaiTroUser");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CuocBauCuId).HasColumnName("CuocBauCuID");
            entity.Property(e => e.PhienBauCuId).HasColumnName("PhienBauCuID");
            entity.Property(e => e.TaiKhoanId).HasColumnName("TaiKhoanID");
            entity.Property(e => e.VaiTroId).HasColumnName("VaiTroID");

            entity.HasOne(d => d.CuocBauCu).WithMany(p => p.TaiKhoanVaiTroUsers)
                .HasForeignKey(d => d.CuocBauCuId)
                .HasConstraintName("FK__TaiKhoanV__CuocB__6383C8BA");

            entity.HasOne(d => d.PhienBauCu).WithMany(p => p.TaiKhoanVaiTroUsers)
                .HasForeignKey(d => d.PhienBauCuId)
                .HasConstraintName("FK__TaiKhoanV__Phien__6477ECF3");

            //entity.HasOne(d => d.TaiKhoan).WithMany(p => p.TaiKhoanVaiTroUsers)
            //    .HasForeignKey(d => d.TaiKhoanId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK__TaiKhoanV__TaiKh__619B8048");

            entity.HasOne(d => d.VaiTro).WithMany(p => p.TaiKhoanVaiTroUsers)
                .HasForeignKey(d => d.VaiTroId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TaiKhoanV__VaiTr__628FA481");
        });

        modelBuilder.Entity<ThongBao>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ThongBao__3214EC277ABA96BB");

            entity.ToTable("ThongBao");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.NgayGui).HasColumnType("datetime");
            entity.Property(e => e.TaiKhoanId).HasColumnName("TaiKhoanID");
            entity.Property(e => e.TieuDe).HasMaxLength(255);

            //entity.HasOne(d => d.TaiKhoan).WithMany(p => p.ThongBaos)
            //    .HasForeignKey(d => d.TaiKhoanId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK__ThongBao__TaiKho__70DDC3D8");
        });

        modelBuilder.Entity<UngCuVien>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UngCuVie__3214EC27864EE498");

            entity.ToTable("UngCuVien");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CuocBauCuId).HasColumnName("CuocBauCuID");
            entity.Property(e => e.PhienBauCuId).HasColumnName("PhienBauCuID");
            entity.Property(e => e.ViTriUngCuId).HasColumnName("ViTriUngCuID");

            entity.HasOne(d => d.CuocBauCu).WithMany(p => p.UngCuViens)
                .HasForeignKey(d => d.CuocBauCuId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UngCuVien__CuocB__4316F928");

            entity.HasOne(d => d.PhienBauCu).WithMany(p => p.UngCuViens)
                .HasForeignKey(d => d.PhienBauCuId)
                .HasConstraintName("FK__UngCuVien__Phien__440B1D61");

            entity.HasOne(d => d.ViTriUngCu).WithMany(p => p.UngCuViens)
                .HasForeignKey(d => d.ViTriUngCuId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UngCuVien__ViTri__4222D4EF");
        });

        modelBuilder.Entity<VKetQuaPhienBauCu>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vKetQuaPhienBauCu");

            entity.Property(e => e.PhienBauCuId).HasColumnName("PhienBauCuID");
            entity.Property(e => e.UngCuVienId).HasColumnName("UngCuVienID");
        });

        modelBuilder.Entity<VPhieuBau>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vPhieuBau");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.SdtcuTri)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("SDTCuTri");
        });

        modelBuilder.Entity<VTrangThaiPbc>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vTrangThai_PBC");

            entity.Property(e => e.CuocBauCuId).HasColumnName("CuocBauCuID");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.NgayBatDau).HasColumnType("datetime");
            entity.Property(e => e.NgayKetThuc).HasColumnType("datetime");
            entity.Property(e => e.TrangThai)
                .HasMaxLength(12)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VaiTro>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__VaiTro__3214EC27D51DCEC4");

            entity.ToTable("VaiTro");

            entity.HasIndex(e => e.TenVaiTro, "UQ__VaiTro__1DA5581450B576A1").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.TenVaiTro).HasMaxLength(150);
        });

        modelBuilder.Entity<VaiTroChucNang>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__VaiTroCh__3214EC2722F02E55");

            entity.ToTable("VaiTroChucNang");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ChucNangId).HasColumnName("ChucNangID");
            entity.Property(e => e.VaiTroId).HasColumnName("VaiTroID");

            entity.HasOne(d => d.ChucNang).WithMany(p => p.VaiTroChucNangs)
                .HasForeignKey(d => d.ChucNangId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__VaiTroChu__ChucN__5EBF139D");

            entity.HasOne(d => d.VaiTro).WithMany(p => p.VaiTroChucNangs)
                .HasForeignKey(d => d.VaiTroId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__VaiTroChu__VaiTr__5DCAEF64");
        });

        modelBuilder.Entity<ViTriUngCu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ViTriUng__3214EC273EF067E8");

            entity.ToTable("ViTriUngCu");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CuocBauCuId).HasColumnName("CuocBauCuID");
            entity.Property(e => e.PhienBauCuId).HasColumnName("PhienBauCuID");

            entity.HasOne(d => d.CuocBauCu).WithMany(p => p.ViTriUngCus)
                .HasForeignKey(d => d.CuocBauCuId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ViTriUngC__CuocB__3F466844");

            entity.HasOne(d => d.PhienBauCu).WithMany(p => p.ViTriUngCus)
                .HasForeignKey(d => d.PhienBauCuId)
                .HasConstraintName("FK__ViTriUngC__Phien__3E52440B");
        });

        modelBuilder.Entity<VtrangThaiCbc>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VTrangThai_CBC");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.NgayBatDau).HasColumnType("datetime");
            entity.Property(e => e.NgayKetThuc).HasColumnType("datetime");
            entity.Property(e => e.TrangThai)
                .HasMaxLength(12)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
