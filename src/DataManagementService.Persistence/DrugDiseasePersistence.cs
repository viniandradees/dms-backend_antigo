using DataManagementService.Domain;
using DataManagementService.Persistence.Contexts;
using DataManagementService.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataManagementService.Persistence
{
    public class DrugDiseasePersistence : GeneralPersistence, IDrugDiseasePersistence
    {
        private readonly DataManagementServiceContext _context;

        public DrugDiseasePersistence(DataManagementServiceContext context) : base(context)
        {
            _context = context;
        }

        public async Task<DrugDisease> GetByIdAsync(int id)
        {
            IQueryable<DrugDisease> query = _context.DrugDiseases.AsNoTracking()
                .Include(dd => dd.Drug)
                .Include(dd => dd.Disease);

            query = query.Where(dd => dd.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<DrugDisease> GetByRelatedIdAsync(int drugId, int diseaseId) 
        {
            IQueryable<DrugDisease> query = _context.DrugDiseases.AsNoTracking()
                .Include(dd => dd.Drug)
                .Include(dd => dd.Disease);

            query = query.Where(dd => dd.DrugId == drugId && dd.DiseaseId == diseaseId);

            return await query.FirstOrDefaultAsync();
        }
    }
}