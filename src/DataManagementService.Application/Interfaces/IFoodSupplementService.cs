using DataManagementService.Application.Dtos;

namespace DataManagementService.Application.Interfaces
{
    public interface IFoodSupplementService
    {
        Task<FoodSupplementDto> Add(FoodSupplementDto model);
        Task<FoodSupplementDto> Update(int id, FoodSupplementDto model);
        Task<bool> Delete(int id);
    }
}