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
    public class DiseaseSupplementDosagePersistence : GeneralPersistence, IDiseaseSupplementDosagePersistence
    {
        private readonly DataManagementServiceContext _context;

        public DiseaseSupplementDosagePersistence(DataManagementServiceContext context) : base(context)
        {
            _context = context;
        }

        public async Task<DiseaseSupplementDosage> GetByIdAsync(int id)
        {
            IQueryable<DiseaseSupplementDosage> query = _context.DiseaseSupplementDosages.AsNoTracking();

            query = query.Where(ddd => ddd.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<DiseaseSupplementDosage> GetByRelatedIdAsync(int diseaseSupplementId, int measurementUnitId) 
        {
            IQueryable<DiseaseSupplementDosage> query = _context.DiseaseSupplementDosages.AsNoTracking();

            query = query.Where(ddd => ddd.DiseaseSupplementId == diseaseSupplementId && ddd.MeasurementUnitId == measurementUnitId);

            return await query.FirstOrDefaultAsync();
        }
    }
}