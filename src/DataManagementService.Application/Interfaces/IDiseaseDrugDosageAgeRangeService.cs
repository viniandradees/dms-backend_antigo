using DataManagementService.Application.Dtos;

namespace DataManagementService.Application.Interfaces
{
    public interface IDiseaseDrugDosageAgeRangeService
    {
        Task<DiseaseDrugDosageAgeRangeDto> Add(DiseaseDrugDosageAgeRangeDto model);
        Task<DiseaseDrugDosageAgeRangeDto> Update(int id, DiseaseDrugDosageAgeRangeDto model);
        Task<bool> Delete(int id);
    }
}