using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DataManagementService.Domain
{
    [Table("disease_drug")]
    public class DiseaseDrug
    {
        [Key]
        [Column("id", Order = 1)]
        [JsonPropertyOrder(1)]
        public int Id { get; set; }

        [Column("disease_id", Order = 2)]
        [JsonPropertyOrder(2)]
        public int DiseaseId { get; set; }

        [Column("drug_id", Order = 3)]
        [JsonPropertyOrder(3)]
        public int DrugId { get; set; }


        [ForeignKey("DiseaseId")]
        [JsonPropertyOrder(4)]
        public Disease Disease { get; set; }

        [ForeignKey("DrugId")]
        [JsonPropertyOrder(5)]
        public Drug Drug { get; set; }
        [JsonPropertyOrder(6)]
        public IEnumerable<DiseaseDrugDosage> Dosages { get; set; }
    }
}