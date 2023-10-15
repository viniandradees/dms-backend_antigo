using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataManagementService.Domain;

namespace DataManagementService.Persistence.Interfaces
{
    public interface IExamFoodPersistence : IGeneralPersistence
    {  
        Task<ExamFood> GetByIdAsync(int id);
        Task<ExamFood> GetByRelatedIdAsync(int examId, int foodId);
    }
}