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
        public DateTime? LastAssetUpdate { get; set; }

        [Column("DecisionRefs")]
        public string? DecisionRefs { get; set; }

        [Column("BudgetExecution")]
        public decimal? BudgetExecution { get; set; }

        [Column("BudgetForTraining")]
        public decimal? BudgetForTraining { get; set; }

        [Column("ConsumablesBudget")]
        public decimal? ConsumablesBudget { get; set; }

        [Column("EquipmentDepreciation")]
        public decimal? EquipmentDepreciation { get; set; }

        [Column("CreatedAt")]
        public DateTime? CreatedAt { get; set; }

        [Column("UpdatedAt")]
        public DateTime? UpdatedAt { get; set; }

        // Mối quan hệ: Một Đề tài có nhiều Tài sản
        public virtual ICollection<TaiSan> TaiSans { get; set; } = new List<TaiSan>();
    }
}