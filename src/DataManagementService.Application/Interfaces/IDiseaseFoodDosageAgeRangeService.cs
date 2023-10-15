using DataManagementService.Application.Dtos;

namespace DataManagementService.Application.Interfaces
{
    public interface IDiseaseFoodDosageAgeRangeService
    {
        Task<DiseaseFoodDosageAgeRangeDto> Add(DiseaseFoodDosageAgeRangeDto model);
        Task<DiseaseFoodDosageAgeRangeDto> Update(int id, DiseaseFoodDosageAgeRangeDto model);
        Task<bool> Delete(int id);
    }
}