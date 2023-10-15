using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using DataManagementService.Domain.Enum;

namespace DataManagementService.Domain
{
    [Table("exam_result_reference_country")]
    public class ExamResultReferenceCountry
    {
        [Key]
        [Column("id", Order = 1)]
        [JsonPropertyOrder(1)]
        public int Id { get; set; }

        [Column("exam_result_reference_id", Order = 2)]
        [JsonPropertyOrder(2)]
        public int ExamResultReferenceId { get; set; }

        [Column("country_id", Order = 3)]
        [JsonPropertyOrder(3)]
        public int CountryId { get; set; }

        public ExamResultReference ExamResultReference { get; set; }
        public Country Country { get; set; }

    }
}