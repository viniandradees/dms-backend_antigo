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
    [Table("food_related_attribute")]
    public class FoodRelatedAttribute
    {
        [Key]
        [Column("id", Order = 1)]
        [JsonPropertyOrder(1)]
        public int Id { get; set; }
        [Column("food_id", Order = 2)]
        public int FoodId { get; set; }
        
        [Column("food_attribute_id", Order = 3)]
        public int FoodAttributeId { get; set; }

        public Food Food { get; set; }
        public FoodAttribute FoodAttribute { get; set; }
    }
}