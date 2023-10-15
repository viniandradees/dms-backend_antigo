using DataManagementService.Domain;

namespace DataManagementService.Application.Dtos
{
    public class FoodDiseaseDto
    {
        public int Id { get; set; }
        public int FoodId { get; set; }
        public int DiseaseId { get; set; }
        
        public Disease Disease { get; set; }
        public Food Food { get; set; }
    }
}