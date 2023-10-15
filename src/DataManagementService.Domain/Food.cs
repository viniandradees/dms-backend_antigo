using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DataManagementService.Domain
{
    [Table("food")]
    public class Food
    {
        [Key]
        [Column("id", Order = 1)]
        [JsonPropertyOrder(1)]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        [Column("name", TypeName = "varchar(50)", Order = 2)]
        [JsonPropertyOrder(2)]
        public string Name { get; set; }

        [StringLength(1000)]
        [Column("description", TypeName = "varchar(1000)", Order = 3)]
        [JsonPropertyOrder(3)]
        public string Description { get; set; }

        [StringLength(1000)]
        [Column("interactions", TypeName = "varchar(1000)", Order = 4)]
        public string Interactions { get; set; }

        [StringLength(1000)]
        [Column("precautions", TypeName = "varchar(1000)", Order = 5)]
        public string Precautions { get; set; }

        [StringLength(500)]
        [Column("best_time_to_take", TypeName = "varchar(500)", Order = 6)]
        public string BestTimeToTake { get; set; }

        public IEnumerable<FoodDisease> SideEffects { get; set; }
        public IEnumerable<FoodRelatedAttribute> Attributes { get; set; }
        public IEnumerable<FoodHealtyObjective> RelatedHealtyObjectives { get; set; }
        public IEnumerable<FoodSupplement> Nutrients { get; set; }
        public IEnumerable<DiseaseFood> TreatableDiseases { get; set; }
        public IEnumerable<ExamFood> RelatedExams { get; set; }
        public IEnumerable<MealFood> FoundIn { get; set; }
    }
}