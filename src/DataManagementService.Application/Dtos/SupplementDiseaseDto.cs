using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataManagementService.Domain;

namespace DataManagementService.Application.Dtos
{
    public class SupplementDiseaseDto
    {
        public int Id { get; set; }
        public int SupplementId { get; set; }
        public int DiseaseId { get; set; }
        
        public Disease Disease { get; set; }
        public Supplement Supplement { get; set; }
    }
}