using DataManagementService.Application.Dtos;

namespace DataManagementService.Application.Interfaces
{
    public interface IDietaryOptionService
    {
        Task<DietaryOptionDto> Add(DietaryOptionDto model);
        Task<DietaryOptionDto> Update(int id, DietaryOptionDto model);
        Task<bool> Delete(int id);

        Task<DietaryOptionDto[]> GetAllAsync(bool getFullData = false);
        Task<DietaryOptionDto> GetByIdAsync(int id);
        Task<DietaryOptionDto> GetByNameAsync(string name);
    }
}