using DataManagementService.Application.Dtos;

namespace DataManagementService.Application.Interfaces
{
    public interface IExamResultReferenceVariationService
    {
        Task<ExamResultReferenceVariationDto> Add(ExamResultReferenceVariationDto model);
        Task<ExamResultReferenceVariationDto> Update(int id, ExamResultReferenceVariationDto model);
        Task<bool> Delete(int id);
    }
}