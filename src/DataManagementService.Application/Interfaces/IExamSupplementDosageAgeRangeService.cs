using DataManagementService.Application.Dtos;

namespace DataManagementService.Application.Interfaces
{
    public interface IExamSupplementDosageAgeRangeService
    {
        Task<ExamSupplementDosageAgeRangeDto> Add(ExamSupplementDosageAgeRangeDto model);
        Task<ExamSupplementDosageAgeRangeDto> Update(int id, ExamSupplementDosageAgeRangeDto model);
        Task<bool> Delete(int id);
    }
}