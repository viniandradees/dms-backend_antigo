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
    public class DrugPersistence : GeneralPersistence, IDrugPersistence
    {
        private readonly DataManagementServiceContext _context;

        public DrugPersistence(DataManagementServiceContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Drug[]> GetAllAsync(bool getFullData = false)
        {
            IQueryable<Drug> query = _context.Drugs.AsNoTracking();

            if (getFullData) 
            {
                query = query
                .Include(d => d.TreatableDiseases).ThenInclude(td => td.Disease)
                .Include(d => d.SideEffects).ThenInclude(se => se.Disease);
            }

            query = query.OrderBy(d => d.Name);

            return await query.ToArrayAsync();
        }

        public async Task<Drug> GetByIdAsync(int id)
        {
            IQueryable<Drug> query = _context.Drugs.AsNoTracking()
                .Include(d => d.TreatableDiseases).ThenInclude(td => td.Disease)
                .Include(d => d.SideEffects).ThenInclude(se => se.Disease);

            query = query.Where(d => d.Id == id);

            return await query.FirstOrDefaultAsync();
        }
        public async Task<Drug> GetByNameAsync(string name)
        {
            IQueryable<Drug> query = _context.Drugs.AsNoTracking()
                .Include(d => d.TreatableDiseases).ThenInclude(td => td.Disease)
                .Include(d => d.SideEffects).ThenInclude(se => se.Disease);

            query = query.Where(d => d.Name == name);

            return await query.FirstOrDefaultAsync();
        }
    }
}