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
    public class FoodSupplementPersistence : GeneralPersistence, IFoodSupplementPersistence
    {
        private readonly DataManagementServiceContext _context;

        public FoodSupplementPersistence(DataManagementServiceContext context) : base(context)
        {
            _context = context;
        }

        public async Task<FoodSupplement> GetByIdAsync(int id)
        {
            IQueryable<FoodSupplement> query = _context.FoodSupplements.AsNoTracking()
                .Include(fs => fs.Food)
                .Include(fs => fs.Supplement);

            query = query.Where(fs => fs.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<FoodSupplement> GetByRelatedIdAsync(int foodId, int supplementId) 
        {
            IQueryable<FoodSupplement> query = _context.FoodSupplements.AsNoTracking()
                .Include(fs => fs.Food)
                .Include(fs => fs.Supplement);

            query = query.Where(fs => fs.FoodId == foodId && fs.SupplementId == supplementId);

            return await query.FirstOrDefaultAsync();
        }
    }
}