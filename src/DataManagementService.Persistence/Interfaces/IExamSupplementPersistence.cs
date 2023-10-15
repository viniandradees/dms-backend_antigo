using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataManagementService.Domain;

namespace DataManagementService.Persistence.Interfaces
{
    public interface IExamSupplementPersistence : IGeneralPersistence
    {  
        Task<ExamSupplement> GetByIdAsync(int id);
        Task<ExamSupplement> GetByRelatedIdAsync(int examId, int supplementId);
    }
}