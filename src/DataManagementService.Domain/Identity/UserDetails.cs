using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DataManagementService.Domain.Identity
{
    [Table("user_details")]
    public class UserDetails
    {
        [Key]
        [Column("id", Order = 1)]
        [JsonPropertyOrder(1)]
        public int Id { get; set; }

        [Column("user_id", Order = 2)]
        [JsonPropertyOrder(2)]
        public string UserId { get; set; }

        [Column("image_profile", TypeName = "varchar(1000)", Order = 3)]
        [JsonPropertyOrder(3)]
        public string ImageProfile { get; set; }
        
        public User User { get; set; }
    }
}