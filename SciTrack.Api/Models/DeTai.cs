using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SciTrack.Api.Models
{
    [Table("DTKHCN")]
    public class DeTai
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("Name")]
        public string Ten { get; set; } = string.Empty; 

        [Column("ProjectCode")]
        public string? MaDeTai { get; set; } 

        [Column("LastAssetUpdate")]
        public DateTime? CapNhatTaiSanLanCuoi { get; set; } 

        [Column("DecisionRefs")]
        public string? QuyetDinhThamChieu { get; set; } 

        [Column("BudgetExecution")]
        public decimal? KinhPhiThucHien { get; set; } 

        [Column("BudgetForTraining")]
        public decimal? KinhPhiDaoTao { get; set; } 

        [Column("ConsumablesBudget")]
        public decimal? KinhPhiTieuHao { get; set; } 

        [Column("EquipmentDepreciation")]
        public decimal? KhauHaoThietBi { get; set; } 

        [Column("CreatedAt")]
        public DateTime? NgayTao { get; set; }

        [Column("UpdatedAt")]
        public DateTime? NgayCapNhat { get; set; }

        public virtual ICollection<TaiSan> TaiSans { get; set; } = new List<TaiSan>();
    }
}