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
    [Table("disease_supplement_dosage")]
    public class DiseaseSupplementDosage
    {
        [Key]
        [Column("id", Order = 1)]
        [JsonPropertyOrder(1)]
        public int Id { get; set; }

        [Column("disease_supplement_id", Order = 2)]
        [JsonPropertyOrder(2)]
        public int DiseaseSupplementId { get; set; }

        [Column("measurement_unit_id", Order = 3)]
        [JsonPropertyOrder(3)]
        public int MeasurementUnitId { get; set; }

        [Column("best_time_to_take", TypeName = "varchar(1000)", Order = 4)]
        [JsonPropertyOrder(4)]
        public string BestTimeToTake { get; set; }

        [Column("more_details", TypeName = "varchar(1000)", Order = 5)]
        [JsonPropertyOrder(5)]
        public string MoreDetails { get; set; }

        [JsonPropertyOrder(6)]
        public IEnumerable<DiseaseSupplementDosageAgeRange> AgeRanges { get; set; }

        [JsonPropertyOrder(7)]
        public MeasurementUnit MeasurementUnit { get; set; }

        [JsonPropertyOrder(8)]
        public DiseaseSupplement DiseaseSupplement { get; set; }
    }
}
