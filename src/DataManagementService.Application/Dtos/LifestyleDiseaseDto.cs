using DataManagementService.Domain;

namespace DataManagementService.Application.Dtos
{
    public class LifestyleDiseaseDto
    {
        public int Id { get; set; }
        public int LifestyleId { get; set; }
        public int DiseaseId { get; set; }
        
        public Disease Disease { get; set; }
        public Lifestyle Lifestyle { get; set; }
    }
}