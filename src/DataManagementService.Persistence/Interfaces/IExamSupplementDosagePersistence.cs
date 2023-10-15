using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataManagementService.Domain;

namespace DataManagementService.Persistence.Interfaces
{
    public interface IExamSupplementDosagePersistence : IGeneralPersistence
    {  
        Task<ExamSupplementDosage> GetByIdAsync(int id);
        Task<ExamSupplementDosage> GetByRelatedIdAsync(int examSupplementId, int measurementUnitId);
    }
}