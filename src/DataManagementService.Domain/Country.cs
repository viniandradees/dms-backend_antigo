using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using DataManagementService.Domain.Enum;

namespace DataManagementService.Domain
{
    [Table("country")]
    public class Country
    {
        [Key]
        [Column("id", Order = 1)]
        [JsonPropertyOrder(1)]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        [Column("name", TypeName = "varchar(50)", Order = 2)]
        [JsonPropertyOrder(2)]
        public string Name { get; set; }

        [Required]
        [StringLength(10)]
        [Column("acronym", TypeName = "varchar(10)", Order = 3)]
        [JsonPropertyOrder(3)]
        public string Acronym { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 4)]
        [Column("nationality", TypeName = "varchar(50)", Order = 4)]
        [JsonPropertyOrder(4)]
        public string Nationality { get; set; }

        [Column("continent", Order = 5)]
        [JsonPropertyOrder(5)]
        public Continent Continent { get; set; }

        [StringLength(1000)]
        [Column("description", TypeName = "varchar(1000)", Order = 6)]
        [JsonPropertyOrder(6)]
        public string Description { get; set; }


        [JsonPropertyOrder(7)]
        public IEnumerable<MealCountry> MealCountries { get; set; }
    }
}