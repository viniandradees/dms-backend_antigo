using DataManagementService.Domain;
using DataManagementService.Domain.Enum;

namespace DataManagementService.Application.Dtos
{
    public class ExamResultReferenceDto
    {
        public int Id { get; set; }
        public int ExamId { get; set; }
        public int MeasurementUnitId { get; set; }
        public decimal MinimumReference { get; set; }
        public decimal MaximumReference { get; set; }
       
        public IEnumerable<ExamResultReferenceVariation> Variations { get; set; }
        public IEnumerable<ExamResultReferenceCountry> Countries { get; set; }
        public Exam Exam { get; set; }
        public MeasurementUnit MeasurementUnit { get; set; }
    }
}