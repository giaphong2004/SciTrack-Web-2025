using Microsoft.EntityFrameworkCore;
using SciTrack.Api.Models;

namespace SciTrack.Api.Data
{
    public class KHCN_DBContext : DbContext
    {
        public KHCN_DBContext(DbContextOptions<KHCN_DBContext> options) : base(options)
        {
        }

        // Đăng ký tất cả các Model với "Người quản lý"
        public DbSet<HopDong> HopDongs { get; set; }
        public DbSet<ThietBi> ThietBis { get; set; }
        public DbSet<DeTai> DeTais { get; set; }
        public DbSet<KetQuaDeTai> KetQuaDeTais { get; set; }
        public DbSet<TaiSan> TaiSans { get; set; }

        // Cấu hình chi tiết các mối quan hệ (quan trọng nhất!)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Cấu hình mối quan hệ Nhiều-Nhiều giữa Tài sản và Kết quả Đề tài
            // thông qua bảng trung gian ASSET_RESULT
            modelBuilder.Entity<TaiSan>()
                .HasMany(ts => ts.KetQuaDeTais)
                .WithMany(kq => kq.TaiSans)
                .UsingEntity(j => j.ToTable("ASSET_RESULT"));

            // Cấu hình mối quan hệ Nhiều-Nhiều giữa Hợp đồng và Đề tài
            // thông qua bảng trung gian DTKHCN_CONTRACT
            modelBuilder.Entity<HopDong>()
                .HasMany(hd => hd.DeTais)
                .WithMany(dt => dt.HopDongs)
                .UsingEntity(j => j.ToTable("DTKHCN_CONTRACT"));

            // Cấu hình mối quan hệ Nhiều-Nhiều giữa Hợp đồng và Kết quả Đề tài
            // thông qua bảng trung gian KQDT_CONTRACT
            modelBuilder.Entity<HopDong>()
                .HasMany(hd => hd.KetQuaDeTais)
                .WithMany(kq => kq.HopDongs)
                .UsingEntity(j => j.ToTable("KQDT_CONTRACT"));
        }
    }
}