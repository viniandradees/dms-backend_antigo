using DataManagementService.Application.Dtos;

namespace DataManagementService.Application.Interfaces
{
    public interface IDiseaseDrugDosageService
    {
        Task<DiseaseDrugDosageDto> Add(DiseaseDrugDosageDto model);
        Task<DiseaseDrugDosageDto> Update(int id, DiseaseDrugDosageDto model);
        Task<bool> Delete(int id);
    }
}