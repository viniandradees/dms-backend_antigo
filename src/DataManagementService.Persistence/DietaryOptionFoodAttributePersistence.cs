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
    public class DietaryOptionFoodAttributePersistence : GeneralPersistence, IDietaryOptionFoodAttributePersistence
    {
        private readonly DataManagementServiceContext _context;

        public DietaryOptionFoodAttributePersistence(DataManagementServiceContext context) : base(context)
        {
            _context = context;
        }

        public async Task<DietaryOptionFoodAttribute> GetByIdAsync(int id)
        {
            IQueryable<DietaryOptionFoodAttribute> query = _context.DietaryOptionFoodAttributes.AsNoTracking()
                .Include(dofa => dofa.DietaryOption)
                .Include(dofa => dofa.FoodAttribute);

            query = query.Where(dofa => dofa.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<DietaryOptionFoodAttribute> GetByRelatedIdAsync(int dietaryOptionId, int foodAttributeId) 
        {
            IQueryable<DietaryOptionFoodAttribute> query = _context.DietaryOptionFoodAttributes.AsNoTracking()
                .Include(dofa => dofa.DietaryOption)
                .Include(dofa => dofa.FoodAttribute);

            query = query.Where(dofa => dofa.DietaryOptionId == dietaryOptionId && dofa.FoodAttributeId == foodAttributeId);

            return await query.FirstOrDefaultAsync();
        }
    }
}