using DataManagementService.Application.Dtos;

namespace DataManagementService.Application.Interfaces
{
    public interface IDiseaseService
    {
        Task<DiseaseDto> Add(DiseaseDto model);
        Task<DiseaseDto> Update(int id, DiseaseDto model);
        Task<bool> Delete(int id);

        Task<DiseaseDto[]> GetAllAsync(bool getFullData = false);
        Task<DiseaseDto> GetByIdAsync(int id);
        Task<DiseaseDto> GetByNameAsync(string name);
    }
}