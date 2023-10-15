using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DataManagementService.Domain;

namespace DataManagementService.Application.Dtos
{
    public class DiseaseFoodDosageDto
    {
        public int Id { get; set; }
        public int DiseaseFoodId { get; set; }
        public int MeasurementUnitId { get; set; }
        public string BestTimeToTake { get; set; }
        public string MoreDetails { get; set; }

        public IEnumerable<DiseaseFoodDosageAgeRangeDto> AgeRanges { get; set; }
        public MeasurementUnitDto MeasurementUnit { get; set; }
        public DiseaseFoodDto DiseaseFood { get; set; }
    }
}