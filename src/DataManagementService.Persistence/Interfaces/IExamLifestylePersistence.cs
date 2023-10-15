using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataManagementService.Domain;

namespace DataManagementService.Persistence.Interfaces
{
    public interface IExamLifestylePersistence : IGeneralPersistence
    {  
        Task<ExamLifestyle> GetByIdAsync(int id);
        Task<ExamLifestyle> GetByRelatedIdAsync(int examId, int lifestyleId);
    }
}