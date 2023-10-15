using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataManagementService.Domain;

namespace DataManagementService.Persistence.Interfaces
{
    public interface IDiseaseSupplementPersistence : IGeneralPersistence
    {  
        Task<DiseaseSupplement> GetByIdAsync(int id);
        Task<DiseaseSupplement> GetByRelatedIdAsync(int diseaseId, int supplementId);
    }
}