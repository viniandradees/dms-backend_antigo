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
    public class DiseaseFoodDosagePersistence : GeneralPersistence, IDiseaseFoodDosagePersistence
    {
        private readonly DataManagementServiceContext _context;

        public DiseaseFoodDosagePersistence(DataManagementServiceContext context) : base(context)
        {
            _context = context;
        }

        public async Task<DiseaseFoodDosage> GetByIdAsync(int id)
        {
            IQueryable<DiseaseFoodDosage> query = _context.DiseaseFoodDosages.AsNoTracking();

            query = query.Where(ddd => ddd.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<DiseaseFoodDosage> GetByRelatedIdAsync(int diseaseFoodId, int measurementUnitId) 
        {
            IQueryable<DiseaseFoodDosage> query = _context.DiseaseFoodDosages.AsNoTracking();

            query = query.Where(ddd => ddd.DiseaseFoodId == diseaseFoodId && ddd.MeasurementUnitId == measurementUnitId);

            return await query.FirstOrDefaultAsync();
        }
    }
}