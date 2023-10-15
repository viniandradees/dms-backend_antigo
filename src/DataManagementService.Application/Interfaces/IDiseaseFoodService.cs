using DataManagementService.Application.Dtos;

namespace DataManagementService.Application.Interfaces
{
    public interface IDiseaseFoodService
    {
        Task<DiseaseFoodDto> Add(DiseaseFoodDto model);
        Task<bool> Delete(int id);
    }
}