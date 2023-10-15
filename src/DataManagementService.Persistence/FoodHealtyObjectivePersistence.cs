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
    public class FoodHealtyObjectivePersistence : GeneralPersistence, IFoodHealtyObjectivePersistence
    {
        private readonly DataManagementServiceContext _context;

        public FoodHealtyObjectivePersistence(DataManagementServiceContext context) : base(context)
        {
            _context = context;
        }

        public async Task<FoodHealtyObjective> GetByIdAsync(int id)
        {
            IQueryable<FoodHealtyObjective> query = _context.FoodHealtyObjectives.AsNoTracking()
                .Include(dd => dd.Food)
                .Include(dd => dd.HealtyObjective);

            query = query.Where(dd => dd.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<FoodHealtyObjective> GetByRelatedIdAsync(int foodId, int healtyObjectiveId) 
        {
            IQueryable<FoodHealtyObjective> query = _context.FoodHealtyObjectives.AsNoTracking()
                .Include(dd => dd.Food)
                .Include(dd => dd.HealtyObjective);

            query = query.Where(dd => dd.FoodId == foodId && dd.HealtyObjectiveId == healtyObjectiveId);

            return await query.FirstOrDefaultAsync();
        }
    }
}