using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DataManagementService.Domain;

namespace DataManagementService.Application.Dtos
{
    public class DiseaseFoodDto
    {
        public int Id { get; set; }
        public int DiseaseId { get; set; }
        public int FoodId { get; set; }

        public Disease Disease { get; set; }
        public Food Food { get; set; }
        public IEnumerable<DiseaseFoodDosage> Dosages { get; set; }
    }
}