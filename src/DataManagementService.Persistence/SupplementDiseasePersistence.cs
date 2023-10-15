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
    public class SupplementDiseasePersistence : GeneralPersistence, ISupplementDiseasePersistence
    {
        private readonly DataManagementServiceContext _context;

        public SupplementDiseasePersistence(DataManagementServiceContext context) : base(context)
        {
            _context = context;
        }

        public async Task<SupplementDisease> GetByIdAsync(int id)
        {
            IQueryable<SupplementDisease> query = _context.SupplementDiseases.AsNoTracking()
                .Include(dd => dd.Supplement)
                .Include(dd => dd.Disease);

            query = query.Where(dd => dd.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<SupplementDisease> GetByRelatedIdAsync(int supplementId, int diseaseId) 
        {
            IQueryable<SupplementDisease> query = _context.SupplementDiseases.AsNoTracking()
                .Include(dd => dd.Supplement)
                .Include(dd => dd.Disease);

            query = query.Where(dd => dd.SupplementId == supplementId && dd.DiseaseId == diseaseId);

            return await query.FirstOrDefaultAsync();
        }
    }
}