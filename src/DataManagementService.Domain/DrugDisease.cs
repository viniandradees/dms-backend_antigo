using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DataManagementService.Domain
{
    [Table("drug_disease")]
    public class DrugDisease
    {
        [Key]
        [Column("id", Order = 1)]
        [JsonPropertyOrder(1)]
        public int Id { get; set; }

        [Column("drug_id", Order = 2)]
        [JsonPropertyOrder(2)]
        public int DrugId { get; set; }

        [Column("disease_id", Order = 3)]
        [JsonPropertyOrder(3)]
        public int DiseaseId { get; set; }


        [JsonPropertyOrder(4)]
        public Disease Disease { get; set; }

        [JsonPropertyOrder(5)]
        public Drug Drug { get; set; }
    }
}