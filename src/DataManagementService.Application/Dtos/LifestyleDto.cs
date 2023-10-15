using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DataManagementService.Domain;

namespace DataManagementService.Application.Dtos
{
    public class LifestyleDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Interactions { get; set; }
        public string Precautions { get; set; }

        public IEnumerable<LifestyleDisease> SideEffects { get; set; }
        public IEnumerable<DiseaseLifestyle> RelatedDiseases { get; set; }
        public IEnumerable<ExamLifestyle> RelatedExams { get; set; }
    }
}