using DataManagementService.Domain;

namespace DataManagementService.Application.Dtos
{
    public class DrugDiseaseDto
    {
        public int Id { get; set; }
        public int DrugId { get; set; }
        public int DiseaseId { get; set; }
        
        public Disease Disease { get; set; }
        public Drug Drug { get; set; }
    }
}