using DataManagementService.Application.Dtos;

namespace DataManagementService.Application.Interfaces
{
    public interface IDiseaseDiseaseService
    {
        Task<DiseaseDiseaseDto> Add(DiseaseDiseaseDto model);
        Task<DiseaseDiseaseDto> Update(int id, DiseaseDiseaseDto model);
        Task<bool> Delete(int id);
    }
}