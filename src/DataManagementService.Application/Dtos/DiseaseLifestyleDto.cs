using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DataManagementService.Domain;
using DataManagementService.Domain.Enum;

namespace DataManagementService.Application.Dtos
{
    public class DiseaseLifestyleDto
    {
        public int Id { get; set; }
        public int DiseaseId { get; set; }
        public int LifestyleId { get; set; }
        public PatientAction Action { get; set; }
        public PatientActionIntensity ActionIntensity { get; set; }
        public string MoreDetails { get; set; }

        public Disease Disease { get; set; }
        public Lifestyle Lifestyle { get; set; }
    }
}