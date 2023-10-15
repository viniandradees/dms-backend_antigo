using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace DataManagementService.Domain.Identity
{
    public class User : IdentityUser
    {
        [Required]
        [StringLength(20, MinimumLength = 2)]
        [Column("first_name", TypeName = "varchar(20)", Order = 2)]
        [JsonPropertyOrder(2)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2)]
        [Column("last_name", TypeName = "varchar(30)", Order = 3)]
        [JsonPropertyOrder(3)]
        public string LastName { get; set; }

        public UserDetails Details { get; set; }
    }
}