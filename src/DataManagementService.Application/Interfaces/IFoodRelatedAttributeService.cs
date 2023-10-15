using DataManagementService.Application.Dtos;

namespace DataManagementService.Application.Interfaces
{
    public interface IFoodRelatedAttributeService
    {
        Task<FoodRelatedAttributeDto> Add(FoodRelatedAttributeDto model);
        Task<bool> Delete(int id);
    }
}