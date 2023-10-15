using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataManagementService.Domain;

namespace DataManagementService.Persistence.Interfaces
{
    public interface IDiseaseExamPersistence : IGeneralPersistence
    {  
        Task<DiseaseExam> GetByIdAsync(int id);
        Task<DiseaseExam> GetByRelatedIdAsync(int diseaseId, int examId);
    }
}