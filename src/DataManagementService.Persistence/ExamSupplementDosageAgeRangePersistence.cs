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
    public class ExamSupplementDosageAgeRangePersistence : GeneralPersistence, IExamSupplementDosageAgeRangePersistence
    {
        private readonly DataManagementServiceContext _context;

        public ExamSupplementDosageAgeRangePersistence(DataManagementServiceContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ExamSupplementDosageAgeRange> GetByIdAsync(int id)
        {
            IQueryable<ExamSupplementDosageAgeRange> query = _context.ExamSupplementDosageAgeRanges.AsNoTracking();

            query = query.Where(dddar => dddar.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<ExamSupplementDosageAgeRange> GetByRelatedDataAsync(int examSupplementDosageId, AgeTimeUnit ageTimeUnit, int minimumAge, int maximumAge) 
        {
            IQueryable<ExamSupplementDosageAgeRange> query = _context.ExamSupplementDosageAgeRanges.AsNoTracking();

            query = query.Where(
                dddar => 
                dddar.ExamSupplementDosageId == examSupplementDosageId && 
                dddar.AgeTimeUnit == ageTimeUnit && 
                dddar.MinimumAge == minimumAge && 
                dddar.MaximumAge == maximumAge
            );

            return await query.FirstOrDefaultAsync();
        }
    }
}