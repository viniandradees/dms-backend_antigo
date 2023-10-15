using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataManagementService.Domain;

namespace DataManagementService.Persistence.Interfaces
{
    public interface IDiseaseFoodPersistence : IGeneralPersistence
    {  
        Task<DiseaseFood> GetByIdAsync(int id);
        Task<DiseaseFood> GetByRelatedIdAsync(int diseaseId, int drugId);
    }
}