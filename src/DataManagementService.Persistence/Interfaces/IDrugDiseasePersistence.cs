using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataManagementService.Domain;

namespace DataManagementService.Persistence.Interfaces
{
    public interface IDrugDiseasePersistence : IGeneralPersistence
    {  
        Task<DrugDisease> GetByIdAsync(int id);
        Task<DrugDisease> GetByRelatedIdAsync(int drugId, int diseaseId);
    }
}