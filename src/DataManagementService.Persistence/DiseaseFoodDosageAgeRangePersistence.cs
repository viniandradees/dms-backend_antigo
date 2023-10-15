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
    public class DiseaseFoodDosageAgeRangePersistence : GeneralPersistence, IDiseaseFoodDosageAgeRangePersistence
    {
        private readonly DataManagementServiceContext _context;

        public DiseaseFoodDosageAgeRangePersistence(DataManagementServiceContext context) : base(context)
        {
            _context = context;
        }

        public async Task<DiseaseFoodDosageAgeRange> GetByIdAsync(int id)
        {
            IQueryable<DiseaseFoodDosageAgeRange> query = _context.DiseaseFoodDosageAgeRanges.AsNoTracking();

            query = query.Where(dddar => dddar.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<DiseaseFoodDosageAgeRange> GetByRelatedDataAsync(int diseaseFoodDosageId, AgeTimeUnit ageTimeUnit, int minimumAge, int maximumAge) 
        {
            IQueryable<DiseaseFoodDosageAgeRange> query = _context.DiseaseFoodDosageAgeRanges.AsNoTracking();

            query = query.Where(
                dddar => 
                dddar.DiseaseFoodDosageId == diseaseFoodDosageId && 
                dddar.AgeTimeUnit == ageTimeUnit && 
                dddar.MinimumAge == minimumAge && 
                dddar.MaximumAge == maximumAge
            );

            return await query.FirstOrDefaultAsync();
        }
    }
}