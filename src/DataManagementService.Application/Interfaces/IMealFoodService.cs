using DataManagementService.Application.Dtos;

namespace DataManagementService.Application.Interfaces
{
    public interface IMealFoodService
    {
        Task<MealFoodDto> Add(MealFoodDto model);
        Task<MealFoodDto> Update(int id, MealFoodDto model);
        Task<bool> Delete(int id);
    }
}