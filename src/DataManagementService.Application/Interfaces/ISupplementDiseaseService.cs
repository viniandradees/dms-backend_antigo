using DataManagementService.Application.Dtos;

namespace DataManagementService.Application.Interfaces
{
    public interface ISupplementDiseaseService
    {
        Task<SupplementDiseaseDto> Add(SupplementDiseaseDto model);
        Task<bool> Delete(int id);
    }
}