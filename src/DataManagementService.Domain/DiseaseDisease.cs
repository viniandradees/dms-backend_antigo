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
    [Table("disease_disease")]
    public class DiseaseDisease
    {
        [Key]
        [Column("id", Order = 1)]
        [JsonPropertyOrder(1)]
        public int Id { get; set; }
        [Column("disease_id", Order = 2)]
        public int DiseaseId { get; set; }

        [Column("symptom_id", Order = 3)]
        public int SymptomId { get; set; }

        [Required]
        [Column("symptom_type", Order = 4)]
        public SymptomType SymptomType { get; set; }


        public Disease Disease { get; set; }
        public Disease Symptom { get; set; }
    }
}