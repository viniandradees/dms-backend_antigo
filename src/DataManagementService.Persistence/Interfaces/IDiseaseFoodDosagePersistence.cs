using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataManagementService.Domain;

namespace DataManagementService.Persistence.Interfaces
{
    public interface IDiseaseFoodDosagePersistence : IGeneralPersistence
    {  
        Task<DiseaseFoodDosage> GetByIdAsync(int id);
        Task<DiseaseFoodDosage> GetByRelatedIdAsync(int diseaseFoodId, int measurementUnitId);
    }
}