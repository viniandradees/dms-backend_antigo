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
    public class FoodPersistence : GeneralPersistence, IFoodPersistence
    {
        private readonly DataManagementServiceContext _context;

        public FoodPersistence(DataManagementServiceContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Food[]> GetAllAsync(bool getFullData = false)
        {
            IQueryable<Food> query = _context.Foods.AsNoTracking();

            if (getFullData)
            {
                query = query
                .Include(f => f.SideEffects).ThenInclude(se => se.Disease)
                .Include(f => f.Attributes).ThenInclude(a => a.FoodAttribute)
                .Include(f => f.RelatedHealtyObjectives).ThenInclude(rho => rho.HealtyObjective)
                .Include(f => f.Nutrients).ThenInclude(n => n.Supplement)
                .Include(f => f.Nutrients).ThenInclude(n => n.MeasurementUnit);
            }

            query = query.OrderBy(f => f.Name);

            return await query.ToArrayAsync();
        }

        public async Task<Food> GetByIdAsync(int id)
        {
            Food food = await _context.Foods.AsNoTracking()
                .Include(f => f.SideEffects).ThenInclude(se => se.Disease)
                .Include(f => f.Attributes).ThenInclude(a => a.FoodAttribute)
                .Include(f => f.RelatedHealtyObjectives).ThenInclude(rho => rho.HealtyObjective)
                .Include(f => f.Nutrients).ThenInclude(n => n.Supplement)
                .Include(f => f.Nutrients).ThenInclude(n => n.MeasurementUnit)
                .Where(f => f.Id == id)
                .FirstOrDefaultAsync();

            if (food != null)
            {
                food.SideEffects = food.SideEffects.OrderBy(td => td.Disease.Name).ToList();
                food.Attributes = food.Attributes.OrderBy(td => td.FoodAttribute.Name).ToList();
                food.Nutrients = food.Nutrients.OrderBy(td => td.Supplement.Name).ToList();
            }

            return food;
        }
        public async Task<Food> GetByNameAsync(string name)
        {
            Food food = await _context.Foods.AsNoTracking()
                .Include(f => f.SideEffects).ThenInclude(se => se.Disease)
                .Include(f => f.Attributes).ThenInclude(a => a.FoodAttribute)
                .Include(f => f.RelatedHealtyObjectives).ThenInclude(rho => rho.HealtyObjective)
                .Include(f => f.Nutrients).ThenInclude(n => n.Supplement)
                .Include(f => f.Nutrients).ThenInclude(n => n.MeasurementUnit)
                .Where(f => f.Name == name)
                .FirstOrDefaultAsync();

            if (food != null)
            {
                food.SideEffects = food.SideEffects.OrderBy(td => td.Disease.Name).ToList();
                food.Attributes = food.Attributes.OrderBy(td => td.FoodAttribute.Name).ToList();
                food.Nutrients = food.Nutrients.OrderBy(td => td.Supplement.Name).ToList();
            }

            return food;
        }
    }
}