using DataManagementService.Application.Dtos;

namespace DataManagementService.Application.Interfaces
{
    public interface ISupplementService
    {
        Task<SupplementDto> Add(SupplementDto model);
        Task<SupplementDto> Update(int id, SupplementDto model);
        Task<bool> Delete(int id);

        Task<SupplementDto[]> GetAllAsync(bool getFullData = false);
        Task<SupplementDto> GetByIdAsync(int id);
        Task<SupplementDto> GetByNameAsync(string name);
    }
}