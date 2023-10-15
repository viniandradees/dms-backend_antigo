using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataManagementService.Domain;

namespace DataManagementService.Persistence.Interfaces
{
    public interface IDiseaseSupplementDosagePersistence : IGeneralPersistence
    {  
        Task<DiseaseSupplementDosage> GetByIdAsync(int id);
        Task<DiseaseSupplementDosage> GetByRelatedIdAsync(int diseaseSupplementId, int measurementUnitId);
    }
}