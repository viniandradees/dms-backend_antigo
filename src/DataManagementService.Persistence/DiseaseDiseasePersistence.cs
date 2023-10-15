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
    public class DiseaseDiseasePersistence : GeneralPersistence, IDiseaseDiseasePersistence
    {
        private readonly DataManagementServiceContext _context;

        public DiseaseDiseasePersistence(DataManagementServiceContext context) : base(context)
        {
            _context = context;
        }

        public async Task<DiseaseDisease> GetByIdAsync(int id)
        {
            IQueryable<DiseaseDisease> query = _context.DiseaseDiseases.AsNoTracking()
                .Include(dd => dd.Disease)
                .Include(dd => dd.Symptom);

            query = query.Where(dd => dd.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<DiseaseDisease> GetByRelatedIdAsync(int diseaseId, int symptomId) 
        {
            IQueryable<DiseaseDisease> query = _context.DiseaseDiseases.AsNoTracking();

            query = query.Where(dd => dd.DiseaseId == diseaseId && dd.SymptomId == symptomId);

            return await query.FirstOrDefaultAsync();
        }
    }
}