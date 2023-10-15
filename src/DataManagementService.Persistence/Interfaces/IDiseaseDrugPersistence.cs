using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataManagementService.Domain;

namespace DataManagementService.Persistence.Interfaces
{
    public interface IDiseaseDrugPersistence : IGeneralPersistence
    {  
        Task<DiseaseDrug> GetByIdAsync(int id);
        Task<DiseaseDrug> GetByRelatedIdAsync(int diseaseId, int drugId);
    }
}