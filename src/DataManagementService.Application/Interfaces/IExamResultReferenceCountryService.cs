using DataManagementService.Application.Dtos;

namespace DataManagementService.Application.Interfaces
{
    public interface IExamResultReferenceCountryService
    {
        Task<ExamResultReferenceCountryDto> Add(ExamResultReferenceCountryDto model);
        Task<bool> Delete(int id);
    }
}