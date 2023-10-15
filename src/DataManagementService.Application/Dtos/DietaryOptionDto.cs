using DataManagementService.Domain;

namespace DataManagementService.Application.Dtos
{
    public class DietaryOptionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
        public IEnumerable<DietaryOptionFoodAttribute> Incompatibilities { get; set; }
        public IEnumerable<MealDietaryOption> MealDietaryOptions { get; set; }
    }
}