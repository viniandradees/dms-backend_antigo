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
    public class MeasurementUnitPersistence : GeneralPersistence, IMeasurementUnitPersistence
    {
        private readonly DataManagementServiceContext _context;

        public MeasurementUnitPersistence(DataManagementServiceContext context) : base(context)
        {
            _context = context;
        }

        public async Task<MeasurementUnit[]> GetAllAsync(bool getFullData = false)
        {
            IQueryable<MeasurementUnit> query = _context.MeasurementUnits.AsNoTracking();

            query = query.OrderBy(d => d.Name);

            return await query.ToArrayAsync();
        }

        public async Task<MeasurementUnit> GetByIdAsync(int id)
        {
            IQueryable<MeasurementUnit> query = _context.MeasurementUnits.AsNoTracking();

            query = query.Where(d => d.Id == id);

            return await query.FirstOrDefaultAsync();
        }
        public async Task<MeasurementUnit> GetByNameAsync(string name)
        {
            IQueryable<MeasurementUnit> query = _context.MeasurementUnits.AsNoTracking();

            query = query.Where(d => d.Name == name);

            return await query.FirstOrDefaultAsync();
        }
    }
}