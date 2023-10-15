using DataManagementService.Application.Dtos;

namespace DataManagementService.Application.Interfaces
{
    public interface IDiseaseExamService
    {
        Task<DiseaseExamDto> Add(DiseaseExamDto model);
        Task<DiseaseExamDto> Update(int id, DiseaseExamDto model);
        Task<bool> Delete(int id);
    }
}