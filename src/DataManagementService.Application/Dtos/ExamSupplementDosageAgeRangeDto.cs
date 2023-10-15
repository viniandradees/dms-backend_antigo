using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DataManagementService.Domain;
using DataManagementService.Domain.Enum;

namespace DataManagementService.Application.Dtos
{
    public class ExamSupplementDosageAgeRangeDto
    {
        public int Id { get; set; }
        public int ExamSupplementDosageId { get; set; }
        public AgeTimeUnit AgeTimeUnit { get; set; }
        public int MinimumAge { get; set; }
        public int MaximumAge { get; set; }
        public decimal DosageMin { get; set; }
        public decimal DosageMax { get; set; }
        public int MaxUsagePeriod { get; set; } // in days
        public bool RecommendedFirstQuarter { get; set; }
        public bool RecommendedSecondQuarter { get; set; }
        public bool RecommendedThirdQuarter { get; set; }
        public bool RecommendedFourthQuarter { get; set; }

        
        public ExamSupplementDosage ExamSupplementDosage { get; set; }
    }
}