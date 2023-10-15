using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DataManagementService.Domain
{
    [Table("food_healty_objective")]
    public class FoodHealtyObjective
    {
        [Key]
        [Column("id", Order = 1)]
        [JsonPropertyOrder(1)]
        public int Id { get; set; }

        [Column("food_id", Order = 2)]
        public int FoodId { get; set; }

        [Column("healty_objective_id", Order = 3)]
        public int HealtyObjectiveId { get; set; }


        public Food Food { get; set; }
        public HealtyObjective HealtyObjective { get; set; }
    }
}