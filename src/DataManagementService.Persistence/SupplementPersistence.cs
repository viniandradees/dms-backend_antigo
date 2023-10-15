using DataManagementService.Domain;
using DataManagementService.Persistence.Contexts;
using DataManagementService.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataManagementService.Persistence
{
    public class SupplementPersistence : GeneralPersistence, ISupplementPersistence
    {
        private readonly DataManagementServiceContext _context;

        public SupplementPersistence(DataManagementServiceContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Supplement[]> GetAllAsync(bool getFullData = false)
        {
            IQueryable<Supplement> query = _context.Supplements.AsNoTracking();

            if (getFullData) 
            {
                query = query
                .Include(s => s.TreatableDiseases).ThenInclude(td => td.Disease)
                .Include(s => s.SideEffects).ThenInclude(se => se.Disease);
            }

            query = query.OrderBy(d => d.Name);

            return await query.ToArrayAsync();
        }

        public async Task<Supplement> GetByIdAsync(int id)
        {
            IQueryable<Supplement> query = _context.Supplements.AsNoTracking()
                .Include(s => s.TreatableDiseases).ThenInclude(td => td.Disease)
                .Include(s => s.SideEffects).ThenInclude(se => se.Disease);

            query = query.Where(s => s.Id == id);

            return await query.FirstOrDefaultAsync();
        }
        public async Task<Supplement> GetByNameAsync(string name)
        {
            IQueryable<Supplement> query = _context.Supplements.AsNoTracking()
                .Include(s => s.TreatableDiseases).ThenInclude(td => td.Disease)
                .Include(s => s.SideEffects).ThenInclude(se => se.Disease);

            query = query.Where(s => s.Name == name);

            return await query.FirstOrDefaultAsync();
        }
    }
}