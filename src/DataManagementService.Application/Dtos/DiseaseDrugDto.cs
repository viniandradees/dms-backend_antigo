using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DataManagementService.Domain;

namespace DataManagementService.Application.Dtos
{
    public class DiseaseDrugDto
    {
        public int Id { get; set; }
        public int DiseaseId { get; set; }
        public int DrugId { get; set; }

        public Disease Disease { get; set; }
        public Drug Drug { get; set; }
        public IEnumerable<DiseaseDrugDosage> Dosages { get; set; }
    }
}