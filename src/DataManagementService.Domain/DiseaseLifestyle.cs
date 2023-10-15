using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using DataManagementService.Domain.Enum;

namespace DataManagementService.Domain
{
    [Table("disease_lifestyle")]
    public class DiseaseLifestyle
    {
        [Key]
        [Column("id", Order = 1)]
        [JsonPropertyOrder(1)]
        public int Id { get; set; }
        [Column("disease_id", Order = 2)]
        [JsonPropertyOrder(2)]
        public int DiseaseId { get; set; }

        [Column("lifestyle_id", Order = 3)]
        [JsonPropertyOrder(3)]
        public int LifestyleId { get; set; }

        [Column("patient_action", Order = 4)]
        [JsonPropertyOrder(4)]
        public PatientAction Action { get; set; }

        [Column("patient_action_intensity", Order = 5)]
        [JsonPropertyOrder(5)]
        public PatientActionIntensity ActionIntensity { get; set; }

        [Column("more_details", TypeName = "varchar(1000)", Order = 6)]
        [JsonPropertyOrder(6)]
        [StringLength(1000)]
        public string MoreDetails { get; set; }

        public Disease Disease { get; set; }
        public Lifestyle Lifestyle { get; set; }
    }
}