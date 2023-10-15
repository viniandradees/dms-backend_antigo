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
    public class DiseaseDrugDosagePersistence : GeneralPersistence, IDiseaseDrugDosagePersistence
    {
        private readonly DataManagementServiceContext _context;

        public DiseaseDrugDosagePersistence(DataManagementServiceContext context) : base(context)
        {
            _context = context;
        }

        public async Task<DiseaseDrugDosage> GetByIdAsync(int id)
        {
            IQueryable<DiseaseDrugDosage> query = _context.DiseaseDrugDosages.AsNoTracking();

            query = query.Where(ddd => ddd.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<DiseaseDrugDosage> GetByRelatedIdAsync(int diseaseDrugId, int measurementUnitId) 
        {
            IQueryable<DiseaseDrugDosage> query = _context.DiseaseDrugDosages.AsNoTracking();

            query = query.Where(ddd => ddd.DiseaseDrugId == diseaseDrugId && ddd.MeasurementUnitId == measurementUnitId);

            return await query.FirstOrDefaultAsync();
        }
    }
}