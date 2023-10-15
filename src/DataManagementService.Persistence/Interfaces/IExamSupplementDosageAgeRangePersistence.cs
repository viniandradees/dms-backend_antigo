using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataManagementService.Domain;
using DataManagementService.Domain.Enum;

namespace DataManagementService.Persistence.Interfaces
{
    public interface IDiseaseSupplementDosageAgeRangePersistence : IGeneralPersistence
    {  
        Task<DiseaseSupplementDosageAgeRange> GetByIdAsync(int id);
        Task<DiseaseSupplementDosageAgeRange> GetByRelatedDataAsync(int diseaseSupplementDosageId, AgeTimeUnit ageTimeUnit, int minimumAge, int maximumAge);
    }
}