using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataManagementService.Domain;
using DataManagementService.Domain.Enum;

namespace DataManagementService.Persistence.Interfaces
{
    public interface IExamSupplementDosageAgeRangePersistence : IGeneralPersistence
    {  
        Task<ExamSupplementDosageAgeRange> GetByIdAsync(int id);
        Task<ExamSupplementDosageAgeRange> GetByRelatedDataAsync(int examSupplementDosageId, AgeTimeUnit ageTimeUnit, int minimumAge, int maximumAge);
    }
}