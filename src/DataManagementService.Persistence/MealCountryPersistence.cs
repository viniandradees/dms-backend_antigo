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
    public class MealCountryPersistence : GeneralPersistence, IMealCountryPersistence
    {
        private readonly DataManagementServiceContext _context;

        public MealCountryPersistence(DataManagementServiceContext context) : base(context)
        {
            _context = context;
        }

        public async Task<MealCountry> GetByIdAsync(int id)
        {
            IQueryable<MealCountry> query = _context.MealCountries.AsNoTracking()
                .Include(dd => dd.Meal)
                .Include(dd => dd.Country);

            query = query.Where(dd => dd.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<MealCountry> GetByRelatedIdAsync(int mealId, int countryId) 
        {
            IQueryable<MealCountry> query = _context.MealCountries.AsNoTracking()
                .Include(dd => dd.Meal)
                .Include(dd => dd.Country);

            query = query.Where(dd => dd.MealId == mealId && dd.CountryId == countryId);

            return await query.FirstOrDefaultAsync();
        }
    }
}