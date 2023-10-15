using DataManagementService.Application.Dtos;

namespace DataManagementService.Application.Interfaces
{
    public interface ILifestyleService
    {
        Task<LifestyleDto> Add(LifestyleDto model);
        Task<LifestyleDto> Update(int id, LifestyleDto model);
        Task<bool> Delete(int id);

        Task<LifestyleDto[]> GetAllAsync(bool getFullData = false);
        Task<LifestyleDto> GetByIdAsync(int id);
        Task<LifestyleDto> GetByNameAsync(string name);
    }
}