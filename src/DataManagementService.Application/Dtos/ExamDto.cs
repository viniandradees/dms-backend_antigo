using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DataManagementService.Domain;

namespace DataManagementService.Application.Dtos
{
    public class ExamDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [StringLength(500)]
        public string Prerequisite { get; set; }

        public IEnumerable<ExamResultReference> ExamResultReferences { get; set; }
        public IEnumerable<DiseaseExam> RelatedDiseases { get; set; }
        public IEnumerable<ExamSupplement> RelatedSupplements { get; set; }
        public IEnumerable<ExamFood> RelatedFoods { get; set; }
        public IEnumerable<ExamLifestyle> RelatedLifestyles { get; set; }
    }
}