using Microsoft.EntityFrameworkCore;
using SciTrack.Api.Models;

namespace SciTrack.Api.Data
{
    public class KHCN_DBContext : DbContext
    {
        public KHCN_DBContext(DbContextOptions<KHCN_DBContext> options) : base(options) { }

        public DbSet<HopDong> HopDongs { get; set; }
        public DbSet<TBKHCN> TTBKHCNs { get; set; }
        public DbSet<DeTai> DeTais { get; set; }
        public DbSet<KetQuaDeTai> KetQuaDeTais { get; set; }
        public DbSet<TaiSan> TaiSans { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // CHỈ GIỮ 2 QUAN HỆ ĐÚNG
            modelBuilder.Entity<KetQuaDeTai>()
                .HasMany(kq => kq.DeTais)
                .WithOne(dt => dt.KetQua)
                .HasForeignKey(dt => dt.KetQuaDeTai)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<DeTai>()
                .HasMany(dt => dt.TaiSans)
                .WithOne(ts => ts.DeTai)
                .HasForeignKey(ts => ts.MaSoDeTaiKHCN)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}