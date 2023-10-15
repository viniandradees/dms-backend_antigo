using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataManagementService.Domain;

namespace DataManagementService.Persistence.Interfaces
{
    public interface IDiseaseDiseasePersistence : IGeneralPersistence
    {  
        Task<DiseaseDisease> GetByIdAsync(int id);
        Task<DiseaseDisease> GetByRelatedIdAsync(int diseaseId, int symptomId);
    }
}