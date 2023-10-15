using DataManagementService.Application.Dtos;

namespace DataManagementService.Application.Interfaces
{
    public interface IDrugDiseaseService
    {
        Task<DrugDiseaseDto> Add(DrugDiseaseDto model);
        Task<bool> Delete(int id);
    }
}