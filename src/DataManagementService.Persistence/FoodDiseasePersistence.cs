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
    public class FoodDiseasePersistence : GeneralPersistence, IFoodDiseasePersistence
    {
        private readonly DataManagementServiceContext _context;

        public FoodDiseasePersistence(DataManagementServiceContext context) : base(context)
        {
            _context = context;
        }

        public async Task<FoodDisease> GetByIdAsync(int id)
        {
            IQueryable<FoodDisease> query = _context.FoodDiseases.AsNoTracking()
                .Include(dd => dd.Food)
                .Include(dd => dd.Disease);

            query = query.Where(dd => dd.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<FoodDisease> GetByRelatedIdAsync(int foodId, int diseaseId) 
        {
            IQueryable<FoodDisease> query = _context.FoodDiseases.AsNoTracking()
                .Include(dd => dd.Food)
                .Include(dd => dd.Disease);

            query = query.Where(dd => dd.FoodId == foodId && dd.DiseaseId == diseaseId);

            return await query.FirstOrDefaultAsync();
        }
    }
}