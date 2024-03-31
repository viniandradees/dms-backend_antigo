using DataManagementService.Domain;
using DataManagementService.Persistence.Contexts;
using DataManagementService.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataManagementService.Persistence
{
    public class CountryPersistence : GeneralPersistence, ICountryPersistence
    {
        private readonly DataManagementServiceContext _context;

        public CountryPersistence(DataManagementServiceContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Country[]> GetAllAsync()
        {
            IQueryable<Country> query = _context.Countries.AsNoTracking();

            query = query.OrderBy(d => d.Name);

            return await query.ToArrayAsync();
        }

        public async Task<Country> GetByIdAsync(int id)
        {
            IQueryable<Country> query = _context.Countries.AsNoTracking();

            query = query.Where(d => d.Id == id);

            return await query.FirstOrDefaultAsync();
        }
        public async Task<Country> GetByNameAsync(string name)
        {
            IQueryable<Country> query = _context.Countries.AsNoTracking();

            query = query.Where(d => d.Name == name);

            return await query.FirstOrDefaultAsync();
        }
    }
}