using DataManagementService.Application.Dtos;

namespace DataManagementService.Application.Interfaces
{
    public interface IDiseaseFoodDosageService
    {
        Task<DiseaseFoodDosageDto> Add(DiseaseFoodDosageDto model);
        Task<DiseaseFoodDosageDto> Update(int id, DiseaseFoodDosageDto model);
        Task<bool> Delete(int id);
    }
}