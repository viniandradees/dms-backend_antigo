using DataManagementService.Application.Dtos;

namespace DataManagementService.Application.Interfaces
{
    public interface IMealDietaryOptionService
    {
        Task<MealDietaryOptionDto> Add(MealDietaryOptionDto model);
        Task<bool> Delete(int id);
    }
}