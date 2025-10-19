using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SciTrack.Api.Models
{
    [Table("KQDT")]
    public class KetQuaDeTai
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("Name")]
        public string Ten { get; set; } = string.Empty;

        // --- Liên kết Một-Nhiều với DeTai (Đã có) ---
        [Column("ProjectID")]
        public int? DeTaiId { get; set; }
        public virtual DeTai? DeTai { get; set; }

        public virtual ICollection<TaiSan> TaiSans { get; set; } = new List<TaiSan>();

        public virtual ICollection<HopDong> HopDongs { get; set; } = new List<HopDong>();
    }
}