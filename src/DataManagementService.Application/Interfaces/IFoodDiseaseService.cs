using DataManagementService.Application.Dtos;

namespace DataManagementService.Application.Interfaces
{
    public interface IFoodDiseaseService
    {
        Task<FoodDiseaseDto> Add(FoodDiseaseDto model);
        Task<bool> Delete(int id);
    }
}