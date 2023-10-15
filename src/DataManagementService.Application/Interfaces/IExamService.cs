using DataManagementService.Application.Dtos;

namespace DataManagementService.Application.Interfaces
{
    public interface IExamService
    {
        Task<ExamDto> Add(ExamDto model);
        Task<ExamDto> Update(int id, ExamDto model);
        Task<bool> Delete(int id);

        Task<ExamDto[]> GetAllAsync(bool getFullData = false);
        Task<ExamDto> GetByIdAsync(int id);
        Task<ExamDto> GetByNameAsync(string name);
    }
}