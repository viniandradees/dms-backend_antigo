using DataManagementService.Application.Dtos;

namespace DataManagementService.Application.Interfaces
{
    public interface IMealPeriodService
    {
        Task<MealPeriodDto> Add(MealPeriodDto model);
        Task<bool> Delete(int id);
    }
}