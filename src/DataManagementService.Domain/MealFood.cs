using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DataManagementService.Domain
{
    [Table("meal_food")]
    public class MealFood
    {
        [Key]
        [Column("id", Order = 1)]
        [JsonPropertyOrder(1)]
        public int Id { get; set; }
        
        [Column("meal_id", Order = 2)]
        public int MealId { get; set; }
        
        [Column("food_id", Order = 3)]
        public int FoodId { get; set; }

        [Column("food_portion", Order = 4)]
        public decimal FoodPortion { get; set; }

        [Column("measurement_unit_id", Order = 5)]
        public int MeasurementUnitId { get; set; }


        public Meal Meal { get; set; }
        public Food Food { get; set; }
        public MeasurementUnit MeasurementUnit { get; set; }
    }
}