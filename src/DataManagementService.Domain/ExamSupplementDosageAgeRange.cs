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
    [Table("exam_supplement_dosage_age_range")]
    public class ExamSupplementDosageAgeRange
    {
        [Key]
        [Column("id", Order = 1)]
        [JsonPropertyOrder(1)]
        public int Id { get; set; }

        [Column("exam_supplement_id", Order = 2)]
        [JsonPropertyOrder(2)]
        public int ExamSupplementDosageId { get; set; }

        [Column("age_unit", Order = 3)]
        [JsonPropertyOrder(3)]
        public AgeTimeUnit AgeTimeUnit { get; set; }

        [Column("time_min", Order = 4)]
        [JsonPropertyOrder(4)]
        public int MinimumAge { get; set; }

        [Column("time_max", Order = 5)]
        [JsonPropertyOrder(5)]
        public int MaximumAge { get; set; }

        [Column("dosage_min", TypeName = "decimal(11,5)", Order = 6)]
        [JsonPropertyOrder(6)]
        public decimal DosageMin { get; set; }

        [Column("dosage_max", TypeName = "decimal(11,5)", Order = 7)]
        [JsonPropertyOrder(7)]
        public decimal DosageMax { get; set; }

        [Column("max_usage_period", Order = 8)]
        [JsonPropertyOrder(8)]
        public int MaxUsagePeriod { get; set; } // in days

        [Column("recommended_first_quarter", Order = 9)]
        [JsonPropertyOrder(9)]
        public bool RecommendedFirstQuarter { get; set; }

        [Column("recommended_second_quarter", Order = 10)]
        [JsonPropertyOrder(10)]
        public bool RecommendedSecondQuarter { get; set; }

        [Column("recommended_third_quarter", Order = 11)]
        [JsonPropertyOrder(11)]
        public bool RecommendedThirdQuarter { get; set; }

        [Column("recommended_fourth_quarter", Order = 12)]
        [JsonPropertyOrder(12)]
        public bool RecommendedFourthQuarter { get; set; }

        [JsonPropertyOrder(13)]
        public ExamSupplementDosage ExamSupplementDosage { get; set; }
    }
}
