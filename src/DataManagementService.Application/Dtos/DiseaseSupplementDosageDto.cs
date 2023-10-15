using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DataManagementService.Domain;

namespace DataManagementService.Application.Dtos
{
    public class DiseaseSupplementDosageDto
    {
        public int Id { get; set; }
        public int DiseaseSupplementId { get; set; }
        public int MeasurementUnitId { get; set; }
        public string BestTimeToTake { get; set; }
        public string MoreDetails { get; set; }

        public IEnumerable<DiseaseSupplementDosageAgeRangeDto> AgeRanges { get; set; }
        public MeasurementUnitDto MeasurementUnit { get; set; }
        public DiseaseSupplementDto DiseaseSupplement { get; set; }
    }
}