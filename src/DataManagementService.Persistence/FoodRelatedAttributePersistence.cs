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
    public class FoodRelatedAttributePersistence : GeneralPersistence, IFoodRelatedAttributePersistence
    {
        private readonly DataManagementServiceContext _context;

        public FoodRelatedAttributePersistence(DataManagementServiceContext context) : base(context)
        {
            _context = context;
        }

        public async Task<FoodRelatedAttribute> GetByIdAsync(int id)
        {
            IQueryable<FoodRelatedAttribute> query = _context.FoodRelatedAttributes.AsNoTracking()
                .Include(dd => dd.Food)
                .Include(dd => dd.FoodAttribute);

            query = query.Where(dd => dd.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<FoodRelatedAttribute> GetByRelatedIdAsync(int foodId, int foodAttributeId) 
        {
            IQueryable<FoodRelatedAttribute> query = _context.FoodRelatedAttributes.AsNoTracking()
                .Include(dd => dd.Food)
                .Include(dd => dd.FoodAttribute);

            query = query.Where(dd => dd.FoodId == foodId && dd.FoodAttributeId == foodAttributeId);

            return await query.FirstOrDefaultAsync();
        }
    }
}