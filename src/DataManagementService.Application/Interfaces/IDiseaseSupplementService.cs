using DataManagementService.Application.Dtos;

namespace DataManagementService.Application.Interfaces
{
    public interface IDiseaseSupplementService
    {
        Task<DiseaseSupplementDto> Add(DiseaseSupplementDto model);
        Task<bool> Delete(int id);
    }
}