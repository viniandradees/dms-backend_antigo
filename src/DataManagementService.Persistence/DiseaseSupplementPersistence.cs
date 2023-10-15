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
    public class DiseaseSupplementPersistence : GeneralPersistence, IDiseaseSupplementPersistence
    {
        private readonly DataManagementServiceContext _context;

        public DiseaseSupplementPersistence(DataManagementServiceContext context) : base(context)
        {
            _context = context;
        }

        public async Task<DiseaseSupplement> GetByIdAsync(int id)
        {
            IQueryable<DiseaseSupplement> query = _context.DiseaseSupplements.AsNoTracking()
                .Include(dd => dd.Disease)
                .Include(dd => dd.Supplement);

            query = query.Where(dd => dd.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<DiseaseSupplement> GetByRelatedIdAsync(int diseaseId, int drugId) 
        {
            IQueryable<DiseaseSupplement> query = _context.DiseaseSupplements.AsNoTracking()
                .Include(dd => dd.Disease)
                .Include(dd => dd.Supplement);

            query = query.Where(dd => dd.DiseaseId == diseaseId && dd.SupplementId == drugId);

            return await query.FirstOrDefaultAsync();
        }
    }
}