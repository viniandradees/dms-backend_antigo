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
    public class DiseaseLifestylePersistence : GeneralPersistence, IDiseaseLifestylePersistence
    {
        private readonly DataManagementServiceContext _context;

        public DiseaseLifestylePersistence(DataManagementServiceContext context) : base(context)
        {
            _context = context;
        }

        public async Task<DiseaseLifestyle> GetByIdAsync(int id)
        {
            IQueryable<DiseaseLifestyle> query = _context.DiseaseLifestyles.AsNoTracking()
                .Include(dl => dl.Disease)
                .Include(dl => dl.Lifestyle);

            query = query.Where(dl => dl.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<DiseaseLifestyle> GetByRelatedIdAsync(int diseaseId, int lifestyleId) 
        {
            IQueryable<DiseaseLifestyle> query = _context.DiseaseLifestyles.AsNoTracking()
                .Include(dl => dl.Disease)
                .Include(dl => dl.Lifestyle);

            query = query.Where(dl => dl.DiseaseId == diseaseId && dl.LifestyleId == lifestyleId);

            return await query.FirstOrDefaultAsync();
        }
    }
}