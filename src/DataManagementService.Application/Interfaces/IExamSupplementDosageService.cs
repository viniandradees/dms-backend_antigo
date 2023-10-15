using DataManagementService.Application.Dtos;

namespace DataManagementService.Application.Interfaces
{
    public interface IExamSupplementDosageService
    {
        Task<ExamSupplementDosageDto> Add(ExamSupplementDosageDto model);
        Task<ExamSupplementDosageDto> Update(int id, ExamSupplementDosageDto model);
        Task<bool> Delete(int id);
    }
}