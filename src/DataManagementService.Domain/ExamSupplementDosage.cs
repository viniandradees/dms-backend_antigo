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
    [Table("exam_supplement_dosage")]
    public class ExamSupplementDosage
    {
        [Key]
        [Column("id", Order = 1)]
        [JsonPropertyOrder(1)]
        public int Id { get; set; }

        [Column("exam_supplement_id", Order = 2)]
        [JsonPropertyOrder(2)]
        public int ExamSupplementId { get; set; }

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
        public IEnumerable<ExamSupplementDosageAgeRange> AgeRanges { get; set; }

        [JsonPropertyOrder(7)]
        public MeasurementUnit MeasurementUnit { get; set; }

        [JsonPropertyOrder(8)]
        public ExamSupplement ExamSupplement { get; set; }
    }
}
