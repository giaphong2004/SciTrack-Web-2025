using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SciTrack.Api.Models;

namespace SciTrack.Api.Data;

public partial class KhcnDbNewContext : DbContext
{
    public KhcnDbNewContext()
    {
    }

    public KhcnDbNewContext(DbContextOptions<KhcnDbNewContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Dtkhcn> Dtkhcns { get; set; }

    public virtual DbSet<Hdkhcn> Hdkhcns { get; set; }

    public virtual DbSet<Kqdt> Kqdts { get; set; }

    public virtual DbSet<LienKetKqdtHd> LienKetKqdtHds { get; set; }

    public virtual DbSet<Tskhcn> Tskhcns { get; set; }

    public virtual DbSet<Ttbkhcn> Ttbkhcns { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Dtkhcn>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DTKHCN__3214EC27DDB71806");

            entity.ToTable("DTKHCN");

            entity.HasIndex(e => e.MaDeTai, "UQ__DTKHCN__9F967D5AE20E0887").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CacQuyetDinh).HasMaxLength(255);
            entity.Property(e => e.HaoMonLienQuan).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.KinhPhiGiaoKhoanChuyen).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.KinhPhiThucHien).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.KinhPhiVatTuTieuHao).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.MaDeTai).HasMaxLength(30);
            entity.Property(e => e.QuyetDinhXuLy).HasMaxLength(255);
            entity.Property(e => e.TenDtkhcn)
                .HasMaxLength(255)
                .HasColumnName("TenDTKHCN");

            entity.HasOne(d => d.KetQuaDeTaiNavigation).WithMany(p => p.Dtkhcns)
                .HasForeignKey(d => d.KetQuaDeTai)
                .HasConstraintName("FK__DTKHCN__KetQuaDe__47DBAE45");
        });

        modelBuilder.Entity<Hdkhcn>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__HDKHCN__3214EC272165E246");

            entity.ToTable("HDKHCN");

            entity.HasIndex(e => e.MaHopDong, "UQ__HDKHCN__36DD43437C4D0342").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ChiPhiHoatDongChuyenMon).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ChiPhiKetQuaDeTai).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ChiPhiTrangThietBi).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.LoiNhuan).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.MaHopDong).HasMaxLength(50);
            entity.Property(e => e.TenDoiTac).HasMaxLength(255);
            entity.Property(e => e.TongGiaTriHopDong).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<Kqdt>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__KQDT__3214EC2712B7398D");

            entity.ToTable("KQDT");

            entity.HasIndex(e => e.MaKetQua, "UQ__KQDT__D5B3102B3970DB92").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DinhGia).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.GiaTriConLai).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.MaKetQua).HasMaxLength(50);
            entity.Property(e => e.PhanLoai).HasMaxLength(100);
            entity.Property(e => e.TenKetQua).HasMaxLength(255);
        });

        modelBuilder.Entity<LienKetKqdtHd>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LienKet___3214EC273D96B375");

            entity.ToTable("LienKet_KQDT_HD");

            entity.HasIndex(e => new { e.KqdtId, e.HdkhcnId }, "UQ_LienKet_KQDT_HD").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.HdkhcnId).HasColumnName("HDKHCN_ID");
            entity.Property(e => e.KqdtId).HasColumnName("KQDT_ID");

            entity.HasOne(d => d.Hdkhcn).WithMany(p => p.LienKetKqdtHds)
                .HasForeignKey(d => d.HdkhcnId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LienKet_K__HDKHC__412EB0B6");

            entity.HasOne(d => d.Kqdt).WithMany(p => p.LienKetKqdtHds)
                .HasForeignKey(d => d.KqdtId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LienKet_K__KQDT___403A8C7D");
        });

        modelBuilder.Entity<Tskhcn>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TSKHCN__3214EC2783DFB770");

            entity.ToTable("TSKHCN");

            entity.HasIndex(e => e.SoDanhMuc, "UQ__TSKHCN__51A213991B81C498").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.GiaTriConLai).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.HaoMon).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.KhauHao).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.MaSoDeTaiKhcn).HasColumnName("MaSoDeTaiKHCN");
            entity.Property(e => e.NguyenGia).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SoDanhMuc).HasMaxLength(50);
            entity.Property(e => e.Ten).HasMaxLength(255);
            entity.Property(e => e.TrangThaiTaiSan).HasMaxLength(100);

            entity.HasOne(d => d.MaSoDeTaiKhcnNavigation).WithMany(p => p.Tskhcns)
                .HasForeignKey(d => d.MaSoDeTaiKhcn)
                .HasConstraintName("FK__TSKHCN__MaSoDeTa__4CA06362");
        });

        modelBuilder.Entity<Ttbkhcn>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TTBKHCN__3214EC274BAEF083");

            entity.ToTable("TTBKHCN");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DtHdKhcnLienQuan)
                .HasMaxLength(255)
                .HasColumnName("DT_HD_KHCN_LienQuan");
            entity.Property(e => e.GiaTriConLai).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.KhauHao).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.MaThietBi).HasMaxLength(50);
            entity.Property(e => e.NguyenGia).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TenThietBi).HasMaxLength(255);
            entity.Property(e => e.TinhTrangThietBi).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
