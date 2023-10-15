using DataManagementService.Application.Dtos;

namespace DataManagementService.Application.Interfaces
{
    public interface IDrugService
    {
        Task<DrugDto> Add(DrugDto model);
        Task<DrugDto> Update(int id, DrugDto model);
        Task<bool> Delete(int id);

        Task<DrugDto[]> GetAllAsync(bool getFullData = false);
        Task<DrugDto> GetByIdAsync(int id);
        Task<DrugDto> GetByNameAsync(string name);
    }
}