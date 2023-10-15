using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DataManagementService.Domain;

namespace DataManagementService.Application.Dtos
{
    public class DiseaseSupplementDto
    {
        public int Id { get; set; }
        public int DiseaseId { get; set; }
        public int SupplementId { get; set; }

        public Disease Disease { get; set; }
        public Supplement Supplement { get; set; }
        public IEnumerable<DiseaseSupplementDosage> Dosages { get; set; }
    }
}