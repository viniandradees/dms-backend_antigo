using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using DataManagementService.Domain.Enum;

namespace DataManagementService.Domain
{
    [Table("exam_result_reference")]
    public class ExamResultReference
    {
        [Key]
        [Column("id", Order = 1)]
        [JsonPropertyOrder(1)]
        public int Id { get; set; }

        [Column("exam_id", Order = 2)]
        [JsonPropertyOrder(2)]
        public int ExamId { get; set; }

        [Column("measurement_unit_id", Order = 3)]
        [JsonPropertyOrder(3)]
        public int MeasurementUnitId { get; set; }

        [Column("minimum_reference", TypeName = "decimal(11,5)", Order = 4)]
        [JsonPropertyOrder(4)]
        public decimal MinimumReference { get; set; }

        [Column("maximum_reference", TypeName = "decimal(11,5)", Order = 5)]
        [JsonPropertyOrder(5)]
        public decimal MaximumReference { get; set; }

        public IEnumerable<ExamResultReferenceVariation> Variations { get; set; }
        public IEnumerable<ExamResultReferenceCountry> Countries { get; set; }
        public Exam Exam { get; set; }
        public MeasurementUnit MeasurementUnit { get; set; }

    }
}