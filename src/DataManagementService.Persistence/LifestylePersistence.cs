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
    public class LifestylePersistence : GeneralPersistence, ILifestylePersistence
    {
        private readonly DataManagementServiceContext _context;

        public LifestylePersistence(DataManagementServiceContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Lifestyle[]> GetAllAsync(bool getFullData = false)
        {
            IQueryable<Lifestyle> query = _context.Lifestyles.AsNoTracking();

            if (getFullData) 
            {
                query = query
                .Include(l => l.SideEffects).ThenInclude(se => se.Disease);
            }

            query = query.OrderBy(l => l.Name);

            return await query.ToArrayAsync();
        }

        public async Task<Lifestyle> GetByIdAsync(int id)
        {
            Lifestyle lifestyle = await _context.Lifestyles.AsNoTracking()
                .Include(l => l.SideEffects).ThenInclude(se => se.Disease)
                .Where(l => l.Id == id)
                .FirstOrDefaultAsync();

            if (lifestyle != null)
            {
                lifestyle.SideEffects = lifestyle.SideEffects.OrderBy(td => td.Disease.Name).ToList();
            }

            return lifestyle;
        }
        public async Task<Lifestyle> GetByNameAsync(string name)
        {
            Lifestyle lifestyle = await _context.Lifestyles.AsNoTracking()
                .Include(l => l.SideEffects).ThenInclude(se => se.Disease)
                .Where(l => l.Name == name)
                .FirstOrDefaultAsync();

            if (lifestyle != null)
            {
                lifestyle.SideEffects = lifestyle.SideEffects.OrderBy(td => td.Disease.Name).ToList();
            }

            return lifestyle;
        }
    }
}