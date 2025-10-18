using System.ComponentModel.DataAnnotations;

namespace SciTrack.Api.DTOs
{
    public class DeTaiCreateDto
    {
        [Required]
        public string Ten { get; set; }

        public string? MaDeTai { get; set; }
        public string? DecisionRefs { get; set; }
        public decimal? BudgetExecution { get; set; }
        public decimal? BudgetForTraining { get; set; }
        public decimal? ConsumablesBudget { get; set; }
        public decimal? EquipmentDepreciation { get; set; }
    }
}