using DataManagementService.Application.Dtos;

namespace DataManagementService.Application.Interfaces
{
    public interface IMealCountryService
    {
        Task<MealCountryDto> Add(MealCountryDto model);
        Task<bool> Delete(int id);
    }
}