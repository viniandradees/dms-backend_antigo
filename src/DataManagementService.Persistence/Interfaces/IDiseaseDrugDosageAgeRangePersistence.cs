using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataManagementService.Domain;
using DataManagementService.Domain.Enum;

namespace DataManagementService.Persistence.Interfaces
{
    public interface IDiseaseDrugDosageAgeRangePersistence : IGeneralPersistence
    {  
        Task<DiseaseDrugDosageAgeRange> GetByIdAsync(int id);
        Task<DiseaseDrugDosageAgeRange> GetByRelatedDataAsync(int diseaseDrugDosageId, AgeTimeUnit ageTimeUnit, int minimumAge, int maximumAge);
    }
}