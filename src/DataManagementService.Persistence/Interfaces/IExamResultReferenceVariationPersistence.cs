using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataManagementService.Domain;
using DataManagementService.Domain.Enum;

namespace DataManagementService.Persistence.Interfaces
{
    public interface IExamResultReferenceVariationPersistence : IGeneralPersistence
    {
        Task<ExamResultReferenceVariation> GetByIdAsync(int id);
        Task<ExamResultReferenceVariation> GetByRelatedSettingsAsync(int examResultReferenceId, int patientMinimumAge, int patientMaximumAge, Gender gender, bool pregnancyRequired, bool menopauseRequired);
    }
}