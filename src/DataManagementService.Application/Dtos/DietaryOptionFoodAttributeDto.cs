using DataManagementService.Domain;

namespace DataManagementService.Application.Dtos
{
    public class DietaryOptionFoodAttributeDto
    {
        public int Id { get; set; }
        public int DietaryOptionId { get; set; }
        public int FoodAttributeId { get; set; }

        public DietaryOption DietaryOption { get; set; }
        public FoodAttribute FoodAttribute { get; set; }
    }
}