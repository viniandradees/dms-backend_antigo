using DataManagementService.Domain;
using DataManagementService.Domain.Enum;

namespace DataManagementService.Application.Dtos
{
    public class ExamSupplementDto
    {
        public int Id { get; set; }
        public int SupplementId { get; set; }
        public int ExamId { get; set; }
        public ExamResult ExamResult { get; set; }
        public string RecommendedQuarters { get; set; }
        
        public Supplement Supplement { get; set; }
        public Exam Exam { get; set; }
        public IEnumerable<ExamSupplementDosage> Dosages { get; set; }
    }
}