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
    public class DiseaseDrugDosageAgeRangePersistence : GeneralPersistence, IDiseaseDrugDosageAgeRangePersistence
    {
        private readonly DataManagementServiceContext _context;

        public DiseaseDrugDosageAgeRangePersistence(DataManagementServiceContext context) : base(context)
        {
            _context = context;
        }

        public async Task<DiseaseDrugDosageAgeRange> GetByIdAsync(int id)
        {
            IQueryable<DiseaseDrugDosageAgeRange> query = _context.DiseaseDrugDosageAgeRanges.AsNoTracking();

            query = query.Where(dddar => dddar.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<DiseaseDrugDosageAgeRange> GetByRelatedDataAsync(int diseaseDrugDosageId, AgeTimeUnit ageTimeUnit, int minimumAge, int maximumAge) 
        {
            IQueryable<DiseaseDrugDosageAgeRange> query = _context.DiseaseDrugDosageAgeRanges.AsNoTracking();

            query = query.Where(
                dddar => 
                dddar.DiseaseDrugDosageId == diseaseDrugDosageId && 
                dddar.AgeTimeUnit == ageTimeUnit && 
                dddar.MinimumAge == minimumAge && 
                dddar.MaximumAge == maximumAge
            );

            return await query.FirstOrDefaultAsync();
        }
    }
}