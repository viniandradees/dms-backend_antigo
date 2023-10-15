using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DataManagementService.Domain
{
    [Table("disease")]
    public class Disease
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

        [StringLength(1000)]
        [Column("description", TypeName = "varchar(1000)", Order = 3)]
        [JsonPropertyOrder(3)]
        public string Description { get; set; }

        [Column("most_indicated_treatment", TypeName = "varchar(1000)", Order = 4)]
        [JsonPropertyOrder(4)]
        [StringLength(1000)]
        public string MostIndicatedTreatment { get; set; }
        
        
        [JsonPropertyOrder(5)]
        public IEnumerable<DiseaseDrug> TreatmentDrugs { get; set; }
        
        [JsonPropertyOrder(6)]
        public IEnumerable<DiseaseSupplement> TreatmentSupplements { get; set; }
        
        [JsonPropertyOrder(7)]
        public IEnumerable<DiseaseFood> TreatmentFoods { get; set; }
        
        [JsonPropertyOrder(8)]
        public IEnumerable<DiseaseLifestyle> TreatmentLifestyles { get; set; }
        
        [JsonPropertyOrder(9)]
        public IEnumerable<DiseaseExam> DiagnoseExams { get; set; }
        
        [JsonPropertyOrder(10)]
        public IEnumerable<DiseaseDisease> DiagnoseSymptoms { get; set; }
        
        [JsonPropertyOrder(11)]
        public IEnumerable<DiseaseDisease> SymptomOfDiseases { get; set; }
        
        [JsonPropertyOrder(12)]
        public IEnumerable<DrugDisease> SideEffectOfDrugs { get; set; }
        
        [JsonPropertyOrder(13)]
        public IEnumerable<SupplementDisease> SideEffectOfSupplements { get; set; }
        
        [JsonPropertyOrder(14)]
        public IEnumerable<FoodDisease> SideEffectOfFoods { get; set; }
        
        [JsonPropertyOrder(15)]
        public IEnumerable<LifestyleDisease> SideEffectOfLifestyles { get; set; }
    }
}