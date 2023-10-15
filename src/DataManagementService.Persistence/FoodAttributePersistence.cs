using DataManagementService.Domain;
using DataManagementService.Persistence.Contexts;
using DataManagementService.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataManagementService.Persistence
{
    public class FoodAttributePersistence : GeneralPersistence, IFoodAttributePersistence
    {
        private readonly DataManagementServiceContext _context;

        public FoodAttributePersistence(DataManagementServiceContext context) : base(context)
        {
            _context = context;
        }

        public async Task<FoodAttribute[]> GetAllAsync(bool getFullData = false)
        {
            IQueryable<FoodAttribute> query = _context.FoodAttributes.AsNoTracking()
                .Include(fa => fa.RelatedFoods);

            query = query.OrderBy(d => d.Name);

            return await query.ToArrayAsync();
        }

        public async Task<FoodAttribute> GetByIdAsync(int id)
        {
            FoodAttribute foodAttribute = await _context.FoodAttributes.AsNoTracking()
                .Include(fa => fa.RelatedFoods)
                .Where(d => d.Id == id)
                .FirstOrDefaultAsync();

            return foodAttribute;
        }

        public async Task<FoodAttribute> GetByNameAsync(string name)
        {
            FoodAttribute foodAttribute = await _context.FoodAttributes.AsNoTracking()
                .Include(fa => fa.RelatedFoods)
                .Where(d => d.Name == name)
                .FirstOrDefaultAsync();

            return foodAttribute;
        }
    }
}