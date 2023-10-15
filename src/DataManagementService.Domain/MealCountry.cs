using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DataManagementService.Domain
{
    [Table("meal_country")]
    public class MealCountry
    {
        [Key]
        [Column("id", Order = 1)]
        [JsonPropertyOrder(1)]
        public int Id { get; set; }

        [Column("meal_id", Order = 2)]
        [JsonPropertyOrder(2)]
        public int MealId { get; set; }

        [Column("country_id", Order = 3)]
        [JsonPropertyOrder(3)]
        public int CountryId { get; set; }


        [JsonPropertyOrder(4)]
        public Meal Meal { get; set; }

        [JsonPropertyOrder(5)]
        public Country Country { get; set; }
    }
}