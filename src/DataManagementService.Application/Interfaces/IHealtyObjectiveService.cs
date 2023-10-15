using DataManagementService.Application.Dtos;

namespace DataManagementService.Application.Interfaces
{
    public interface IHealtyObjectiveService
    {
        Task<HealtyObjectiveDto> Add(HealtyObjectiveDto model);
        Task<HealtyObjectiveDto> Update(int id, HealtyObjectiveDto model);
        Task<bool> Delete(int id);

        Task<HealtyObjectiveDto[]> GetAllAsync(bool getFullData = false);
        Task<HealtyObjectiveDto> GetByIdAsync(int id);
        Task<HealtyObjectiveDto> GetByNameAsync(string name);
    }
}