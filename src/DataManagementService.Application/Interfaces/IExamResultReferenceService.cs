using DataManagementService.Application.Dtos;

namespace DataManagementService.Application.Interfaces
{
    public interface IExamResultReferenceService
    {
        Task<ExamResultReferenceDto> Add(ExamResultReferenceDto model);
        Task<ExamResultReferenceDto> Update(int id, ExamResultReferenceDto model);
        Task<bool> Delete(int id);
    }
}