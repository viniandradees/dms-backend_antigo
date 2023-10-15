using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DataManagementService.Domain;
using DataManagementService.Domain.Enum;

namespace DataManagementService.Application.Dtos
{
    public class DiseaseDiseaseDto
    {
        public int Id { get; set; }
        public int DiseaseId { get; set; }
        public int SymptomId { get; set; }
        public SymptomType SymptomType { get; set; }

        public Disease Disease { get; set;}
        public Disease Symptom { get; set; }
    }
}