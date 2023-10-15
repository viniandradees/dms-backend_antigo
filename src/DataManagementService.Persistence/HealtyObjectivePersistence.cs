using DataManagementService.Domain;
using DataManagementService.Persistence.Contexts;
using DataManagementService.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataManagementService.Persistence
{
    public class HealtyObjectivePersistence : GeneralPersistence, IHealtyObjectivePersistence
    {
        private readonly DataManagementServiceContext _context;

        public HealtyObjectivePersistence(DataManagementServiceContext context) : base(context)
        {
            _context = context;
        }

        public async Task<HealtyObjective[]> GetAllAsync(bool getFullData = false)
        {
            IQueryable<HealtyObjective> query = _context.HealtyObjectives.AsNoTracking();

            query = query.OrderBy(d => d.Name);

            return await query.ToArrayAsync();
        }

        public async Task<HealtyObjective> GetByIdAsync(int id)
        {
            IQueryable<HealtyObjective> query = _context.HealtyObjectives.AsNoTracking();

            query = query.Where(d => d.Id == id);

            return await query.FirstOrDefaultAsync();
        }
        public async Task<HealtyObjective> GetByNameAsync(string name)
        {
            IQueryable<HealtyObjective> query = _context.HealtyObjectives.AsNoTracking();

            query = query.Where(d => d.Name == name);

            return await query.FirstOrDefaultAsync();
        }
    }
}