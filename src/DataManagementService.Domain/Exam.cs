using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DataManagementService.Domain
{
    [Table("exam")]
    public class Exam
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

        [StringLength(500)]
        [Column("prerequisite", TypeName = "varchar(500)", Order = 4)]
        public string Prerequisite { get; set; }

        public IEnumerable<ExamResultReference> ExamResultReferences { get; set; }
        public IEnumerable<DiseaseExam> RelatedDiseases { get; set; }
        public IEnumerable<ExamSupplement> RelatedSupplements { get; set; }
        public IEnumerable<ExamFood> RelatedFoods { get; set; }
        public IEnumerable<ExamLifestyle> RelatedLifestyles { get; set; }
    }
}