using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataManagementService.Domain;
using DataManagementService.Domain.Enum;

namespace DataManagementService.Persistence.Interfaces
{
    public interface IDiseaseFoodDosageAgeRangePersistence : IGeneralPersistence
    {  
        Task<DiseaseFoodDosageAgeRange> GetByIdAsync(int id);
        Task<DiseaseFoodDosageAgeRange> GetByRelatedDataAsync(int diseaseFoodDosageId, AgeTimeUnit ageTimeUnit, int minimumAge, int maximumAge);
    }
}