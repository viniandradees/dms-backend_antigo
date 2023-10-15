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
    public class ExamResultReferenceCountryPersistence : GeneralPersistence, IExamResultReferenceCountryPersistence
    {
        private readonly DataManagementServiceContext _context;

        public ExamResultReferenceCountryPersistence(DataManagementServiceContext context) : base(context) => _context = context;

        public async Task<ExamResultReferenceCountry> GetByIdAsync(int id)
        {
            IQueryable<ExamResultReferenceCountry> query = _context.ExamResultReferenceCountries.AsNoTracking();

            query = query.Where(v => v.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<ExamResultReferenceCountry> GetByRelatedSettingsAsync(int examResultReferenceId, int countryId)
        {
            IQueryable<ExamResultReferenceCountry> query = _context.ExamResultReferenceCountries.AsNoTracking();

            query = query.Where(
                    v => v.ExamResultReferenceId == examResultReferenceId 
                    && v.CountryId == countryId
                );

            return await query.FirstOrDefaultAsync();
        }
    }
}