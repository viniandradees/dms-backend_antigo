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
    public class DiseaseSupplementDosageAgeRangePersistence : GeneralPersistence, IDiseaseSupplementDosageAgeRangePersistence
    {
        private readonly DataManagementServiceContext _context;

        public DiseaseSupplementDosageAgeRangePersistence(DataManagementServiceContext context) : base(context)
        {
            _context = context;
        }

        public async Task<DiseaseSupplementDosageAgeRange> GetByIdAsync(int id)
        {
            IQueryable<DiseaseSupplementDosageAgeRange> query = _context.DiseaseSupplementDosageAgeRanges.AsNoTracking();

            query = query.Where(dddar => dddar.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<DiseaseSupplementDosageAgeRange> GetByRelatedDataAsync(int diseaseSupplementDosageId, AgeTimeUnit ageTimeUnit, int minimumAge, int maximumAge) 
        {
            IQueryable<DiseaseSupplementDosageAgeRange> query = _context.DiseaseSupplementDosageAgeRanges.AsNoTracking();

            query = query.Where(
                dddar => 
                dddar.DiseaseSupplementDosageId == diseaseSupplementDosageId && 
                dddar.AgeTimeUnit == ageTimeUnit && 
                dddar.MinimumAge == minimumAge && 
                dddar.MaximumAge == maximumAge
            );

            return await query.FirstOrDefaultAsync();
        }
    }
}