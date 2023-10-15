using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataManagementService.Domain;
using DataManagementService.Domain.Enum;
using DataManagementService.Persistence.Contexts;
using DataManagementService.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataManagementService.Persistence
{
    public class MealPeriodPersistence : GeneralPersistence, IMealPeriodPersistence
    {
        private readonly DataManagementServiceContext _context;

        public MealPeriodPersistence(DataManagementServiceContext context) : base(context)
        {
            _context = context;
        }

        public async Task<MealPeriod> GetByIdAsync(int id)
        {
            IQueryable<MealPeriod> query = _context.MealPeriods.AsNoTracking();

            query = query.Where(dd => dd.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<MealPeriod> GetByRelatedIdAsync(int mealId, MealTime mealTime) 
        {
            IQueryable<MealPeriod> query = _context.MealPeriods.AsNoTracking();

            query = query.Where(dd => dd.MealId == mealId && dd.MealTime == mealTime);

            return await query.FirstOrDefaultAsync();
        }
    }
}