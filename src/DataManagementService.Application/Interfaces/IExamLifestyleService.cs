using DataManagementService.Application.Dtos;

namespace DataManagementService.Application.Interfaces
{
    public interface IExamLifestyleService
    {
        Task<ExamLifestyleDto> Add(ExamLifestyleDto model);
        Task<bool> Delete(int id);
    }
}