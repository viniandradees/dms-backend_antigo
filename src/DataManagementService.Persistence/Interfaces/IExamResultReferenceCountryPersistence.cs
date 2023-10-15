using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataManagementService.Domain;
using DataManagementService.Domain.Enum;

namespace DataManagementService.Persistence.Interfaces
{
    public interface IExamResultReferenceCountryPersistence : IGeneralPersistence
    {
        Task<ExamResultReferenceCountry> GetByIdAsync(int id);
        Task<ExamResultReferenceCountry> GetByRelatedSettingsAsync(int examResultReferenceId, int countryId);
    }
}