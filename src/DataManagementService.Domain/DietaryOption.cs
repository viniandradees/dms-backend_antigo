using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DataManagementService.Domain
{
    [Table("dietary_option")]
    public class DietaryOption
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


        public IEnumerable<DietaryOptionFoodAttribute> Incompatibilities { get; set; }
        public IEnumerable<MealDietaryOption> MealDietaryOptions { get; set; }
    }
}