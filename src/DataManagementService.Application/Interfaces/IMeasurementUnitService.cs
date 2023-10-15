using DataManagementService.Application.Dtos;

namespace DataManagementService.Application.Interfaces
{
    public interface IMeasurementUnitService
    {
        Task<MeasurementUnitDto> Add(MeasurementUnitDto model);
        Task<MeasurementUnitDto> Update(int id, MeasurementUnitDto model);
        Task<bool> Delete(int id);

        Task<MeasurementUnitDto[]> GetAllAsync(bool getFullData = false);
        Task<MeasurementUnitDto> GetByIdAsync(int id);
        Task<MeasurementUnitDto> GetByNameAsync(string name);
    }
}