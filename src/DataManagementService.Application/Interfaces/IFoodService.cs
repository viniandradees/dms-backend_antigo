using DataManagementService.Application.Dtos;

namespace DataManagementService.Application.Interfaces
{
    public interface IFoodService
    {
        Task<FoodDto> Add(FoodDto model);
        Task<FoodDto> Update(int id, FoodDto model);
        Task<bool> Delete(int id);

        Task<FoodDto[]> GetAllAsync(bool getFullData = false);
        Task<FoodDto> GetByIdAsync(int id);
        Task<FoodDto> GetByNameAsync(string name);
    }
}