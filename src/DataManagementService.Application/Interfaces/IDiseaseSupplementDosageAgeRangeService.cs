using DataManagementService.Application.Dtos;

namespace DataManagementService.Application.Interfaces
{
    public interface IDiseaseSupplementDosageAgeRangeService
    {
        Task<DiseaseSupplementDosageAgeRangeDto> Add(DiseaseSupplementDosageAgeRangeDto model);
        Task<DiseaseSupplementDosageAgeRangeDto> Update(int id, DiseaseSupplementDosageAgeRangeDto model);
        Task<bool> Delete(int id);
    }
}