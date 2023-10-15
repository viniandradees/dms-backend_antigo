using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataManagementService.Domain;
using DataManagementService.Domain.Enum;
using DataManagementService.Persistence.Contexts;
using DataManagementService.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataManagementService.Persistence
{
    public class ExamResultReferenceVariationPersistence : GeneralPersistence, IExamResultReferenceVariationPersistence
    {
        private readonly DataManagementServiceContext _context;

        public ExamResultReferenceVariationPersistence(DataManagementServiceContext context) : base(context) => _context = context;

        public async Task<ExamResultReferenceVariation> GetByIdAsync(int id)
        {
            IQueryable<ExamResultReferenceVariation> query = _context.ExamResultReferenceVariations.AsNoTracking();

            query = query.Where(v => v.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<ExamResultReferenceVariation> GetByRelatedSettingsAsync(int examResultReferenceId, int patientMinimumAge, int patientMaximumAge, Gender gender, bool pregnancyRequired, bool menopauseRequired)
        {
            IQueryable<ExamResultReferenceVariation> query = _context.ExamResultReferenceVariations.AsNoTracking();

            query = query.Where(
                    v => v.ExamResultReferenceId == examResultReferenceId 
                    && v.Gender == gender
                    && v.PatientMinimumAge == patientMinimumAge
                    && v.PatientMaximumAge == patientMaximumAge
                    && v.PregnancyRequired == pregnancyRequired
                    && v.MenopauseRequired == menopauseRequired
                );

            return await query.FirstOrDefaultAsync();
        }
    }
}