using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DataManagementService.Domain
{
    [Table("measurement_unit")]
    public class MeasurementUnit
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

        [Required]
        [StringLength(10)]
        [Column("acronym", TypeName = "varchar(10)", Order = 3)]
        [JsonPropertyOrder(3)]
        public string Acronym { get; set; }

        [StringLength(1000)]
        [Column("description", TypeName = "varchar(1000)", Order = 4)]
        [JsonPropertyOrder(4)]
        public string Description { get; set; }
    }
}