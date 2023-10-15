using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DataManagementService.Domain
{
    [Table("meal")]
    public class Meal
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
        [Column("preparation_method", TypeName = "varchar(1000)", Order = 4)]
        public string PreparationMethod { get; set; }

        [Column("total_calories", Order = 5)]
        public int TotalCalories { get; set; }

        public IEnumerable<MealFood> Ingredients { get; set; }
        public IEnumerable<MealPeriod> MealPeriods { get; set; }
        public IEnumerable<MealCountry> InternationalCuisines { get; set; }
        public IEnumerable<MealDietaryOption> MealDietaryOptions { get; set; }
    }
}