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
    public class MealFoodPersistence : GeneralPersistence, IMealFoodPersistence
    {
        private readonly DataManagementServiceContext _context;

        public MealFoodPersistence(DataManagementServiceContext context) : base(context)
        {
            _context = context;
        }

        public async Task<MealFood> GetByIdAsync(int id)
        {
            IQueryable<MealFood> query = _context.MealFoods.AsNoTracking()
                .Include(dd => dd.Meal)
                .Include(dd => dd.Food);

            query = query.Where(dd => dd.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<MealFood> GetByRelatedIdAsync(int mealId, int foodId) 
        {
            IQueryable<MealFood> query = _context.MealFoods.AsNoTracking()
                .Include(dd => dd.Meal)
                .Include(dd => dd.Food);

            query = query.Where(dd => dd.MealId == mealId && dd.FoodId == foodId);

            return await query.FirstOrDefaultAsync();
        }
    }
}