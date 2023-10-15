using DataManagementService.Application.Dtos;

namespace DataManagementService.Application.Interfaces
{
    public interface IDiseaseDrugService
    {
        Task<DiseaseDrugDto> Add(DiseaseDrugDto model);
        Task<bool> Delete(int id);
    }
}