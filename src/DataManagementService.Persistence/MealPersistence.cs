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
    public class MealPersistence : GeneralPersistence, IMealPersistence
    {
        private readonly DataManagementServiceContext _context;

        public MealPersistence(DataManagementServiceContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Meal[]> GetAllAsync(bool getFullData = false)
        {
            IQueryable<Meal> query = _context.Meals.AsNoTracking();

            if (getFullData)
            {
                query = query
                .Include(m => m.Ingredients).ThenInclude(i => i.Food).ThenInclude(f => f.Attributes)
                .Include(m => m.Ingredients).ThenInclude(i => i.MeasurementUnit)
                .Include(m => m.MealPeriods)
                .Include(m => m.InternationalCuisines).ThenInclude(ic => ic.Country)
                .Include(m => m.MealDietaryOptions).ThenInclude(mdo => mdo.DietaryOption).ThenInclude(diopt => diopt.Incompatibilities);
            }
            
            query = query.OrderBy(d => d.Name);

            return await query.ToArrayAsync();
        }

        public async Task<Meal> GetByIdAsync(int id)
        {
            Meal meal = await _context.Meals.AsNoTracking()
                .Include(m => m.Ingredients).ThenInclude(i => i.Food).ThenInclude(f => f.Attributes)
                .Include(m => m.Ingredients).ThenInclude(i => i.MeasurementUnit)
                .Include(m => m.MealPeriods)
                .Include(m => m.InternationalCuisines).ThenInclude(ic => ic.Country)
                .Include(m => m.MealDietaryOptions).ThenInclude(mdo => mdo.DietaryOption).ThenInclude(diopt => diopt.Incompatibilities)
                .Where(d => d.Id == id)
                .FirstOrDefaultAsync();

            if (meal != null)
            {
                meal.Ingredients = meal.Ingredients.OrderBy(i => i.Food.Name).ToList();
                meal.MealPeriods = meal.MealPeriods.OrderBy(mp => mp.MealTime).ToList();
                meal.InternationalCuisines = meal.InternationalCuisines.OrderBy(ic => ic.Country.Name).ToList();
                meal.MealDietaryOptions = meal.MealDietaryOptions.OrderBy(diopt => diopt.DietaryOption.Name).ToList();
            }

            return meal;
        }
        public async Task<Meal> GetByNameAsync(string name)
        {
            Meal meal = await _context.Meals.AsNoTracking()
                .Include(m => m.Ingredients).ThenInclude(i => i.Food).ThenInclude(f => f.Attributes)
                .Include(m => m.Ingredients).ThenInclude(i => i.MeasurementUnit)
                .Include(m => m.MealPeriods)
                .Include(m => m.InternationalCuisines).ThenInclude(ic => ic.Country)
                .Include(m => m.MealDietaryOptions).ThenInclude(mdo => mdo.DietaryOption).ThenInclude(diopt => diopt.Incompatibilities)
                .Where(d => d.Name == name)
                .FirstOrDefaultAsync();

            if (meal != null)
            {
                meal.Ingredients = meal.Ingredients.OrderBy(i => i.Food.Name).ToList();
                meal.MealPeriods = meal.MealPeriods.OrderBy(mp => mp.MealTime).ToList();
                meal.InternationalCuisines = meal.InternationalCuisines.OrderBy(ic => ic.Country.Name).ToList();
                meal.MealDietaryOptions = meal.MealDietaryOptions.OrderBy(diopt => diopt.DietaryOption.Name).ToList();
            }

            return meal;
        }
    }
}