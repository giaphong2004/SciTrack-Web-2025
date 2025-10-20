using Microsoft.EntityFrameworkCore;
using SciTrack.Api.Models;

namespace SciTrack.Api.Data
{
    public class KHCN_DBContext : DbContext
    {
        public KHCN_DBContext(DbContextOptions<KHCN_DBContext> options) : base(options)
        {
        }

        public DbSet<HopDong> HopDongs { get; set; }
        public DbSet<ThietBi> ThietBis { get; set; }
        public DbSet<DeTai> DeTais { get; set; }
        public DbSet<KetQuaDeTai> KetQuaDeTais { get; set; }
        public DbSet<TaiSan> TaiSans { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Cấu hình mối quan hệ HDKHCN -> TTBKHCN (Một-Nhiều)
            modelBuilder.Entity<HopDong>()
                .HasMany(hd => hd.ThietBis)
                .WithOne(tb => tb.HopDong)
                .HasForeignKey(tb => tb.MaSoHopDong)
                .OnDelete(DeleteBehavior.SetNull);

            // Cấu hình mối quan hệ HDKHCN -> KQDT (Một-Nhiều)
            modelBuilder.Entity<HopDong>()
                .HasMany(hd => hd.KetQuaDeTais)
                .WithOne(kq => kq.HopDong)
                .HasForeignKey(kq => kq.MaSoThietBi)
                .OnDelete(DeleteBehavior.SetNull);

            // Cấu hình mối quan hệ KQDT -> DTKHCN (Một-Nhiều)
            modelBuilder.Entity<KetQuaDeTai>()
                .HasMany(kq => kq.DeTais)
                .WithOne(dt => dt.KetQua)
                .HasForeignKey(dt => dt.KetQuaDeTai)
                .OnDelete(DeleteBehavior.SetNull);

            // Cấu hình mối quan hệ DTKHCN -> TSKHCN (Một-Nhiều)
            modelBuilder.Entity<DeTai>()
                .HasMany(dt => dt.TaiSans)
                .WithOne(ts => ts.DeTai)
                .HasForeignKey(ts => ts.MaSoDeTaiKHCN)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}