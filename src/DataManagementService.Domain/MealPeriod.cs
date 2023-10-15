using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using DataManagementService.Domain.Enum;

namespace DataManagementService.Domain
{
    [Table("meal_period")]
    public class MealPeriod
    {
        [Key]
        [Column("id", Order = 1)]
        [JsonPropertyOrder(1)]
        public int Id { get; set; }
        [Column("meal_id", Order = 2)]
        public int MealId { get; set; }

        [Column("meal_period_id", Order = 3)]
        public MealTime MealTime { get; set; }


        public Meal Meal { get; set; }
    }
}