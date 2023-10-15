using DataManagementService.Domain;
using DataManagementService.Persistence.Contexts;
using DataManagementService.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataManagementService.Persistence
{
    public class DietaryOptionPersistence : GeneralPersistence, IDietaryOptionPersistence
    {
        private readonly DataManagementServiceContext _context;

        public DietaryOptionPersistence(DataManagementServiceContext context) : base(context)
        {
            _context = context;
        }

        public async Task<DietaryOption[]> GetAllAsync(bool getFullData = false)
        {
            IQueryable<DietaryOption> query = _context.DietaryOptions.AsNoTracking();

            if (getFullData)
            {
                query = query
                .Include(d => d.Incompatibilities).ThenInclude(i => i.FoodAttribute)
                .Include(d => d.MealDietaryOptions).ThenInclude(mdo => mdo.Meal);
            }

            query = query.OrderBy(d => d.Name);

            return await query.ToArrayAsync();
        }

        public async Task<DietaryOption> GetByIdAsync(int id)
        {
            DietaryOption dietaryOption = await _context.DietaryOptions.AsNoTracking()
                .Include(d => d.Incompatibilities).ThenInclude(i => i.FoodAttribute)
                .Include(d => d.MealDietaryOptions).ThenInclude(mdo => mdo.Meal)
                .Where(d => d.Id == id)
                .FirstOrDefaultAsync();

            if (dietaryOption != null)
            {
                dietaryOption.MealDietaryOptions = dietaryOption.MealDietaryOptions.OrderBy(mdo => mdo.Meal.Name).ToList();
                dietaryOption.Incompatibilities = dietaryOption.Incompatibilities.OrderBy(ts => ts.FoodAttribute.Name).ToList();
            }

            return dietaryOption;
        }

        public async Task<DietaryOption> GetByNameAsync(string name)
        {
            DietaryOption dietaryOption = await _context.DietaryOptions.AsNoTracking()
                .Include(d => d.Incompatibilities).ThenInclude(i => i.FoodAttribute)
                .Include(d => d.MealDietaryOptions)
                .Where(d => d.Name == name)
                .FirstOrDefaultAsync();

            if (dietaryOption != null)
            {
                dietaryOption.MealDietaryOptions = dietaryOption.MealDietaryOptions.OrderBy(mdo => mdo.Meal.Name).ToList();
                dietaryOption.Incompatibilities = dietaryOption.Incompatibilities.OrderBy(ts => ts.FoodAttribute.Name).ToList();
            }

            return dietaryOption;
        }
    }
}