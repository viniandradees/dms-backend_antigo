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
    public class DiseaseDrugPersistence : GeneralPersistence, IDiseaseDrugPersistence
    {
        private readonly DataManagementServiceContext _context;

        public DiseaseDrugPersistence(DataManagementServiceContext context) : base(context)
        {
            _context = context;
        }

        public async Task<DiseaseDrug> GetByIdAsync(int id)
        {
            IQueryable<DiseaseDrug> query = _context.DiseaseDrugs.AsNoTracking()
                .Include(dd => dd.Disease)
                .Include(dd => dd.Drug);

            query = query.Where(dd => dd.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<DiseaseDrug> GetByRelatedIdAsync(int diseaseId, int drugId) 
        {
            IQueryable<DiseaseDrug> query = _context.DiseaseDrugs.AsNoTracking()
                .Include(dd => dd.Disease)
                .Include(dd => dd.Drug);

            query = query.Where(dd => dd.DiseaseId == diseaseId && dd.DrugId == drugId);

            return await query.FirstOrDefaultAsync();
        }
    }
}