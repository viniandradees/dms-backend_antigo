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
    public class MealDietaryOptionPersistence : GeneralPersistence, IMealDietaryOptionPersistence
    {
        private readonly DataManagementServiceContext _context;

        public MealDietaryOptionPersistence(DataManagementServiceContext context) : base(context)
        {
            _context = context;
        }

        public async Task<MealDietaryOption> GetByIdAsync(int id)
        {
            IQueryable<MealDietaryOption> query = _context.MealDietaryOptions.AsNoTracking()
                .Include(dd => dd.Meal)
                .Include(dd => dd.DietaryOption);

            query = query.Where(dd => dd.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<MealDietaryOption> GetByRelatedIdAsync(int mealId, int dietaryOptionId) 
        {
            IQueryable<MealDietaryOption> query = _context.MealDietaryOptions.AsNoTracking()
                .Include(dd => dd.Meal)
                .Include(dd => dd.DietaryOption);

            query = query.Where(dd => dd.MealId == mealId && dd.DietaryOptionId == dietaryOptionId);

            return await query.FirstOrDefaultAsync();
        }
    }
}