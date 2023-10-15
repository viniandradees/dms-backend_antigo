using DataManagementService.Application.Dtos;

namespace DataManagementService.Application.Interfaces
{
    public interface IDietaryOptionFoodAttributeService
    {
        Task<DietaryOptionFoodAttributeDto> Add(DietaryOptionFoodAttributeDto model);
        Task<bool> Delete(int id);
    }
}