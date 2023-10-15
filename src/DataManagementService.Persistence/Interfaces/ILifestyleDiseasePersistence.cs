using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataManagementService.Domain;

namespace DataManagementService.Persistence.Interfaces
{
    public interface ILifestyleDiseasePersistence : IGeneralPersistence
    {  
        Task<LifestyleDisease> GetByIdAsync(int id);
        Task<LifestyleDisease> GetByRelatedIdAsync(int lifestyleId, int diseaseId);
    }
}