using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DataManagementService.Domain
{
    [Table("dietary_option_food_attribute")]
    public class DietaryOptionFoodAttribute
    {
        [Key]
        [Column("id", Order = 1)]
        [JsonPropertyOrder(1)]
        public int Id { get; set; }

        [Column("dietary_option_id", Order = 2)]
        [JsonPropertyOrder(2)]
        public int DietaryOptionId { get; set; }

        [Column("food_attribute_id", Order = 3)]
        [JsonPropertyOrder(3)]
        public int FoodAttributeId { get; set; }


        [JsonPropertyOrder(4)]
        public DietaryOption DietaryOption { get; set; }

        [JsonPropertyOrder(5)]
        public FoodAttribute FoodAttribute { get; set; }
    }
}