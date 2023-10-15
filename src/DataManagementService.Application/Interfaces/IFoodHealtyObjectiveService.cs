using DataManagementService.Application.Dtos;

namespace DataManagementService.Application.Interfaces
{
    public interface IFoodHealtyObjectiveService
    {
        Task<FoodHealtyObjectiveDto> Add(FoodHealtyObjectiveDto model);
        Task<bool> Delete(int id);
    }
}