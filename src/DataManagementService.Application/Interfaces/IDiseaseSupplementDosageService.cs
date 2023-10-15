using DataManagementService.Application.Dtos;

namespace DataManagementService.Application.Interfaces
{
    public interface IDiseaseSupplementDosageService
    {
        Task<DiseaseSupplementDosageDto> Add(DiseaseSupplementDosageDto model);
        Task<DiseaseSupplementDosageDto> Update(int id, DiseaseSupplementDosageDto model);
        Task<bool> Delete(int id);
    }
}