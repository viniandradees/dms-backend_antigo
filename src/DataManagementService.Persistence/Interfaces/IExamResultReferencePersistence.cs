using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataManagementService.Domain;

namespace DataManagementService.Persistence.Interfaces
{
    public interface IExamResultReferencePersistence : IGeneralPersistence
    {
        Task<ExamResultReference> GetByIdAsync(int id);
        Task<ExamResultReference> GetByRelatedIdAsync(int examId, int measurementUnitId);
    }
}