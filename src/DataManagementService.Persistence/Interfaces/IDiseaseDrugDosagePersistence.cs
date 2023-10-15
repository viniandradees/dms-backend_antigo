using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataManagementService.Domain;

namespace DataManagementService.Persistence.Interfaces
{
    public interface IDiseaseDrugDosagePersistence : IGeneralPersistence
    {  
        Task<DiseaseDrugDosage> GetByIdAsync(int id);
        Task<DiseaseDrugDosage> GetByRelatedIdAsync(int diseaseDrugId, int measurementUnitId);
    }
}