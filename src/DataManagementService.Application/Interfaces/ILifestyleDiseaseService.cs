using DataManagementService.Application.Dtos;

namespace DataManagementService.Application.Interfaces
{
    public interface ILifestyleDiseaseService
    {
        Task<LifestyleDiseaseDto> Add(LifestyleDiseaseDto model);
        Task<bool> Delete(int id);
    }
}