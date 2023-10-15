using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataManagementService.Domain;
using DataManagementService.Persistence.Contexts;
using DataManagementService.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataManagementService.Persistence
{
    public class LifestyleDiseasePersistence : GeneralPersistence, ILifestyleDiseasePersistence
    {
        private readonly DataManagementServiceContext _context;

        public LifestyleDiseasePersistence(DataManagementServiceContext context) : base(context)
        {
            _context = context;
        }

        public async Task<LifestyleDisease> GetByIdAsync(int id)
        {
            IQueryable<LifestyleDisease> query = _context.LifestyleDiseases.AsNoTracking()
                .Include(dd => dd.Lifestyle)
                .Include(dd => dd.Disease);

            query = query.Where(dd => dd.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<LifestyleDisease> GetByRelatedIdAsync(int lifestyleId, int diseaseId) 
        {
            IQueryable<LifestyleDisease> query = _context.LifestyleDiseases.AsNoTracking()
                .Include(dd => dd.Lifestyle)
                .Include(dd => dd.Disease);

            query = query.Where(dd => dd.LifestyleId == lifestyleId && dd.DiseaseId == diseaseId);

            return await query.FirstOrDefaultAsync();
        }
    }
}