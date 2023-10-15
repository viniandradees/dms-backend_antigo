using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataManagementService.Domain;

namespace DataManagementService.Persistence.Interfaces
{
    public interface IDiseaseLifestylePersistence : IGeneralPersistence
    {  
        Task<DiseaseLifestyle> GetByIdAsync(int id);
        Task<DiseaseLifestyle> GetByRelatedIdAsync(int diseaseId, int lifestyleId);
    }
}