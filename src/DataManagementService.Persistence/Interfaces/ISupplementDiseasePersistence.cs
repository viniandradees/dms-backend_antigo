using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataManagementService.Domain;

namespace DataManagementService.Persistence.Interfaces
{
    public interface ISupplementDiseasePersistence : IGeneralPersistence
    {  
        Task<SupplementDisease> GetByIdAsync(int id);
        Task<SupplementDisease> GetByRelatedIdAsync(int supplementId, int diseaseId);
    }
}