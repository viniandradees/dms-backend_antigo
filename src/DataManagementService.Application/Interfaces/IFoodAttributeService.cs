using DataManagementService.Application.Dtos;

namespace DataManagementService.Application.Interfaces
{
    public interface IFoodAttributeService
    {
        Task<FoodAttributeDto> Add(FoodAttributeDto model);
        Task<FoodAttributeDto> Update(int id, FoodAttributeDto model);
        Task<bool> Delete(int id);

        Task<FoodAttributeDto[]> GetAllAsync(bool getFullData = false);
        Task<FoodAttributeDto> GetByIdAsync(int id);
        Task<FoodAttributeDto> GetByNameAsync(string name);
    }
}