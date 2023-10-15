using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DataManagementService.Domain;
using DataManagementService.Domain.Enum;

namespace DataManagementService.Application.Dtos
{
    public class ExamResultReferenceVariationDto
    {
        public int Id { get; set; }
        public int ExamResultReferenceId { get; set; }
        public int MinimumReference { get; set; }
        public int MaximumReference { get; set; }
        public AgeTimeUnit AgeTimeUnit { get; set; }
        public int PatientMinimumAge { get; set; }
        public int PatientMaximumAge { get; set; }
        public Gender Gender { get; set; }
        public bool PregnancyRequired { get; set; }
        public bool MenopauseRequired { get; set; }
        
        public ExamResultReference ExamResultReference { get; set; }
    }
}