using DataManagementService.Application.Dtos;

namespace DataManagementService.Application.Interfaces
{
    public interface IMealService
    {
        Task<MealDto> Add(MealDto model);
        Task<MealDto> Update(int id, MealDto model);
        Task<bool> Delete(int id);

        Task<MealDto[]> GetAllAsync(bool getFullData = false);
        Task<MealDto> GetByIdAsync(int id);
        Task<MealDto> GetByNameAsync(string name);
    }
}