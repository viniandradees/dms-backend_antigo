using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using DataManagementService.Domain.Enum;

namespace DataManagementService.Domain
{
    [Table("exam_result_reference_variation")]
    public class ExamResultReferenceVariation
    {
        [Key]
        [Column("id", Order = 1)]
        [JsonPropertyOrder(1)]
        public int Id { get; set; }

        [Column("exam_result_reference_id", Order = 2)]
        [JsonPropertyOrder(2)]
        public int ExamResultReferenceId { get; set; }

        [Column("minimum_reference", TypeName = "decimal(11,5)", Order = 3)]
        [JsonPropertyOrder(3)]
        public int MinimumReference { get; set; }

        [Column("maximum_reference", TypeName = "decimal(11,5)", Order = 4)]
        [JsonPropertyOrder(4)]
        public int MaximumReference { get; set; }

        [Column("age_unit", Order = 5)]
        [JsonPropertyOrder(5)]
        public AgeTimeUnit AgeTimeUnit { get; set; }

        [Column("patient_minimum_age", Order = 6)]
        [JsonPropertyOrder(6)]
        public int PatientMinimumAge { get; set; }

        [Column("patient_maximum_age", Order = 7)]
        [JsonPropertyOrder(7)]
        public int PatientMaximumAge { get; set; }

        [Column("gender", Order = 8)]
        [JsonPropertyOrder(8)]
        public Gender Gender { get; set; }

        [Column("pregnancy_required", Order = 9)]
        [JsonPropertyOrder(9)]
        public bool PregnancyRequired { get; set; }

        [Column("menopause_required", Order = 10)]
        [JsonPropertyOrder(10)]
        public bool MenopauseRequired { get; set; }


        public ExamResultReference ExamResultReference { get; set; }
    }
}