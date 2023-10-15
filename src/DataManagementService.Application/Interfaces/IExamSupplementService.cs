using DataManagementService.Application.Dtos;

namespace DataManagementService.Application.Interfaces
{
    public interface IExamSupplementService
    {
        Task<ExamSupplementDto> Add(ExamSupplementDto model);
        Task<ExamSupplementDto> Update(int id, ExamSupplementDto model);
        Task<bool> Delete(int id);
    }
}