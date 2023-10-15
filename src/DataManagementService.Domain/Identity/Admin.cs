using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DataManagementService.Domain.Identity
{
    public class Admin
    {
        [Key]
        [Column("id", Order = 1)]
        [JsonPropertyOrder(1)]
        public Guid Id { get; set; }

        [StringLength(1000)]
        [Column("description", TypeName = "varchar(1000)", Order = 3)]
        [JsonPropertyOrder(3)]
        public string description { get; set; }
    }
}