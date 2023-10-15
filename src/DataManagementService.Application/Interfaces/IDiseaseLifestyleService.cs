using DataManagementService.Application.Dtos;

namespace DataManagementService.Application.Interfaces
{
    public interface IDiseaseLifestyleService
    {
        Task<DiseaseLifestyleDto> Add(DiseaseLifestyleDto model);
        Task<DiseaseLifestyleDto> Update(int id, DiseaseLifestyleDto model);
        Task<bool> Delete(int id);
    }
}