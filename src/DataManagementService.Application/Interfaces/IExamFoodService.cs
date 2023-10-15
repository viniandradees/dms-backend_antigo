using DataManagementService.Application.Dtos;

namespace DataManagementService.Application.Interfaces
{
    public interface IExamFoodService
    {
        Task<ExamFoodDto> Add(ExamFoodDto model);
        Task<ExamFoodDto> Update(int id, ExamFoodDto model);
        Task<bool> Delete(int id);
    }
}