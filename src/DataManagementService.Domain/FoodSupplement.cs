using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DataManagementService.Domain
{
    [Table("food_supplement")]
    public class FoodSupplement
    {
        [Key]
        [Column("id", Order = 1)]
        [JsonPropertyOrder(1)]
        public int Id { get; set; }
        [Column("food_id", Order = 2)]
        public int FoodId { get; set; }

        [Column("supplement_id", Order = 3)]
        public int SupplementId { get; set; }

        [Column("quantity", TypeName = "decimal(11,5)", Order = 4)]
        public decimal Quantity { get; set; }

        [Column("measurement_unit_id", Order = 5)]
        public int MeasurementUnitId { get; set; }


        public Food Food { get; set; }
        public Supplement Supplement { get; set; }
        public MeasurementUnit MeasurementUnit { get; set; }
    }
}