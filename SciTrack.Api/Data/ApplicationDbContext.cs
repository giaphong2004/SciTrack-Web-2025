using Microsoft.EntityFrameworkCore;
using SciTrack.Api.Models; 
namespace SciTrack.Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<TaiSan> TaiSans { get; set; }
        public DbSet<DeTai> DeTais { get; set; }
        public DbSet<KetQuaDeTai> KetQuaDeTais { get; set; }
        public DbSet<TrangThietBi> TrangThietBis { get; set; }
        public DbSet<HopDong> HopDongs { get; set; }
    }
}