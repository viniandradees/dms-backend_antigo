using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DataManagementService.Domain;

namespace DataManagementService.Application.Dtos
{
    public class ExamSupplementDosageDto
    {
        public int Id { get; set; }
        public int ExamSupplementId { get; set; }
        public int MeasurementUnitId { get; set; }
        public string BestTimeToTake { get; set; }
        public string MoreDetails { get; set; }

        public IEnumerable<ExamSupplementDosageAgeRangeDto> AgeRanges { get; set; }
        public MeasurementUnitDto MeasurementUnit { get; set; }
        public ExamSupplementDto ExamSupplement { get; set; }
    }
}