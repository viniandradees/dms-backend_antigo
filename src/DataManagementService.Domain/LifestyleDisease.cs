using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DataManagementService.Domain
{
    [Table("lifestyle_disease")]
    public class LifestyleDisease
    {
        [Key]
        [Column("id", Order = 1)]
        [JsonPropertyOrder(1)]
        public int Id { get; set; }

        [Column("lifestyle_id", Order = 2)]
        [JsonPropertyOrder(2)]
        public int LifestyleId { get; set; }

        [Column("disease_id", Order = 3)]
        [JsonPropertyOrder(3)]
        public int DiseaseId { get; set; }


        [JsonPropertyOrder(4)]
        public Disease Disease { get; set; }

        [JsonPropertyOrder(5)]
        public Lifestyle Lifestyle { get; set; }
    }
}